<script setup>
import { onMounted, reactive, ref } from 'vue'
import {
  getEmployees,
  getEmployeeById,
  createEmployee,
  updateEmployee,
  deleteEmployee,
  getDepartments,
  getRoles
} from '../services/api'

const loading = ref(false)
const saving = ref(false)
const error = ref('')
const employees = ref([])
const departments = ref([])
const roles = ref([])
const isEditing = ref(false)
const showForm = ref(false)
const selectedId = ref(null)


const filters = reactive({
  Query: '',
  DepartmentId: '',
  RoleId: '',
  Status: ''
})

const form = reactive({
  FirstName: '',
  
  LastName: '',
  Email: '',
  AvailableDays: 14,
  UsedDays: 0,
  DepartmentId: '',
  RoleId: '',
  Status: 2
})

const statusOptions = [
  { value: 1, label: 'Inactivo' },
  { value: 2, label: 'Activo' },
  { value: 3, label: 'En vacaciones' }
]

function resetForm() {
  form.FirstName = ''
  form.LastName = ''
  form.Email = ''
  form.AvailableDays = 14
  form.UsedDays = 0
  form.DepartmentId = ''
  form.RoleId = ''
  form.Status = 2
}

async function loadEmployees() {
  error.value = ''
  loading.value = true
  try {
    employees.value = await getEmployees(filters)
  } catch (err) {
    error.value = err.message
  } finally {
    loading.value = false
  }
}

async function loadMeta() {
  try {
    const [dept, role] = await Promise.all([getDepartments(), getRoles()])
    departments.value = dept
    roles.value = role
  } catch (err) {
    error.value = err.message
  }
}

function openCreate() {
  resetForm()
  isEditing.value = false
  selectedId.value = null
  showForm.value = true
}

async function openEdit(id) {
  error.value = ''
  showForm.value = true
  isEditing.value = true
  selectedId.value = id
  try {
    const data = await getEmployeeById(id)
    form.FirstName = data.firstName || ''
    form.LastName = data.lastName || ''
    form.Email = data.email || ''
    form.AvailableDays = data.availableDays ?? 14
    form.UsedDays = data.usedDays ?? 0
    form.DepartmentId = data.departmentId ?? ''
    form.RoleId = data.roleId ?? ''
    form.Status = data.status ?? 2
  } catch (err) {
    error.value = err.message
  }
}

async function onSave() {
  error.value = ''
  saving.value = true
  try {
    const payload = {
      FirstName: form.FirstName,
      LastName: form.LastName,
      Email: form.Email || null,
      AvailableDays: Number(form.AvailableDays),
      UsedDays: Number(form.UsedDays),
      DepartmentId: Number(form.DepartmentId),
      RoleId: Number(form.RoleId),
      Status: Number(form.Status)
    }

    if (isEditing.value && selectedId.value) {
      await updateEmployee(selectedId.value, payload)
    } else {
      await createEmployee(payload)
    }

    showForm.value = false
    await loadEmployees()
  } catch (err) {
    error.value = err.message
  } finally {
    saving.value = false
  }
}

async function onDelete(id) {
  if (!confirm('¿Seguro que quieres desactivar este empleado?')) return
  error.value = ''
  try {
    await deleteEmployee(id)
    await loadEmployees()
  } catch (err) {
    error.value = err.message
  }
}

onMounted(async () => {
  await loadMeta()
  await loadEmployees()
})
</script>

<template>
  <div class="page">
    <header class="hero">
      <div>
        <h1>Empleados</h1>
        <p>Administra tu personal y su balance de vacaciones.</p>
      </div>
      <button class="primary" @click="openCreate">Nuevo empleado</button>
    </header>

    <section class="card">
      <div class="filters">
        <input v-model="filters.Query" placeholder="Buscar por nombre, apellido o email" />
        <select v-model="filters.DepartmentId">
          <option value="">Departamento</option>
          <option v-for="dept in departments" :key="dept.id" :value="dept.id">
            {{ dept.name }}
          </option>
        </select>
        <select v-model="filters.RoleId">
          <option value="">Rol</option>
          <option v-for="role in roles" :key="role.id" :value="role.id">
            {{ role.name }}
          </option>
        </select>
        <select v-model="filters.Status">
          <option value="">Estado</option>
          <option v-for="opt in statusOptions" :key="opt.value" :value="opt.value">
            {{ opt.label }}
          </option>
        </select>
        <button class="ghost" @click="loadEmployees">Filtrar</button>
      </div>

      <div v-if="error" class="error">{{ error }}</div>
      <div v-if="loading" class="loading">Cargando empleados...</div>

      <table v-if="!loading" class="table">
        <thead>
          <tr>
            <th>Empleado</th>
            <th>Departamento</th>
            <th>Rol</th>
            <th>Estado</th>
            <th>Días</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="employees.length === 0">
            <td colspan="6">No hay empleados registrados.</td>
          </tr>
          <tr v-for="emp in employees" :key="emp.id">
            <td>
              <strong>{{ emp.fullName }}</strong>
            </td>
            <td>{{ emp.departmentName }}</td>
            <td>{{ emp.roleName }}</td>
            <td>{{ emp.status }}</td>
            <td>
              {{ emp.remainingDays }} restantes
              <span class="muted">({{ emp.usedDays }} usados)</span>
            </td>
            <td class="actions">
              <button class="ghost" @click="openEdit(emp.id)">Editar</button>
              <button class="danger" @click="onDelete(emp.id)">Desactivar</button>
            </td>
          </tr>
        </tbody>
      </table>
    </section>

    <section v-if="showForm" class="drawer">
      <div class="drawer-card">
        <header>
          <h2>{{ isEditing ? 'Editar empleado' : 'Nuevo empleado' }}</h2>
          <button class="ghost" @click="showForm = false">Cerrar</button>
        </header>

        <div class="form-grid">
          <label>
            Nombre
            <input v-model="form.FirstName" placeholder="Nombre" />
          </label>
          <label>
            Apellido
            <input v-model="form.LastName" placeholder="Apellido" />
          </label>
          <label>
            Email
            <input v-model="form.Email" placeholder="correo@empresa.com" />
          </label>
          <label>
            Departamento
            <select v-model="form.DepartmentId">
              <option value="">Selecciona</option>
              <option v-for="dept in departments" :key="dept.id" :value="dept.id">
                {{ dept.name }}
              </option>
            </select>
          </label>
          <label>
            Rol
            <select v-model="form.RoleId">
              <option value="">Selecciona</option>
              <option v-for="role in roles" :key="role.id" :value="role.id">
                {{ role.name }}
              </option>
            </select>
          </label>
          <label>
            Estado
            <select v-model="form.Status">
              <option v-for="opt in statusOptions" :key="opt.value" :value="opt.value">
                {{ opt.label }}
              </option>
            </select>
          </label>
          <label>
            Días disponibles
            <input type="number" min="0" v-model="form.AvailableDays" />
          </label>
          <label>
            Días usados
            <input type="number" min="0" v-model="form.UsedDays" />
          </label>
        </div>

        <footer>
          <button class="ghost" @click="showForm = false">Cancelar</button>
          <button class="primary" :disabled="saving" @click="onSave">
            {{ saving ? 'Guardando...' : 'Guardar' }}
          </button>
        </footer>
      </div>
    </section>
  </div>
</template>

<style scoped>
:global(body) {
  margin: 0;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: #f5f5f5;
  color: #1c1c1c;
}

.page {
  max-width: 1100px;
  margin: 40px auto;
  padding: 0 24px 80px;
}

.hero {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 16px;
  margin-bottom: 24px;
}

.hero h1 {
  margin: 0 0 8px;
  font-size: 28px;
}

.hero p {
  margin: 0;
  color: #5a5a5a;
}

.card {
  background: #fff;
  border-radius: 16px;
  padding: 20px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.06);
}

.filters {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(160px, 1fr));
  gap: 12px;
  margin-bottom: 16px;
}

input,
select {
  width: 100%;
  padding: 10px 12px;
  border-radius: 10px;
  border: 1px solid #e0e0e0;
  font-size: 14px;
}

.table {
  width: 100%;
  border-collapse: collapse;
}

.table th,
.table td {
  text-align: left;
  padding: 12px 10px;
  border-bottom: 1px solid #eee;
  font-size: 14px;
}

.actions {
  display: flex;
  gap: 8px;
}

.primary,
.ghost,
.danger {
  border: none;
  border-radius: 10px;
  padding: 10px 16px;
  font-weight: 600;
  cursor: pointer;
}

.primary {
  background: #111827;
  color: #fff;
}

.ghost {
  background: #f0f0f0;
  color: #333;
}

.danger {
  background: #ffe5e5;
  color: #b42318;
}

.muted {
  color: #6b7280;
  font-size: 12px;
}

.error {
  background: #ffe5e5;
  border: 1px solid #ffb4b4;
  padding: 12px;
  border-radius: 10px;
  margin-bottom: 12px;
}

.loading {
  padding: 12px 0;
  color: #6b7280;
}

.drawer {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.35);
  display: flex;
  justify-content: center;
  align-items: flex-start;
  padding: 40px 16px;
}

.drawer-card {
  background: #fff;
  border-radius: 16px;
  padding: 24px;
  width: min(900px, 100%);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.15);
}

.drawer-card header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
  gap: 16px;
}

.drawer-card footer {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style>

<template>
  <div class="page-section">
    <PageHeader title="Empleados" />

    <div class="card mt-4">
      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar por nombre completo, email o ID"
            />

            <select v-model="departmentFilter" class="input table-select">
              <option :value="null">Filtrar por departamento</option>
              <option
                v-for="option in departments"
                :key="option.description"
                :value="option.id"
              >
                {{ option.description }}
              </option>
            </select>

            <select v-model="roleFilter" class="input table-select">
              <option :value="null">Filtrar por rol</option>
              <option
                v-for="option in roles"
                :key="option.description"
                :value="option.id"
              >
                {{ option.description }}
              </option>
            </select>

            <select v-model="statusFilter" class="input table-select">
              <option :value="null">Filtrar por estado</option>
              <option
                v-for="option in statusOptions"
                :key="option.description"
                :value="option.id"
              >
                {{ option.description }}
              </option>
            </select>

            <button class="btn btn-primary shine-effect" @click="openCreateModal">
              Crear nuevo empleado
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre completo</th>
              <th>Email</th>
              <th>Departamento</th>
              <th>Rol</th>
              <th>Estado</th>
              <th>Días disponibles</th>
              <th>Días usados</th>
              <th>Días restantes</th>
              <th>Días extra restantes</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredEmployees.length === 0">
              <td colspan="11" class="empty-state-cell">
                No se encontraron empleados.
              </td>
            </tr>

            <tr v-for="item in filteredEmployees" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.firstName }} {{ item.lastName }}</td>
              <td>{{ item.email || '-' }}</td>
              <td>{{ item.department.description }}</td>
              <td>{{ item.role.description }}</td>
              <td>{{ item.status.description }}</td>
              <td>{{ item.availableDays }}</td>
              <td>{{ item.usedDays }}</td>
              <td>{{ item.remainingDays }}</td>
              <td>{{ item.remainingExtraBenefitDays }}</td>
              <td class="actions-column">
                <RowActions
                  @edit="edit(item.id)"
                  @cancel="remove(item.id)"
                />
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <div class="page-section">
      <ModalComponent
        v-model="isModalOpen"
        @close="closeModal"
        @dismiss="closeModal"
      >
        <div class="vacation-modal">
          <h2 class="modal-title">{{ modalTitle }}</h2>
          <p class="modal-subtitle">
            Completa la información para {{ isEditing ? 'editar' : 'registrar' }} el empleado.
          </p>

          <div class="form-grid">
            <div class="form-group">
              <label class="form-label">Nombre</label>
              <input
                v-model="form.firstName"
                class="input"
                type="text"
                placeholder="Ej: Nasser"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Apellido</label>
              <input
                v-model="form.lastName"
                class="input"
                type="text"
                placeholder="Ej: Issa"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Email</label>
              <input
                v-model="form.email"
                class="input"
                type="email"
                placeholder="Ej: correo@empresa.com"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Días disponibles</label>
              <input
                v-model.number="form.availableDays"
                class="input"
                type="number"
                min="0"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Días usados</label>
              <input
                v-model.number="form.usedDays"
                class="input"
                type="number"
                min="0"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Departamento</label>
              <select v-model="form.departmentId" class="input">
                <option :value="0" disabled>Selecciona un departamento</option>
                <option
                  v-for="option in departments"
                  :key="option.description"
                  :value="option.id"
                >
                  {{ option.description }}
                </option>
              </select>
            </div>

            <div class="form-group">
              <label class="form-label">Rol</label>
              <select v-model="form.roleId" class="input">
                <option :value="0" disabled>Selecciona un rol</option>
                <option
                  v-for="option in roles"
                  :key="option.description"
                  :value="option.id"
                >
                  {{ option.description }}
                </option>
              </select>
            </div>

            <div class="form-group">
              <label class="form-label">Estado</label>
              <select v-model="form.status" class="input">
                <option
                  v-for="option in statusOptions"
                  :key="option.description"
                  :value="option.id"
                >
                  {{ option.description }}
                </option>
              </select>
            </div>
          </div>

          <div class="modal-actions">
            <button class="btn btn-danger" @click="closeModal">
              Cancelar
              <fa-icon icon="circle-xmark" class="ml-1" />
            </button>

            <button
              class="btn btn-primary shine-effect"
              :disabled="working || !isFormValid"
              @click="saveEmployee"
            >
              {{ submitLabel }}
              <fa-icon :icon="isEditing ? 'floppy-disk' : 'plus'" class="ml-1" />
            </button>
          </div>
        </div>
      </ModalComponent>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import ModalComponent from '@/components/Modal.component.vue'
import PageHeader from '@/components/PageHeader.component.vue'
import RowActions from '@/components/RowActions.component.vue'
import type { Item } from '@/models/ItemModel'
import type { EmployeeDataDto } from '@/models/employee/EmployeeDataDto'
import type { EmployeeListDto } from '@/models/employee/EmployeeListDto'
import { EmployeeStatusEnum } from '@/models/enums/EmployeeStatusEnum'

type EmployeeForm = {
  firstName: string
  lastName: string
  email: string
  availableDays: number
  usedDays: number
  departmentId: number
  roleId: number
  status: number
}

const search = ref('')
const departmentFilter = ref<number | null>(null)
const roleFilter = ref<number | null>(null)
const statusFilter = ref<number | null>(null)
const creating = ref(false)
const editing = ref<number | null>(null)
const working = ref(false)

const form = ref<EmployeeForm>({
  firstName: '',
  lastName: '',
  email: '',
  availableDays: 14,
  usedDays: 0,
  departmentId: 0,
  roleId: 0,
  status: EmployeeStatusEnum.Active
})

function openCreateModal() {
  form.value = {
    firstName: '',
    lastName: '',
    email: '',
    availableDays: 14,
    usedDays: 0,
    departmentId: 0,
    roleId: 0,
    status: EmployeeStatusEnum.Active
  }

  creating.value = true
  editing.value = null
}

function closeModal() {
  creating.value = false
  editing.value = null
}

function saveEmployee() {
  const payload: EmployeeDataDto = {
    firstName: form.value.firstName.trim(),
    lastName: form.value.lastName.trim(),
    email: form.value.email.trim() || null,
    availableDays: form.value.availableDays,
    usedDays: form.value.usedDays,
    departmentId: form.value.departmentId,
    roleId: form.value.roleId,
    status: form.value.status
  }

  console.log('guardar employee', payload)

  // TODO: llamar API create / update con payload

  creating.value = false
  editing.value = null
}

async function edit(id: number) {
  const employee = employees.value.find(item => item.id === id)
  if (!employee) return

  form.value = {
    firstName: employee.firstName,
    lastName: employee.lastName,
    email: employee.email || '',
    availableDays: employee.availableDays,
    usedDays: employee.usedDays,
    departmentId: employee.department.id,
    roleId: employee.role.id,
    status: employee.status.id
  }

  creating.value = false
  editing.value = id
}

async function remove(id: number) {
  try {
    console.log('eliminando employee', id)

    // TODO: llamar API delete
  } catch (error) {
    console.error(error)
  }
}

const isModalOpen = computed(() => creating.value || editing.value !== null)
const isEditing = computed(() => editing.value !== null)

const modalTitle = computed(() =>
  isEditing.value
    ? `Editar empleado: ${editing.value}`
    : 'Nuevo empleado'
)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})

const isFormValid = computed(() => {
  return (
    form.value.firstName.trim().length > 0 &&
    form.value.lastName.trim().length > 0 &&
    form.value.availableDays >= 0 &&
    form.value.usedDays >= 0 &&
    form.value.departmentId > 0 &&
    form.value.roleId > 0
  )
})

const filteredEmployees = computed(() => {
  return employees.value.filter((item) => {
    const matchesSearch =
      item.firstName.toLowerCase().includes(search.value.toLowerCase()) || item.lastName.toLowerCase().includes(search.value.toLowerCase()) ||
      (item.email || '').toLowerCase().includes(search.value.toLowerCase()) ||
      item.id.toString().includes(search.value)

    const matchesDepartment =
      departmentFilter.value !== null
        ? item.department.id === Number(departmentFilter.value)
        : true

    const matchesRole =
      roleFilter.value !== null
        ? item.role.id === Number(roleFilter.value)
        : true

    const matchesStatus =
      statusFilter.value !== null
        ? item.status.id === Number(statusFilter.value)
        : true

    return matchesSearch && matchesDepartment && matchesRole && matchesStatus
  })
})

const statusOptions: Item[] = [
  { id: EmployeeStatusEnum.Active, description: 'Activo' },
  { id: EmployeeStatusEnum.Inactive, description: 'Inactivo' },
  { id: EmployeeStatusEnum.Suspended, description: 'Suspendido' }
]

const departments: Item[] = [
  { id: 1, description: 'Recursos Humanos' },
  { id: 2, description: 'Tecnología' },
  { id: 3, description: 'Finanzas' }
]

const roles: Item[] = [
  { id: 1, description: 'Administrador' },
  { id: 2, description: 'Supervisor' },
  { id: 3, description: 'Empleado' }
]

// Temporal mientras llega la API
const employees = ref<EmployeeListDto[]>([
  {
    id: 1,
    firstName: 'Nasser',
    lastName: 'Issa',
    email: 'nasser@empresa.com',
    department: {
      id: 2,
      description: 'Tecnología'
    },
    role: {
      id: 1,
      description: 'Administrador'
    },
    status: {
      id: EmployeeStatusEnum.Active,
      description: 'Activo'
    },
    availableDays: 14,
    usedDays: 4,
    remainingDays: 10,
    remainingExtraBenefitDays: 2
  },
  {
    id: 2,
    firstName: 'María',
    lastName: 'Rodríguez',
    email: 'maria@empresa.com',
    department: {
      id: 1,
      description: 'Recursos Humanos'
    },
    role: {
      id: 2,
      description: 'Supervisor'
    },
    status: {
      id: EmployeeStatusEnum.Inactive,
      description: 'Inactivo'
    },
    availableDays: 14,
    usedDays: 10,
    remainingDays: 4,
    remainingExtraBenefitDays: 1
  },
  {
    id: 3,
    firstName: 'Carlos',
    lastName: 'Gonzalez',
    email: 'carlos@empresa.com',
    department: {
      id: 3,
      description: 'Finanzas'
    },
    role: {
      id: 3,
      description: 'Empleado'
    },
    status: {
      id: EmployeeStatusEnum.Suspended,
      description: 'Suspendido'
    },
    availableDays: 14,
    usedDays: 14,
    remainingDays: 0,
    remainingExtraBenefitDays: 0
  }
])
</script>
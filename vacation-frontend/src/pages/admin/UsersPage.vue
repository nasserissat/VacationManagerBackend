<template>
  <div class="page-section">
    <PageHeader title="Usuarios" />

    <div class="card mt-4">
      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar usuarios..."
            />

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
              Agregar usuario
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Usuario</th>
              <th>Empleado</th>
              <th>Rol</th>
              <th>Estado</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredUsers.length === 0">
              <td colspan="6" class="empty-state-cell">
                No se encontraron usuarios.
              </td>
            </tr>

            <tr v-for="item in filteredUsers" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.username }}</td>
              <td>{{ item.employee?.description || '-' }}</td>
              <td>{{ item.role.description }}</td>
              <td>{{ item.status.description }}</td>
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

    <ModalComponent
      v-model="isModalOpen"
      @close="closeModal"
      @dismiss="closeModal"
    >
      <div class="vacation-modal">
        <h2 class="modal-title">{{ modalTitle }}</h2>
        <p class="modal-subtitle">
          Completa la información para {{ isEditing ? 'editar' : 'registrar' }} el usuario.
        </p>

        <div class="form-grid">
          <div class="form-group">
            <label class="form-label">Nombre de usuario</label>
            <input
              v-model="form.username"
              class="input"
              type="text"
              placeholder="Ej: superadmin"
            />
          </div>

          <div class="form-group">
            <label class="form-label">Empleado vinculado</label>
            <select v-model="form.employeeId" class="input">
              <option :value="null">Sin empleado</option>
              <option
                v-for="option in employees"
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

          <div class="form-group" v-if="!isEditing">
            <label class="form-label">Contraseña</label>
            <input
              v-model="form.password"
              class="input"
              type="password"
              placeholder="********"
            />
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
            @click="saveUser"
          >
            {{ submitLabel }}
            <fa-icon :icon="isEditing ? 'floppy-disk' : 'plus'" class="ml-1" />
          </button>
        </div>
      </div>
    </ModalComponent>
  </div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import PageHeader from '@/components/PageHeader.component.vue'
import ModalComponent from '@/components/Modal.component.vue'
import RowActions from '@/components/RowActions.component.vue'
import type { Item } from '@/models/ItemModel'
import type { UserDataDto } from '@/models/admin/UserDataDto'
import type { UserListDto } from '@/models/admin/UserListDto'

type UserForm = {
  username: string
  password: string
  roleId: number
  employeeId: number | null
  status: number
}

const search = ref('')
const roleFilter = ref<number | null>(null)
const statusFilter = ref<number | null>(null)
const creating = ref(false)
const editing = ref<number | null>(null)
const working = ref(false)

const form = ref<UserForm>({
  username: '',
  password: '',
  roleId: 0,
  employeeId: null,
  status: 2
})

function openCreateModal() {
  form.value = {
    username: '',
    password: '',
    roleId: 0,
    employeeId: null,
    status: 2
  }

  creating.value = true
  editing.value = null
}

function closeModal() {
  creating.value = false
  editing.value = null
}

function saveUser() {
  const payload: UserDataDto = {
    username: form.value.username.trim(),
    password: isEditing.value ? null : form.value.password,
    roleId: form.value.roleId,
    employeeId: form.value.employeeId,
    status: form.value.status
  }

  console.log('guardar user', payload)

  creating.value = false
  editing.value = null
}

async function edit(id: number) {
  const user = users.value.find(item => item.id === id)
  if (!user) return

  form.value = {
    username: user.username,
    password: '',
    roleId: user.role.id,
    employeeId: user.employee?.id ?? null,
    status: user.status.id
  }

  creating.value = false
  editing.value = id
}

async function remove(id: number) {
  console.log('eliminando user', id)
}

const isModalOpen = computed(() => creating.value || editing.value !== null)
const isEditing = computed(() => editing.value !== null)

const modalTitle = computed(() =>
  isEditing.value ? `Editar usuario: ${editing.value}` : 'Nuevo usuario'
)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})

const isFormValid = computed(() => {
  return (
    form.value.username.trim().length > 0 &&
    form.value.roleId > 0 &&
    (isEditing.value || form.value.password.trim().length > 0)
  )
})

const filteredUsers = computed(() => {
  return users.value.filter((item) => {
    const matchesSearch =
      item.username.toLowerCase().includes(search.value.toLowerCase()) ||
      item.id.toString().includes(search.value) ||
      (item.employee?.description || '').toLowerCase().includes(search.value.toLowerCase())

    const matchesRole =
      roleFilter.value !== null
        ? item.role.id === Number(roleFilter.value)
        : true

    const matchesStatus =
      statusFilter.value !== null
        ? item.status.id === Number(statusFilter.value)
        : true

    return matchesSearch && matchesRole && matchesStatus
  })
})

const statusOptions: Item[] = [
  { id: 1, description: 'Inactivo' },
  { id: 2, description: 'Activo' }
]

const roles: Item[] = [
  { id: 1, description: 'Director' },
  { id: 2, description: 'Gerente' },
  { id: 3, description: 'Subgerente' },
  { id: 4, description: 'Auditor' }
]

const employees: Item[] = [
  { id: 1, description: 'Nasser Issa' },
  { id: 2, description: 'María Rodríguez' },
  { id: 3, description: 'Carlos Méndez' }
]

const users = ref<UserListDto[]>([
  {
    id: 1,
    username: 'superadmin',
    role: { id: 1, description: 'Director' },
    employee: null,
    status: { id: 2, description: 'Activo' }
  },
  {
    id: 2,
    username: 'maria.rh',
    role: { id: 2, description: 'Gerente' },
    employee: { id: 2, description: 'María Rodríguez' },
    status: { id: 2, description: 'Activo' }
  }
])
</script>
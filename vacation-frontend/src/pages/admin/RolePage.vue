<template>
  <div class="page-section">
    <PageHeader title="Roles" />

    <div class="card mt-4">
      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar roles..."
            />

            <button class="btn btn-primary shine-effect" @click="openCreateModal">
              Crear nuevo rol
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Posición</th>
              <th>Permisos</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredRoles.length === 0">
              <td colspan="4" class="empty-state-cell">
                No se encontraron roles.
              </td>
            </tr>

            <tr v-for="item in filteredRoles" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.position }}</td>
              <td>{{ item.permissions.map(p => p.description).join(', ') || '-' }}</td>
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
          Completa la información para {{ isEditing ? 'editar' : 'registrar' }} el rol.
        </p>

        <div class="form-grid">
          <div class="form-group">
            <label class="form-label">Posición</label>
            <input
              v-model="form.position"
              class="input"
              type="text"
              placeholder="Ej: Gerente"
            />
          </div>

          <div class="form-group full-width">
            <label class="form-label">Permisos</label>
            <div class="permissions-grid">
              <label
                v-for="permission in permissions"
                :key="permission.id"
                class="permission-option"
              >
                <input
                  type="checkbox"
                  :value="permission.id"
                  v-model="form.permissionIds"
                />
                <span>{{ permission.description }}</span>
              </label>
            </div>
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
            @click="saveRole"
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
import type { PermissionListDto } from '@/models/admin/PermissionListDto'
import type { RoleDataDto } from '@/models/admin/RoleDataDto'
import type { RoleListDto } from '@/models/admin/RoleListDto'

type RoleForm = {
  position: string
  permissionIds: number[]
}

const search = ref('')
const creating = ref(false)
const editing = ref<number | null>(null)
const working = ref(false)

const form = ref<RoleForm>({
  position: '',
  permissionIds: []
})

function openCreateModal() {
  form.value = {
    position: '',
    permissionIds: []
  }

  creating.value = true
  editing.value = null
}

function closeModal() {
  creating.value = false
  editing.value = null
}

function saveRole() {
  const payload: RoleDataDto = {
    position: form.value.position.trim(),
    permissionIds: form.value.permissionIds
  }

  console.log('guardar role', payload)

  creating.value = false
  editing.value = null
}

async function edit(id: number) {
  const role = roles.value.find(item => item.id === id)
  if (!role) return

  form.value = {
    position: role.position,
    permissionIds: role.permissions.map(item => item.id)
  }

  creating.value = false
  editing.value = id
}

async function remove(id: number) {
  console.log('eliminando role', id)
}

const isModalOpen = computed(() => creating.value || editing.value !== null)
const isEditing = computed(() => editing.value !== null)

const modalTitle = computed(() =>
  isEditing.value ? `Editar rol: ${editing.value}` : 'Nuevo rol'
)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})

const isFormValid = computed(() => form.value.position.trim().length > 0)

const filteredRoles = computed(() => {
  return roles.value.filter((item) =>
    item.position.toLowerCase().includes(search.value.toLowerCase()) ||
    item.id.toString().includes(search.value)
  )
})

const permissions = ref<PermissionListDto[]>([
  { id: 1, description: 'Ver usuarios' },
  { id: 2, description: 'Crear usuarios' },
  { id: 3, description: 'Editar usuarios' },
  { id: 4, description: 'Eliminar usuarios' },
  { id: 5, description: 'Ver roles' },
  { id: 6, description: 'Editar roles' }
])

const roles = ref<RoleListDto[]>([
  {
    id: 1,
    position: 'Director',
    permissions: [
      { id: 1, description: 'Ver usuarios' },
      { id: 2, description: 'Crear usuarios' },
      { id: 3, description: 'Editar usuarios' },
      { id: 4, description: 'Eliminar usuarios' },
      { id: 5, description: 'Ver roles' },
      { id: 6, description: 'Editar roles' }
    ]
  },
  {
    id: 2,
    position: 'Auditor',
    permissions: [
      { id: 1, description: 'Ver usuarios' },
      { id: 5, description: 'Ver roles' }
    ]
  }
])
</script>
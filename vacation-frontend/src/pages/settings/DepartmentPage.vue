<template>
  <div class="page-section">
    <PageHeader title="Departamentos" />

    <div class="card mt-4">
      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar por nombre o ID"
            />

            <button class="btn btn-primary shine-effect" @click="openCreateModal">
              Crear nuevo departamento
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredDepartments.length === 0">
              <td colspan="3" class="empty-state-cell">
                No se encontraron departamentos.
              </td>
            </tr>

            <tr v-for="item in filteredDepartments" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.name }}</td>
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
            Completa la información para {{ isEditing ? 'editar' : 'registrar' }} el departamento.
          </p>

          <div class="form-grid">
            <div class="form-group">
              <label class="form-label">Nombre</label>
              <input
                v-model="form.name"
                class="input"
                type="text"
                placeholder="Ej: Recursos Humanos"
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
              :disabled="working || !form.name.trim()"
              @click="saveDepartment"
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
import type { DepartmentDataDto } from '@/models/department/DepartmentDataDto'
import type { DepartmentListDto } from '@/models/department/DepartmentListDto'

const search = ref('')
const creating = ref(false)
const editing = ref<number | null>(null)
const working = ref(false)

const form = ref<DepartmentDataDto>({
  name: ''
})

function openCreateModal() {
  form.value = {
    name: ''
  }

  creating.value = true
  editing.value = null
}

function closeModal() {
  creating.value = false
  editing.value = null
}

function saveDepartment() {
  console.log('guardar department', form.value)

  // TODO: llamar API con DepartmentDataDto
  // create => { name: form.value.name }
  // update => { name: form.value.name }

  creating.value = false
  editing.value = null
}

async function edit(id: number) {
  const department = departments.value.find(item => item.id === id)
  if (!department) return

  form.value = {
    name: department.name
  }

  creating.value = false
  editing.value = id
}

async function remove(id: number) {
  try {
    console.log('eliminando department', id)

    // TODO: llamar API delete
  } catch (error) {
    console.error(error)
  }
}

const isModalOpen = computed(() => creating.value || editing.value !== null)

const isEditing = computed(() => editing.value !== null)

const modalTitle = computed(() =>
  isEditing.value
    ? `Editar departamento: ${editing.value}`
    : 'Nuevo departamento'
)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})

const filteredDepartments = computed(() => {
  return departments.value.filter((item) => {
    const matchesSearch =
      item.name.toLowerCase().includes(search.value.toLowerCase()) ||
      item.id.toString().includes(search.value)

    return matchesSearch
  })
})

// Temporal mientras llega la API
const departments = ref<DepartmentListDto[]>([
  {
    id: 1,
    name: 'Gerencia Desarrollo Pruebas de Adutoría'
  },
  {
    id: 2,
    name: 'Gerencia Diseño y Automatización de Pruebas'
  },
  {
    id: 3,
    name: 'Gerencia de Validación de Pruebas'
  }
])
</script>
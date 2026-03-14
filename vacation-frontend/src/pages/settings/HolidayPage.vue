<template>
  <div class="page-section">
    <PageHeader title="Feriados" />

    <div class="card mt-4">
      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar por nombre, año o ID"
            />

            <select v-model="statusFilter" class="input table-select">
              <option :value="null">Filtrar por estado</option>
              <option
                v-for="option in statusOptions"
                :key="option.label"
                :value="option.value"
              >
                {{ option.label }}
              </option>
            </select>

            <button class="btn btn-gray" @click="openImportModal">
              Importar feriados
              <fa-icon icon="file-import" class="ml-1" />
            </button>

            <button class="btn btn-primary shine-effect" @click="openCreateModal">
              Crear nuevo feriado
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Año</th>
              <th>Fecha inicio</th>
              <th>Fecha fin</th>
              <th>Estado</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredHolidays.length === 0">
              <td colspan="7" class="empty-state-cell">
                No se encontraron feriados.
              </td>
            </tr>

            <tr v-for="item in filteredHolidays" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.name }}</td>
              <td>{{ item.year }}</td>
              <td>{{ formatDate(item.startDate) }}</td>
              <td>{{ formatDate(item.endDate) }}</td>
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

    <div class="page-section">
      <ModalComponent
        v-model="isModalOpen"
        @close="closeModal"
        @dismiss="closeModal"
      >
        <div class="vacation-modal">
          <h2 class="modal-title">{{ modalTitle }}</h2>
          <p class="modal-subtitle">
            Completa la información para {{ isEditing ? 'editar' : 'registrar' }} el feriado.
          </p>

          <div class="form-grid">
            <div class="form-group">
              <label class="form-label">Nombre</label>
              <input
                v-model="form.name"
                class="input"
                type="text"
                placeholder="Ej: Día de la Independencia"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Año</label>
              <input
                v-model.number="form.year"
                class="input"
                type="number"
                min="2000"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Fecha inicio</label>
              <input
                v-model="form.startDate"
                class="input"
                type="date"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Fecha fin</label>
              <input
                v-model="form.endDate"
                class="input"
                type="date"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Estado</label>
              <select v-model="form.status" class="input">
                <option
                  v-for="option in statusOptions"
                  :key="option.label"
                  :value="option.value"
                >
                  {{ option.label }}
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
              @click="saveHoliday"
            >
              {{ submitLabel }}
              <fa-icon :icon="isEditing ? 'floppy-disk' : 'plus'" class="ml-1" />
            </button>
          </div>
        </div>
      </ModalComponent>
    </div>

    <div class="page-section">
      <ModalComponent
        v-model="isImportModalOpen"
        @close="closeImportModal"
        @dismiss="closeImportModal"
      >
        <div class="vacation-modal">
          <h2 class="modal-title">Importar feriados</h2>
          <p class="modal-subtitle">
            Aquí podrás cargar o importar los feriados desde una fuente externa.
          </p>

          <div class="form-grid">
            <div class="form-group">
              <label class="form-label">Archivo o fuente</label>
              <input
                class="input"
                type="file"
                disabled
              />
            </div>
          </div>

          <div class="modal-actions">
            <button class="btn btn-danger" @click="closeImportModal">
              Cancelar
              <fa-icon icon="circle-xmark" class="ml-1" />
            </button>

            <button class="btn btn-primary shine-effect" @click="importHolidays">
              Importar
              <fa-icon icon="file-import" class="ml-1" />
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
import type { HolidayDataDto } from '@/models/vacation/HolidayDataDto'
import type { HolidayListDto } from '@/models/vacation/HolidayListDto'

type HolidayForm = {
  name: string
  year: number
  startDate: string
  endDate: string
  status: number
}

const search = ref('')
const statusFilter = ref<number | null>(null)
const creating = ref(false)
const editing = ref<number | null>(null)
const working = ref(false)
const isImportModalOpen = ref(false)

const form = ref<HolidayForm>({
  name: '',
  year: new Date().getFullYear(),
  startDate: '',
  endDate: '',
  status: 2
})

function openCreateModal() {
  form.value = {
    name: '',
    year: new Date().getFullYear(),
    startDate: '',
    endDate: '',
    status: 2
  }

  creating.value = true
  editing.value = null
}

function closeModal() {
  creating.value = false
  editing.value = null
}

function openImportModal() {
  isImportModalOpen.value = true
}

function closeImportModal() {
  isImportModalOpen.value = false
}

function importHolidays() {
  console.log('estructura lista para importar feriados')
  // TODO: implementar lógica de importación
  isImportModalOpen.value = false
}

function saveHoliday() {
  const payload: HolidayDataDto = {
    name: form.value.name.trim(),
    year: form.value.year,
    startDate: form.value.startDate,
    endDate: form.value.endDate || null,
    status: form.value.status
  }

  console.log('guardar holiday', payload)

  // TODO: llamar API create / update con payload

  creating.value = false
  editing.value = null
}

async function edit(id: number) {
  const holiday = holidays.value.find(item => item.id === id)
  if (!holiday) return

  form.value = {
    name: holiday.name,
    year: holiday.year,
    startDate: toDateInputValue(holiday.startDate),
    endDate: toDateInputValue(holiday.endDate),
    status: holiday.status.id
  }

  creating.value = false
  editing.value = id
}

async function remove(id: number) {
  try {
    console.log('eliminando holiday', id)

    // TODO: llamar API delete
  } catch (error) {
    console.error(error)
  }
}

const isModalOpen = computed(() => creating.value || editing.value !== null)

const isEditing = computed(() => editing.value !== null)

const modalTitle = computed(() =>
  isEditing.value
    ? `Editar feriado: ${editing.value}`
    : 'Nuevo feriado'
)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})

const isFormValid = computed(() => {
  return (
    form.value.name.trim().length > 0 &&
    form.value.year > 0 &&
    form.value.startDate.length > 0 &&
    (!form.value.endDate || form.value.startDate <= form.value.endDate)
  )
})

const filteredHolidays = computed(() => {
  return holidays.value.filter((item) => {
    const matchesSearch =
      item.name.toLowerCase().includes(search.value.toLowerCase()) ||
      item.id.toString().includes(search.value) ||
      item.year.toString().includes(search.value)

    const matchesStatus =
      statusFilter.value !== null
        ? item.status.id === Number(statusFilter.value)
        : true

    return matchesSearch && matchesStatus
  })
})

function formatDate(value: string | null) {
  if (!value) return '-'
  return value.slice(0, 10)
}

function toDateInputValue(value: string | null) {
  if (!value) return ''
  return value.slice(0, 10)
}

const statusOptions = [
  { value: 1, label: 'Inactivo' },
  { value: 2, label: 'Activo' }
]

// Temporal mientras llega la API
const holidays = ref<HolidayListDto[]>([
  {
    id: 1,
    name: 'Año Nuevo',
    year: 2026,
    startDate: '2026-01-01T00:00:00',
    endDate: null,
    status: {
      id: 2,
      description: 'Activo'
    }
  },
  {
    id: 2,
    name: 'Día de Duarte',
    year: 2026,
    startDate: '2026-01-26T00:00:00',
    endDate: null,
    status: {
      id: 2,
      description: 'Activo'
    }
  },
  {
    id: 3,
    name: 'Semana Santa',
    year: 2026,
    startDate: '2026-04-03T00:00:00',
    endDate: '2026-04-05T00:00:00',
    status: {
      id: 2,
      description: 'Activo'
    }
  },
  {
    id: 4,
    name: 'Feriado temporal',
    year: 2026,
    startDate: '2026-08-10T00:00:00',
    endDate: null,
    status: {
      id: 1,
      description: 'Inactivo'
    }
  }
])
</script>
<template>
  <div class="page-section">
    <PageHeader title="Días extra de vacaciones" />

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

            <button class="btn btn-primary shine-effect" @click="openCreateModal">
              Crear nuevo día extra
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Días concedidos</th>
              <th>Válido desde</th>
              <th>Válido hasta</th>
              <th>Estado</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredExtraBenefitDays.length === 0">
              <td colspan="7" class="empty-state-cell">
                No se encontraron días extra.
              </td>
            </tr>

            <tr v-for="item in filteredExtraBenefitDays" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.name }}</td>
              <td>{{ item.daysGranted }}</td>
              <td>{{ formatDate(item.validFrom) }}</td>
              <td>{{ formatDate(item.validTo) }}</td>
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
            Completa la información para {{ isEditing ? 'editar' : 'registrar' }} el día extra.
          </p>

          <div class="form-grid">
            <div class="form-group">
              <label class="form-label">Nombre</label>
              <input
                v-model="form.name"
                class="input"
                type="text"
                placeholder="Ej: Cumpleaños"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Días concedidos</label>
              <input
                v-model.number="form.daysGranted"
                class="input"
                type="number"
                min="1"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Válido desde</label>
              <input
                v-model="form.validFrom"
                class="input"
                type="date"
              />
            </div>

            <div class="form-group">
              <label class="form-label">Válido hasta</label>
              <input
                v-model="form.validTo"
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
              @click="saveExtraBenefitDay"
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
import type { ExtraBenefitDayDataDto } from '@/models/vacation/ExtraBenefitDayDataDto'
import type { ExtraBenefitDayListDto } from '@/models/vacation/ExtraBenefitDayListDto'

type ExtraBenefitDayForm = {
  name: string
  daysGranted: number
  validFrom: string
  validTo: string
  status: number
}

const search = ref('')
const statusFilter = ref<number | null>(null)
const creating = ref(false)
const editing = ref<number | null>(null)
const working = ref(false)

const form = ref<ExtraBenefitDayForm>({
  name: '',
  daysGranted: 1,
  validFrom: '',
  validTo: '',
  status: 2
})

function openCreateModal() {
  form.value = {
    name: '',
    daysGranted: 1,
    validFrom: '',
    validTo: '',
    status: 2
  }

  creating.value = true
  editing.value = null
}

function closeModal() {
  creating.value = false
  editing.value = null
}

function saveExtraBenefitDay() {
  const payload: ExtraBenefitDayDataDto = {
    name: form.value.name.trim(),
    daysGranted: form.value.daysGranted,
    validFrom: form.value.validFrom || null,
    validTo: form.value.validTo || null,
    status: form.value.status
  }

  console.log('guardar extra benefit day', payload)

  // TODO: llamar API create / update con payload

  creating.value = false
  editing.value = null
}

async function edit(id: number) {
  const extraBenefitDay = extraBenefitDays.value.find(item => item.id === id)
  if (!extraBenefitDay) return

  form.value = {
    name: extraBenefitDay.name,
    daysGranted: extraBenefitDay.daysGranted,
    validFrom: toDateInputValue(extraBenefitDay.validFrom),
    validTo: toDateInputValue(extraBenefitDay.validTo),
    status: extraBenefitDay.status.id
  }

  creating.value = false
  editing.value = id
}

async function remove(id: number) {
  try {
    console.log('eliminando extra benefit day', id)

    // TODO: llamar API delete
  } catch (error) {
    console.error(error)
  }
}

const isModalOpen = computed(() => creating.value || editing.value !== null)

const isEditing = computed(() => editing.value !== null)

const modalTitle = computed(() =>
  isEditing.value
    ? `Editar día extra: ${editing.value}`
    : 'Nuevo día extra'
)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})

const isFormValid = computed(() => {
  return (
    form.value.name.trim().length > 0 &&
    form.value.daysGranted >= 1 &&
    (!form.value.validFrom || !form.value.validTo || form.value.validFrom <= form.value.validTo)
  )
})

const filteredExtraBenefitDays = computed(() => {
  return extraBenefitDays.value.filter((item) => {
    const matchesSearch =
      item.name.toLowerCase().includes(search.value.toLowerCase()) ||
      item.id.toString().includes(search.value)

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
const extraBenefitDays = ref<ExtraBenefitDayListDto[]>([
  {
    id: 1,
    name: 'Cumpleaños',
    daysGranted: 1,
    validFrom: null,
    validTo: null,
    status: {
      id: 2,
      description: 'Activo'
    }
  },
  {
    id: 2,
    name: 'Día de Verano',
    daysGranted: 1,
    validFrom: '2026-06-01T00:00:00',
    validTo: '2026-09-30T00:00:00',
    status: {
      id: 2,
      description: 'Activo'
    }
  },
  {
    id: 3,
    name: 'Día Puente',
    daysGranted: 2,
    validFrom: null,
    validTo: null,
    status: {
      id: 2,
      description: 'Activo'
    }
  },
   {
    id: 3,
    name: 'Día libre por Graduación',
    daysGranted: 2,
    validFrom: null,
    validTo: null,
    status: {
      id: 1,
      description: 'Inactivo'
    }
  }
])
</script>
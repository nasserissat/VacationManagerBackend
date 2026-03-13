<template>
  <div class="page-section">
    <div>
      <h1 class="page-title">Solicitudes de vacaciones</h1>
    </div>
    <div class="card mt-4">

      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar por empleado o ID"
            />
    
            <select v-model="statusFilter" class="input table-select">
              <option :value="null">Tipos los estados</option>
              <option
                v-for="option in vacationStatusOptions"
                :key="option.label"
                :value="option.value"
              >
                {{ option.label }}
              </option>
            </select>
    
            <select v-model="typeFilter" class="input table-select">
                <option :value="null">Tipos los tipos</option>
              <option
                v-for="option in vacationTypeOptions"
                :key="option.label"
                :value="option.value"
              >
                {{ option.label }}
              </option>
            </select>
            <button class="btn btn-primary shine-effect" @click="openCreateModal">
              Crear nueva solicitud 
              <fa-icon icon="plus" class="ml-1" />
            </button>
          </div>  
        </div>
        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Empleado</th>
              <th>Tipo</th>
              <th>Fecha inicio</th>
              <th>Fecha fin</th>
              <th>Total días</th>
              <th>Estado</th>
              <th>Creada en</th>
              <th class="actions-column">Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredRequests.length === 0">
              <td colspan="9" class="empty-state-cell">
                No se encontraron solicitudes.
              </td>
            </tr>

            <tr v-for="item in filteredRequests" :key="item.id">
              <td>#{{ item.id }}</td>
              <td>{{ item.employeeName }}</td>
              <td>{{ item.vacationType.description }}</td>
              <td>{{ item.startDate }}</td>
              <td>{{ item.endDate }}</td>
              <td>{{ item.totalDays }}</td>
              <td>{{ item.status.description }} </td>
              <td>{{ item.createdAt}}</td>
              <td class="actions-column">
                <div class="row-actions">
                  <button
                    class="btn btn-gray btn-sm"
                    @click="edit(item.id)"
                    :disabled="item.status.description === 'Cancelled'"
                  >
                    <fa-icon icon="pen-to-square" />
                  </button>

                  <button
                    class="btn btn-danger btn-sm"
                    @click="cancel(item.id)"
                    :disabled="item.status.description === 'Cancelled'"
                  >
                    <fa-icon icon="ban" />
                  </button>
                </div>
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
      @dismiss="closeModal">
      <div class="vacation-modal">
        <h2 class="modal-title">{{ modalTitle }}</h2>
        <p class="modal-subtitle">
          Completa la información para {{editing ? 'editar' : 'registrar'}} la solicitud de vacaciones.
        </p>  

        <div class="form-grid">
          <div class="form-group">
            <label class="form-label">Empleado</label>
            <select v-model="form.employeeId" class="input">
              <option :value=0 selected disabled>Selecciona un empleado</option>
              <option
                v-for="option in employees"
                :key="option.label"
                :value="option.value"
              >
                {{ option.label }}
              </option>
            </select>
          </div>

          <div class="form-group">
            <label class="form-label">Tipo</label>
            <select v-model="form.vacationType" class="input">
              <option :value=0 selected disabled>Selecciona</option>
              <option
                v-for="option in vacationTypeOptions"
                :key="option.label"
                :value="option.value"
              >
                {{ option.label }}
              </option>
            </select>
          </div>
            <div class="form-group">
              <label class="form-label">Fecha inicio</label>
              <input v-model="form.startDate" class="input" type="date" />
            </div>
  
            <div class="form-group">
              <label class="form-label">Fecha fin</label>
              <input v-model="form.endDate" class="input" type="date" />
            </div>
          </div>

        <div class="modal-actions">
          <button class="btn btn-danger" @click="closeModal">
            Cancelar 
            <fa-icon icon="circle-xmark" class="ml-1" />
          </button>

          <button class="btn btn-primary shine-effect" :disabled="working" @click="saveRequest">
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
import { VacationRequestListDto } from '@/models/vacation/VacationRequestListDto'
import ModalComponent from '@/components/Modal.component.vue'



const search = ref('')
const statusFilter = ref<number | null>(null)
const typeFilter = ref<number | null>(null)
const	creating = ref(false)
const	editing = ref<number | null>(null)
const	working = ref(false)

const form = ref({
  employeeId: 0,
  vacationType: 1,
  startDate: '',
  endDate: '',
  extraBenefitDayId: null as number | null
})

function openCreateModal() {
  form.value = {
    employeeId: 0,
    vacationType: 0,
    startDate: '',
    endDate: '',
    extraBenefitDayId: null
  }

  creating.value = true
  editing.value = null
}

function saveRequest() {
  console.log('guardar request', form.value)
  creating.value = false
  editing.value = null
}
async function edit(id: number) {
  // temporal mientras el api no este listo
  const request = requests.value.find(item => item.id === id)
  if (!request) return

  form.value = {
    employeeId: request.employeeId,
    vacationType: request.vacationType.id,
    startDate: request.startDate,
    endDate: request.endDate,
    extraBenefitDayId: null,
  }

  creating.value = false
  editing.value = id
}

async function cancel(id: number) {
  try {
    console.log('Cancelando', id)

    // TODO: llamar API
  } catch (error) {
    console.error(error)
  }
}
function closeModal() {
  creating.value = false
  editing.value = null
}

// Variables computadas
const isModalOpen = computed(() => creating.value || editing.value !== null)

const modalTitle = computed(() =>
  editing.value !== null
    ? `Editar solicitud: ${editing.value}`
    : 'Nueva solicitud'
)
const isEditing = computed(() => editing.value !== null)

const submitLabel = computed(() => {
  if (working.value) return isEditing.value ? 'Guardando...' : 'Creando...'
  return isEditing.value ? 'Guardar' : 'Crear'
})
const filteredRequests = computed(() => {
  return requests.value.filter((item) => {
    const matchesSearch =
      item.employeeName.toLowerCase().includes(search.value.toLowerCase()) ||
      item.id.toString().includes(search.value)

    const matchesStatus =
      statusFilter.value !== null
        ? item.status.id === Number(statusFilter.value)
        : true

    const matchesType =
      typeFilter.value !== null
        ? item.vacationType.id === Number(typeFilter.value)
        : true

    return matchesSearch && matchesStatus && matchesType
  })
})

// Temporalmente defino estas variables aquí, pero deberían venir de la API
const requests = ref<VacationRequestListDto[]>([
  {
    id: 1,
    employeeId: 101,
    employeeName: 'Nasser Emil Issa Tavares',
    vacationType: {id: 1, description: 'Vacaciones'},
    startDate: '2026-03-15',
    endDate: '2026-03-20',
    totalDays: 6,
    status: { id: 1, description: 'Pendiente' },
    createdAt: '2026-03-07T10:30:00'
  },
  {
    id: 2,
    employeeId: 102,
    employeeName: 'María Rodríguez',
    vacationType: { id: 2, description: 'Día extra' },
    startDate: '2026-03-25',
    endDate: '2026-03-25',
    totalDays: 1,
    status: { id: 2, description: 'Aprobada' },
    createdAt: '2026-03-06T09:00:00'
  },
  {
    id: 3,
    employeeId: 103,
    employeeName: 'Carlos Méndez',
    vacationType: { id: 1, description: 'Vacaciones' },
    startDate: '2026-04-01',
    endDate: '2026-04-05',
    totalDays: 5,
    status: { id: 4, description: 'Cancelada' },
    createdAt: '2026-03-05T14:15:00'
  }
])
const vacationStatusOptions = [
  { value: 1, label: 'Pendiente' },
  { value: 2, label: 'Aprobada' },
  { value: 3, label: 'Rechazada' },
  { value: 4, label: 'Cancelada' }
]
const vacationTypeOptions = [
  { value: 1, label: 'Vacaciones' },
  { value: 2, label: 'Día extra' }
]
const employees = ref([
  { value: 101, label: 'Nasser Emil Issa Tavares' },
  { value: 102, label: 'María Rodríguez' },
  { value: 103, label: 'Carlos Méndez' }
])

</script>

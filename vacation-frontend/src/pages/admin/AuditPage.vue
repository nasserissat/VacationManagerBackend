<template>
  <div class="page-section">
    <PageHeader title="Auditoría" />

    <div class="card mt-4">
      <div class="table-wrapper card-soft">
        <div class="table-toolbar">
          <div class="table-filters">
            <input
              v-model="search"
              class="input table-search"
              type="text"
              placeholder="Buscar por empleado, usuario o ID de solicitud"
            />

            <select v-model="actionFilter" class="input table-select">
              <option :value="null">Filtrar por última acción</option>
              <option
                v-for="option in actionOptions"
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

            <select v-model="vacationTypeFilter" class="input table-select">
              <option :value="null">Filtrar por tipo</option>
              <option
                v-for="option in vacationTypeOptions"
                :key="option.description"
                :value="option.id"
              >
                {{ option.description }}
              </option>
            </select>
          </div>
        </div>

        <table class="table">
          <thead>
            <tr>
              <th>Solicitud</th>
              <th>Empleado</th>
              <th>Última acción</th>
              <th>Realizado por</th>
              <th>Última fecha</th>
              <th>Tipo</th>
              <th>Inicio</th>
              <th>Fin</th>
              <th>Total días</th>
              <th>Estado actual</th>
              <th>Eventos</th>
              <th class="actions-column">Detalle</th>
            </tr>
          </thead>

          <tbody>
            <tr v-if="filteredAuditRequests.length === 0">
              <td colspan="12" class="empty-state-cell">
                No se encontraron registros de auditoría.
              </td>
            </tr>

            <tr v-for="item in filteredAuditRequests" :key="item.requestId">
              <td>#{{ item.requestId }}</td>
              <td>{{ item.employeeName }}</td>
              <td>{{ item.lastAction.description }}</td>
              <td>{{ item.lastPerformedBy }}</td>
              <td>{{ formatDateTime(item.lastPerformedAt) }}</td>
              <td>{{ item.vacationType.description }}</td>
              <td>{{ formatDate(item.startDate) }}</td>
              <td>{{ formatDate(item.endDate) }}</td>
              <td>{{ item.totalDays }}</td>
              <td>{{ item.status.description }}</td>
              <td>{{ item.eventsCount }}</td>
              <td class="actions-column">
                <button class="btn btn-gree btn-sm" @click="openHistoryModal(item.requestId)">
                  <fa-icon icon="eye" />
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

   <ModalComponent
  v-model="isHistoryModalOpen"
  @close="closeHistoryModal"
  @dismiss="closeHistoryModal"
>
  <div class="audit-detail-modal" v-if="selectedAuditDetail">
    <div class="audit-modal-header">
      <div>
        <h2 class="audit-modal-title">
          Historial de solicitud #{{ selectedAuditDetail.requestId }}
        </h2>
        <p class="audit-modal-subtitle">
          Revisión completa de acciones realizadas sobre esta solicitud.
        </p>
      </div>

      <button class="audit-close-btn" @click="closeHistoryModal">
        <fa-icon icon="xmark" />
      </button>
    </div>

    <div class="audit-summary-grid">
      <div class="audit-summary-card">
        <span class="audit-summary-label">Empleado</span>
        <span class="audit-summary-value">{{ selectedAuditDetail.employeeName }}</span>
      </div>

      <div class="audit-summary-card">
        <span class="audit-summary-label">Tipo</span>
        <span class="audit-summary-value">{{ selectedAuditDetail.vacationType.description }}</span>
      </div>

      <div class="audit-summary-card">
        <span class="audit-summary-label">Estado actual</span>
        <span class="audit-summary-value">{{ selectedAuditDetail.status.description }}</span>
      </div>

      <div class="audit-summary-card">
        <span class="audit-summary-label">Fecha inicio</span>
        <span class="audit-summary-value">{{ formatDate(selectedAuditDetail.startDate) }}</span>
      </div>

      <div class="audit-summary-card">
        <span class="audit-summary-label">Fecha fin</span>
        <span class="audit-summary-value">{{ formatDate(selectedAuditDetail.endDate) }}</span>
      </div>

      <div class="audit-summary-card">
        <span class="audit-summary-label">Total días</span>
        <span class="audit-summary-value">{{ selectedAuditDetail.totalDays }}</span>
      </div>
    </div>

    <div class="audit-history-section">
      <div class="audit-history-header">
        <h3 class="audit-history-title">Historial de eventos</h3>
        <span class="audit-history-badge">
          {{ selectedAuditDetail.history.length }} evento<span v-if="selectedAuditDetail.history.length !== 1">s</span>
        </span>
      </div>

      <div class="audit-history-table-wrapper">
        <table class="table audit-history-table">
          <thead>
            <tr>
              <th>Acción</th>
              <th>Realizado por</th>
              <th>Fecha</th>
              <th>Estado</th>
              <th>Tipo</th>
              <th>Inicio</th>
              <th>Fin</th>
              <th>Total días</th>
            </tr>
          </thead>

          <tbody>
            <tr v-for="event in selectedAuditDetail.history" :key="event.id">
              <td>{{ event.action.description }}</td>
              <td>{{ event.performedBy }}</td>
              <td>{{ formatDateTime(event.performedAt) }}</td>
              <td>{{ event.status.description }}</td>
              <td>{{ event.vacationType.description }}</td>
              <td>{{ formatDate(event.startDate) }}</td>
              <td>{{ formatDate(event.endDate) }}</td>
              <td>{{ event.totalDays }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <div class="audit-modal-footer">
      <button class="btn btn-danger" @click="closeHistoryModal">
        Cerrar
        <fa-icon icon="circle-xmark" class="ml-1" />
      </button>
    </div>
  </div>
</ModalComponent>
  </div>
</template>

<script setup lang="ts">
/*
¿Qué hace esta página?

Permite a los administradores revisar el historial completo de acciones relacionadas con
las solicitudes de vacaciones, incluyendo:

- quién solicitó vacaciones
- quién creó una solicitud a nombre de otro
- quién editó
- quién aprobó
- quién rechazó
- quién canceló
- quién asignó un día extra
- cuándo ocurrió cada acción
- sobre cuál solicitud fue
- a qué empleado afectó
- el tipo y estado de la solicitud en ese momento
*/
import { computed, ref } from 'vue'
import PageHeader from '@/components/PageHeader.component.vue'
import ModalComponent from '@/components/Modal.component.vue'
import type { Item } from '@/models/ItemModel'
import type {
  VacationAuditDetailedDto,
  VacationAuditHistoryEventDto
} from '@/models/admin/VacationAuditDetailedDto'
import type { VacationAuditListDto } from '@/models/admin/VacationAuditListDto'

const search = ref('')
const actionFilter = ref<number | null>(null)
const statusFilter = ref<number | null>(null)
const vacationTypeFilter = ref<number | null>(null)

const selectedRequestId = ref<number | null>(null)
const isHistoryModalOpen = ref(false)

const actionOptions: Item[] = [
  { id: 1, description: 'Solicitada' },
  { id: 2, description: 'Creada' },
  { id: 3, description: 'Editada' },
  { id: 4, description: 'Aprobada' },
  { id: 5, description: 'Rechazada' },
  { id: 6, description: 'Cancelada' },
  { id: 7, description: 'Asignada' }
]

const statusOptions: Item[] = [
  { id: 1, description: 'Pendiente' },
  { id: 2, description: 'Aprobada' },
  { id: 3, description: 'Rechazada' },
  { id: 4, description: 'Cancelada' }
]

const vacationTypeOptions: Item[] = [
  { id: 1, description: 'Vacaciones' },
  { id: 2, description: 'Día extra' }
]

const auditRows = ref<VacationAuditHistoryEventDto[]>([
  {
    id: 1,
    requestId: 1001,
    employeeId: 101,
    employeeName: 'Nasser Emil Issa Tavares',
    action: { id: 1, description: 'Solicitada' },
    vacationType: { id: 1, description: 'Vacaciones' },
    status: { id: 1, description: 'Pendiente' },
    startDate: '2026-04-10',
    endDate: '2026-04-15',
    totalDays: 6,
    performedBy: 'Nasser Emil Issa Tavares',
    performedAt: '2026-03-15T09:10:00'
  },
  {
    id: 2,
    requestId: 1001,
    employeeId: 101,
    employeeName: 'Nasser Emil Issa Tavares',
    action: { id: 3, description: 'Editada' },
    vacationType: { id: 1, description: 'Vacaciones' },
    status: { id: 1, description: 'Pendiente' },
    startDate: '2026-04-10',
    endDate: '2026-04-16',
    totalDays: 7,
    performedBy: 'María Rodríguez',
    performedAt: '2026-03-16T11:25:00'
  },
  {
    id: 3,
    requestId: 1001,
    employeeId: 101,
    employeeName: 'Nasser Emil Issa Tavares',
    action: { id: 4, description: 'Aprobada' },
    vacationType: { id: 1, description: 'Vacaciones' },
    status: { id: 2, description: 'Aprobada' },
    startDate: '2026-04-10',
    endDate: '2026-04-16',
    totalDays: 7,
    performedBy: 'Carlos Méndez',
    performedAt: '2026-03-17T08:40:00'
  },
  {
    id: 4,
    requestId: 1002,
    employeeId: 102,
    employeeName: 'María Rodríguez',
    action: { id: 2, description: 'Creada' },
    vacationType: { id: 2, description: 'Día extra' },
    status: { id: 1, description: 'Pendiente' },
    startDate: '2026-05-05',
    endDate: '2026-05-05',
    totalDays: 1,
    performedBy: 'superadmin',
    performedAt: '2026-03-18T10:15:00'
  },
  {
    id: 5,
    requestId: 1002,
    employeeId: 102,
    employeeName: 'María Rodríguez',
    action: { id: 6, description: 'Cancelada' },
    vacationType: { id: 2, description: 'Día extra' },
    status: { id: 4, description: 'Cancelada' },
    startDate: '2026-05-05',
    endDate: '2026-05-05',
    totalDays: 1,
    performedBy: 'María Rodríguez',
    performedAt: '2026-03-19T07:55:00'
  },
  {
    id: 6,
    requestId: 1003,
    employeeId: 103,
    employeeName: 'Carlos Méndez',
    action: { id: 7, description: 'Asignada' },
    vacationType: { id: 2, description: 'Día extra' },
    status: { id: 2, description: 'Aprobada' },
    startDate: '2026-06-01',
    endDate: '2026-06-01',
    totalDays: 1,
    performedBy: 'superadmin',
    performedAt: '2026-03-19T12:20:00'
  }
])

const groupedAuditRequests = computed<VacationAuditListDto[]>(() => {
  const groups = new Map<number, VacationAuditHistoryEventDto[]>()

  for (const row of auditRows.value) {
    if (!groups.has(row.requestId)) {
      groups.set(row.requestId, [])
    }
    groups.get(row.requestId)!.push(row)
  }

  return Array.from(groups.values()).map(group => {
    const sortedDesc = [...group].sort(
      (a, b) => new Date(b.performedAt).getTime() - new Date(a.performedAt).getTime()
    )

    const latest = sortedDesc[0]
    const first = group[0]

    return {
      requestId: first.requestId,
      employeeId: first.employeeId,
      employeeName: first.employeeName,
      vacationType: latest.vacationType,
      status: latest.status,
      startDate: latest.startDate,
      endDate: latest.endDate,
      totalDays: latest.totalDays,
      lastAction: latest.action,
      lastPerformedBy: latest.performedBy,
      lastPerformedAt: latest.performedAt,
      eventsCount: group.length
    }
  })
})

const filteredAuditRequests = computed(() => {
  return groupedAuditRequests.value.filter((item) => {
    const query = search.value.trim().toLowerCase()

    const matchesSearch =
      query.length === 0 ||
      item.employeeName.toLowerCase().includes(query) ||
      item.lastPerformedBy.toLowerCase().includes(query) ||
      item.requestId.toString().includes(query)

    const matchesAction =
      actionFilter.value !== null
        ? item.lastAction.id === Number(actionFilter.value)
        : true

    const matchesStatus =
      statusFilter.value !== null
        ? item.status.id === Number(statusFilter.value)
        : true

    const matchesVacationType =
      vacationTypeFilter.value !== null
        ? item.vacationType.id === Number(vacationTypeFilter.value)
        : true

    return matchesSearch && matchesAction && matchesStatus && matchesVacationType
  })
})

const selectedAuditDetail = computed<VacationAuditDetailedDto | null>(() => {
  if (selectedRequestId.value === null) return null

  const history = auditRows.value
    .filter(item => item.requestId === selectedRequestId.value)
    .sort((a, b) => new Date(a.performedAt).getTime() - new Date(b.performedAt).getTime())

  if (history.length === 0) return null

  const latest = [...history].sort(
    (a, b) => new Date(b.performedAt).getTime() - new Date(a.performedAt).getTime()
  )[0]

  const first = history[0]

  return {
    requestId: first.requestId,
    employeeId: first.employeeId,
    employeeName: first.employeeName,
    vacationType: latest.vacationType,
    status: latest.status,
    startDate: latest.startDate,
    endDate: latest.endDate,
    totalDays: latest.totalDays,
    history
  }
})

function openHistoryModal(requestId: number) {
  selectedRequestId.value = requestId
  isHistoryModalOpen.value = true
}

function closeHistoryModal() {
  selectedRequestId.value = null
  isHistoryModalOpen.value = false
}

function formatDate(value: string | null) {
  if (!value) return '-'
  return value.slice(0, 10)
}

function formatDateTime(value: string | null) {
  if (!value) return '-'

  const normalized = value.replace('T', ' ')
  const datePart = normalized.slice(0, 10)
  const timePart = normalized.slice(11, 16)

  return `${datePart} ${timePart}`
}
</script>
<style scoped>
.audit-detail-modal{
  width: min(800px, 92vw);
  max-height: 85vh;
  overflow-y: auto;
  overflow-x: hidden;
  background: var(--white);
  border-radius: 16px;
  padding: 24px;
  box-sizing: border-box;
}

.audit-modal-header,
.audit-summary-grid,
.audit-history-section,
.audit-modal-footer{
  width: 100%;
  max-width: 100%;
  box-sizing: border-box;
}

.audit-modal-header{
  display:flex;
  justify-content:space-between;
  align-items:flex-start;
  gap:16px;
  margin-bottom:20px;
}

.audit-modal-title{
  margin:0;
  font-size:32px;
  font-weight:700;
  color:var(--gray-900);
}

.audit-modal-subtitle{
  margin:6px 0 0;
  font-size:14px;
  color:var(--text-light);
}

.audit-close-btn{
  border:none;
  background:transparent;
  cursor:pointer;
  font-size:20px;
  color:var(--gray-500);
  padding:6px;
}

.audit-close-btn:hover{
  color:var(--gray-900);
}

.audit-summary-grid{
  display:grid;
  grid-template-columns:repeat(3, minmax(0, 1fr));
  gap:16px;
  margin-bottom:24px;
  width: 100%;
  max-width: 100%;
}

.audit-summary-card{
  display:flex;
  flex-direction:column;
  gap:6px;
  padding:14px 16px;
  background:var(--gray-50);
  border:1px solid var(--border);
  border-radius:12px;
  min-width: 0;
}

.audit-summary-label{
  font-size:13px;
  font-weight:600;
  color:var(--text-light);
}

.audit-summary-value{
  font-size:16px;
  font-weight:600;
  color:var(--text);
  line-height:1.3;
}

.audit-history-section{
  margin-top:4px;
}

.audit-history-header{
  display:flex;
  justify-content:space-between;
  align-items:center;
  gap:12px;
  margin-bottom:14px;
}

.audit-history-title{
  margin:0;
  font-size:20px;
  font-weight:700;
  color:var(--gray-900);
}

.audit-history-badge{
  display:inline-flex;
  align-items:center;
  justify-content:center;
  padding:6px 12px;
  border-radius:999px;
  background:var(--gray-100);
  color:var(--gray-700);
  font-size:13px;
  font-weight:600;
}

.audit-history-table-wrapper{
  width: 100%;
  max-width: 100%;
  overflow-x: auto;
  overflow-y: hidden;
  border:1px solid var(--border);
  border-radius:12px;
  background:var(--white);
  box-sizing: border-box;
}

.audit-history-table{
  width: 100%;
  min-width: 900px;
  margin:0;
  border-collapse: collapse;
}

.audit-history-table thead th{
  background:var(--gray-50);
  font-size:13px;
  font-weight:700;
}

.audit-history-table tbody td{
  vertical-align:top;
}

.audit-modal-footer{
  display:flex;
  justify-content:flex-end;
  margin-top:20px;
  padding-top:16px;
  border-top:1px solid var(--border);
}

@media (max-width: 900px){
  .audit-summary-grid{
    grid-template-columns:1fr 1fr;
  }
}

@media (max-width: 640px){
  .audit-detail-modal{
    width: 95vw;
    padding: 18px;
  }

  .audit-modal-title{
    font-size:24px;
  }

  .audit-summary-grid{
    grid-template-columns:1fr;
  }

  .audit-history-header{
    flex-direction:column;
    align-items:flex-start;
  }
}
</style>
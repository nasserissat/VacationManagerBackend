<template>
  <div class="page-section">
    <PageHeader title="Dashboard" />

    <div class="dashboard-grid">
      <div class="dashboard-card kpi-card">
        <span class="kpi-label">Total empleados</span>
        <strong class="kpi-value">{{ totalEmployees }}</strong>
      </div>

      <div class="dashboard-card kpi-card">
        <span class="kpi-label">Solicitudes pendientes</span>
        <strong class="kpi-value">{{ pendingRequests }}</strong>
      </div>

      <div class="dashboard-card kpi-card">
        <span class="kpi-label">Aprobadas este mes</span>
        <strong class="kpi-value">{{ approvedThisMonth }}</strong>
      </div>

      <div class="dashboard-card kpi-card">
        <span class="kpi-label">Tiempo prom. aprobación</span>
        <strong class="kpi-value">{{ averageApprovalDays }} días</strong>
      </div>
    </div>

    <div class="dashboard-grid two-columns mt-4">
      <div class="dashboard-card">
        <div class="dashboard-card-header">
          <h3>Solicitudes por estado</h3>
        </div>

        <div class="simple-bars">
          <div
            v-for="item in requestsByStatus"
            :key="item.label"
            class="simple-bar-row"
          >
            <div class="simple-bar-label">
              <span>{{ item.label }}</span>
              <strong>{{ item.value }}</strong>
            </div>

            <div class="simple-bar-track">
              <div
                class="simple-bar-fill"
                :style="{ width: `${getPercentage(item.value, maxRequestsByStatus)}%` }"
              ></div>
            </div>
          </div>
        </div>
      </div>

      <div class="dashboard-card">
        <div class="dashboard-card-header">
          <h3>Solicitudes por mes</h3>
        </div>

        <div class="simple-bars">
          <div
            v-for="item in requestsByMonth"
            :key="item.label"
            class="simple-bar-row"
          >
            <div class="simple-bar-label">
              <span>{{ item.label }}</span>
              <strong>{{ item.value }}</strong>
            </div>

            <div class="simple-bar-track">
              <div
                class="simple-bar-fill"
                :style="{ width: `${getPercentage(item.value, maxRequestsByMonth)}%` }"
              ></div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="dashboard-grid two-columns mt-4">
      <div class="dashboard-card">
        <div class="dashboard-card-header">
          <h3>Solicitudes por departamento</h3>
        </div>

        <div class="simple-bars">
          <div
            v-for="item in requestsByDepartment"
            :key="item.label"
            class="simple-bar-row"
          >
            <div class="simple-bar-label">
              <span>{{ item.label }}</span>
              <strong>{{ item.value }}</strong>
            </div>

            <div class="simple-bar-track">
              <div
                class="simple-bar-fill"
                :style="{ width: `${getPercentage(item.value, maxRequestsByDepartment)}%` }"
              ></div>
            </div>
          </div>
        </div>
      </div>

      <div class="dashboard-card">
        <div class="dashboard-card-header">
          <h3>Top empleados con menos días disponibles</h3>
        </div>

        <table class="table dashboard-table">
          <thead>
            <tr>
              <th>Empleado</th>
              <th>Disponibles</th>
              <th>Usados</th>
              <th>Restantes</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in employeesWithLessDays" :key="item.id">
              <td>{{ item.fullName }}</td>
              <td>{{ item.availableDays }}</td>
              <td>{{ item.usedDays }}</td>
              <td>{{ item.remainingDays }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <div class="dashboard-grid two-columns mt-4">
      <div class="dashboard-card">
        <div class="dashboard-card-header">
          <h3>Distribución por tipo de solicitud</h3>
        </div>

        <div class="simple-bars">
          <div
            v-for="item in requestsByType"
            :key="item.label"
            class="simple-bar-row"
          >
            <div class="simple-bar-label">
              <span>{{ item.label }}</span>
              <strong>{{ item.value }}</strong>
            </div>

            <div class="simple-bar-track">
              <div
                class="simple-bar-fill"
                :style="{ width: `${getPercentage(item.value, maxRequestsByType)}%` }"
              ></div>
            </div>
          </div>
        </div>
      </div>

      <div class="dashboard-card">
        <div class="dashboard-card-header">
          <h3>Acciones de auditoría por tipo</h3>
        </div>

        <div class="simple-bars">
          <div
            v-for="item in auditActionsSummary"
            :key="item.label"
            class="simple-bar-row"
          >
            <div class="simple-bar-label">
              <span>{{ item.label }}</span>
              <strong>{{ item.value }}</strong>
            </div>

            <div class="simple-bar-track">
              <div
                class="simple-bar-fill"
                :style="{ width: `${getPercentage(item.value, maxAuditActions)}%` }"
              ></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import PageHeader from '@/components/PageHeader.component.vue'
import type { Item } from '@/models/ItemModel'

type DashboardEmployee = {
  id: number
  fullName: string
  department: Item
  availableDays: number
  usedDays: number
  remainingDays: number
}

type DashboardRequest = {
  id: number
  employeeId: number
  employeeName: string
  department: Item
  vacationType: Item
  status: Item
  startDate: string
  endDate: string
  totalDays: number
  createdAt: string
  approvedAt?: string | null
}

type DashboardAudit = {
  id: number
  action: Item
  performedAt: string
}

const employees: DashboardEmployee[] = [
  {
    id: 1,
    fullName: 'Nasser Emil Issa Tavares',
    department: { id: 2, description: 'Tecnología' },
    availableDays: 14,
    usedDays: 10,
    remainingDays: 4
  },
  {
    id: 2,
    fullName: 'María Rodríguez',
    department: { id: 1, description: 'Recursos Humanos' },
    availableDays: 14,
    usedDays: 12,
    remainingDays: 2
  },
  {
    id: 3,
    fullName: 'Carlos Méndez',
    department: { id: 3, description: 'Finanzas' },
    availableDays: 14,
    usedDays: 14,
    remainingDays: 0
  },
  {
    id: 4,
    fullName: 'Laura Pérez',
    department: { id: 2, description: 'Tecnología' },
    availableDays: 14,
    usedDays: 5,
    remainingDays: 9
  }
]

const requests: DashboardRequest[] = [
  {
    id: 1001,
    employeeId: 1,
    employeeName: 'Nasser Emil Issa Tavares',
    department: { id: 2, description: 'Tecnología' },
    vacationType: { id: 1, description: 'Vacaciones' },
    status: { id: 2, description: 'Aprobada' },
    startDate: '2026-04-10',
    endDate: '2026-04-16',
    totalDays: 7,
    createdAt: '2026-03-10T09:00:00',
    approvedAt: '2026-03-12T10:00:00'
  },
  {
    id: 1002,
    employeeId: 2,
    employeeName: 'María Rodríguez',
    department: { id: 1, description: 'Recursos Humanos' },
    vacationType: { id: 2, description: 'Día extra' },
    status: { id: 1, description: 'Pendiente' },
    startDate: '2026-05-05',
    endDate: '2026-05-05',
    totalDays: 1,
    createdAt: '2026-03-18T10:15:00'
  },
  {
    id: 1003,
    employeeId: 3,
    employeeName: 'Carlos Méndez',
    department: { id: 3, description: 'Finanzas' },
    vacationType: { id: 1, description: 'Vacaciones' },
    status: { id: 4, description: 'Cancelada' },
    startDate: '2026-06-01',
    endDate: '2026-06-05',
    totalDays: 5,
    createdAt: '2026-03-19T11:20:00'
  },
  {
    id: 1004,
    employeeId: 4,
    employeeName: 'Laura Pérez',
    department: { id: 2, description: 'Tecnología' },
    vacationType: { id: 1, description: 'Vacaciones' },
    status: { id: 2, description: 'Aprobada' },
    startDate: '2026-02-10',
    endDate: '2026-02-12',
    totalDays: 3,
    createdAt: '2026-02-01T08:30:00',
    approvedAt: '2026-02-03T12:30:00'
  },
  {
    id: 1005,
    employeeId: 1,
    employeeName: 'Nasser Emil Issa Tavares',
    department: { id: 2, description: 'Tecnología' },
    vacationType: { id: 2, description: 'Día extra' },
    status: { id: 3, description: 'Rechazada' },
    startDate: '2026-01-15',
    endDate: '2026-01-15',
    totalDays: 1,
    createdAt: '2026-01-12T09:45:00'
  }
]

const auditRows: DashboardAudit[] = [
  { id: 1, action: { id: 1, description: 'Solicitada' }, performedAt: '2026-03-10T09:00:00' },
  { id: 2, action: { id: 3, description: 'Editada' }, performedAt: '2026-03-11T10:20:00' },
  { id: 3, action: { id: 4, description: 'Aprobada' }, performedAt: '2026-03-12T10:00:00' },
  { id: 4, action: { id: 2, description: 'Creada' }, performedAt: '2026-03-18T10:15:00' },
  { id: 5, action: { id: 6, description: 'Cancelada' }, performedAt: '2026-03-19T07:55:00' },
  { id: 6, action: { id: 7, description: 'Asignada' }, performedAt: '2026-03-19T12:20:00' }
]

const totalEmployees = computed(() => employees.length)

const pendingRequests = computed(() =>
  requests.filter(item => item.status.description === 'Pendiente').length
)

const approvedThisMonth = computed(() => {
  const currentMonth = '2026-03'
  return requests.filter(item =>
    item.status.description === 'Aprobada' &&
    item.approvedAt &&
    item.approvedAt.startsWith(currentMonth)
  ).length
})

const averageApprovalDays = computed(() => {
  const approved = requests.filter(item =>
    item.status.description === 'Aprobada' &&
    item.approvedAt
  )

  if (approved.length === 0) return 0

  const total = approved.reduce((sum, item) => {
    const created = new Date(item.createdAt).getTime()
    const approvedAt = new Date(item.approvedAt as string).getTime()
    const diffDays = (approvedAt - created) / (1000 * 60 * 60 * 24)
    return sum + diffDays
  }, 0)

  return total / approved.length
})

const requestsByStatus = computed(() => {
  const map = new Map<string, number>()

  for (const item of requests) {
    map.set(item.status.description, (map.get(item.status.description) || 0) + 1)
  }

  return Array.from(map.entries()).map(([label, value]) => ({ label, value }))
})

const requestsByMonth = computed(() => {
  const monthNames: Record<string, string> = {
    '2026-01': 'Enero',
    '2026-02': 'Febrero',
    '2026-03': 'Marzo',
    '2026-04': 'Abril',
    '2026-05': 'Mayo',
    '2026-06': 'Junio'
  }

  const map = new Map<string, number>()

  for (const item of requests) {
    const key = item.createdAt.slice(0, 7)
    map.set(key, (map.get(key) || 0) + 1)
  }

  return Array.from(map.entries()).map(([key, value]) => ({
    label: monthNames[key] || key,
    value
  }))
})

const requestsByDepartment = computed(() => {
  const map = new Map<string, number>()

  for (const item of requests) {
    map.set(item.department.description, (map.get(item.department.description) || 0) + 1)
  }

  return Array.from(map.entries()).map(([label, value]) => ({ label, value }))
})

const employeesWithLessDays = computed(() => {
  return [...employees]
    .sort((a, b) => a.remainingDays - b.remainingDays)
    .slice(0, 5)
})

const requestsByType = computed(() => {
  const map = new Map<string, number>()

  for (const item of requests) {
    map.set(item.vacationType.description, (map.get(item.vacationType.description) || 0) + 1)
  }

  return Array.from(map.entries()).map(([label, value]) => ({ label, value }))
})

const auditActionsSummary = computed(() => {
  const map = new Map<string, number>()

  for (const item of auditRows) {
    map.set(item.action.description, (map.get(item.action.description) || 0) + 1)
  }

  return Array.from(map.entries()).map(([label, value]) => ({ label, value }))
})

const maxRequestsByStatus = computed(() =>
  Math.max(...requestsByStatus.value.map(item => item.value), 1)
)

const maxRequestsByMonth = computed(() =>
  Math.max(...requestsByMonth.value.map(item => item.value), 1)
)

const maxRequestsByDepartment = computed(() =>
  Math.max(...requestsByDepartment.value.map(item => item.value), 1)
)

const maxRequestsByType = computed(() =>
  Math.max(...requestsByType.value.map(item => item.value), 1)
)

const maxAuditActions = computed(() =>
  Math.max(...auditActionsSummary.value.map(item => item.value), 1)
)

function getPercentage(value: number, max: number) {
  if (max === 0) return 0
  return (value / max) * 100
}
</script>

<style scoped>
.dashboard-grid{
  display:grid;
  grid-template-columns:repeat(4, minmax(0, 1fr));
  gap:16px;
}

.dashboard-grid.two-columns{
  grid-template-columns:repeat(2, minmax(0, 1fr));
}

.dashboard-card{
  background:var(--white);
  border:1px solid var(--border);
  border-radius:12px;
  box-shadow:var(--shadow-sm);
  padding:20px;
}

.kpi-card{
  display:flex;
  flex-direction:column;
  gap:8px;
}

.kpi-label{
  font-size:14px;
  color:var(--text-light);
  font-weight:600;
}

.kpi-value{
  font-size:32px;
  color:var(--text);
  font-weight:700;
}

.dashboard-card-header{
  margin-bottom:16px;
}

.dashboard-card-header h3{
  margin:0;
  font-size:18px;
  font-weight:700;
}

.simple-bars{
  display:flex;
  flex-direction:column;
  gap:14px;
}

.simple-bar-row{
  display:flex;
  flex-direction:column;
  gap:8px;
}

.simple-bar-label{
  display:flex;
  justify-content:space-between;
  gap:12px;
  font-size:14px;
}

.simple-bar-track{
  width:100%;
  height:10px;
  background:var(--gray-100);
  border-radius:999px;
  overflow:hidden;
}

.simple-bar-fill{
  height:100%;
  background:var(--primary);
  border-radius:999px;
}

.dashboard-table{
  margin-top:8px;
}

@media (max-width: 1100px){
  .dashboard-grid{
    grid-template-columns:repeat(2, minmax(0, 1fr));
  }
}

@media (max-width: 700px){
  .dashboard-grid,
  .dashboard-grid.two-columns{
    grid-template-columns:1fr;
  }
}
</style> necesitamos conectarlo a una libreria de graficos sofisticada, recuerda que para vue necesitamos una compatible con vue para no inventar
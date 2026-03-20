import { Item } from "../ItemModel"

export interface VacationAuditDetailedDto {
  requestId: number
  employeeId: number
  employeeName: string
  vacationType: Item
  status: Item
  startDate: string
  endDate: string
  totalDays: number
  history: VacationAuditHistoryEventDto[]
}

export interface VacationAuditHistoryEventDto {
  id: number
  requestId: number
  employeeId: number
  employeeName: string
  action: Item
  vacationType: Item
  status: Item
  startDate: string
  endDate: string
  totalDays: number
  performedBy: string
  performedAt: string
}
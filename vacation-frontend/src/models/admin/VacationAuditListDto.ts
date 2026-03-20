import type { Item } from '../ItemModel'

export interface VacationAuditListDto {
  requestId: number
  employeeId: number
  employeeName: string
  vacationType: Item
  status: Item
  startDate: string
  endDate: string
  totalDays: number
  lastAction: Item
  lastPerformedBy: string
  lastPerformedAt: string
  eventsCount: number
}
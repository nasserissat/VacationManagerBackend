import type { Item } from '../ItemModel'

export interface VacationAuditListDto {
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
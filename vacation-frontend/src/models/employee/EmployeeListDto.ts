import type { Item } from '../ItemModel'

export interface EmployeeListDto {
  id: number
  firstName: string
  lastName: string
  email?: string | null

  department: Item
  role: Item
  status: Item

  availableDays: number
  usedDays: number
  remainingDays: number
  remainingExtraBenefitDays: number
}
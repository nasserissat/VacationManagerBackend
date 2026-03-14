import type { EmployeeExtraBenefitDayDto } from './EmployeeExtraBenefitDayDto'
import type { Item } from '../ItemModel'
import { VacationRequestHistoryDto } from '../vacation/VacationRequestHistoryDto'

export interface EmployeeDetailedDto {
  id: number
  firstName: string
  lastName: string
  fullName: string
  email?: string | null

  availableDays: number
  usedDays: number
  remainingDays: number
  hasAvailableDays: boolean

  remainingExtraBenefitDays: number
  hasAvailableExtraBenefitDays: boolean

  departmentId: number
  departmentName: string

  roleId: number
  roleName: string

  status: Item

  extraBenefitDays?: EmployeeExtraBenefitDayDto[] | null
  vacationRequestHistory?: VacationRequestHistoryDto[] | null
}
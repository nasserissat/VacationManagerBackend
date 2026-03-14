import { Item } from "../ItemModel"

export interface EmployeeDataDto {
  firstName: string
  lastName: string
  email?: string | null
  availableDays: number
  usedDays: number
  departmentId: number
  roleId: number
  status: number
}

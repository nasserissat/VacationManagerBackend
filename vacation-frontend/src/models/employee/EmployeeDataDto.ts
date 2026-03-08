import { EmployeeStatusEnum } from "@/models/enums/EmployeeStatusEnum"

export interface EmployeeData {
  firstName: string
  lastName: string
  email?: string | null
  availableDays: number
  usedDays: number
  departmentId: number
  roleId: number
  status: EmployeeStatusEnum
}

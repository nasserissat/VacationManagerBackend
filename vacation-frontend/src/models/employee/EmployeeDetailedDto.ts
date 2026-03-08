import { EmployeeStatusEnum } from "@/models/enums/EmployeeStatusEnum"

export interface EmployeeDetailedOutput {
  id: number
  firstName: string
  lastName: string
  email?: string | null

  departmentId: number
  roleId: number

  availableDays: number
  usedDays: number

  status: EmployeeStatusEnum

}
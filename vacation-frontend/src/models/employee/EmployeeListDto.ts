import { EmployeeStatusEnum } from "@/models/enums/EmployeeStatusEnum"

export interface EmployeeListODto {
  id: number
  firstName: string
  lastName: string
  email?: string | null
  departmentId: number
  roleId: number
  status: EmployeeStatusEnum
}
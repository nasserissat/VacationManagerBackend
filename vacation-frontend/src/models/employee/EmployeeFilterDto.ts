export interface EmployeeFilterInput {
  search?: string
  departmentId?: number
  isActive?: boolean
  pageNumber: number
  pageSize: number
}

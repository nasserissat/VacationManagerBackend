export interface UserDataDto {
  username: string
  password?: string | null
  roleId: number
  employeeId?: number | null
  status: number
}
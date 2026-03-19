import type { PermissionListDto } from './PermissionListDto'

export interface RoleListDto {
  id: number
  position: string
  permissions: PermissionListDto[]
}
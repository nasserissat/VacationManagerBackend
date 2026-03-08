import { PermissionListDto } from "./PermissionListDto"

export interface RoleDetailedDto {
  id: number
  name: string
  permissions: PermissionListDto[]
}
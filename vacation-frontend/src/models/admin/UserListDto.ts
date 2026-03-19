import type { Item } from '../ItemModel'

export interface UserListDto {
  id: number
  username: string
  role: Item
  employee?: Item | null
  status: Item
}
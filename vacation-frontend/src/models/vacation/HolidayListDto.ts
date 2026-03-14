import { Item } from "../ItemModel"

export interface HolidayListDto {
  id: number
  name: string
  year: number
  startDate: string
  endDate: string | null
  status: Item
}
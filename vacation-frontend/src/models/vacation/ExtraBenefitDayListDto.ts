import { Item } from "../ItemModel"

export interface ExtraBenefitDayListDto {
  id: number
  name: string
  daysGranted: number
  validFrom: string | null
  validTo: string | null
  status: Item
}
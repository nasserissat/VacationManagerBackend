import { VacationStatusEnum } from "../enums/VacationStatusEnum"
import { VacationType } from "../enums/VacationTypeEnum"
import { Item } from "../ItemModel"

export interface VacationRequestListDto {
  id: number
  employeeId: number
  employeeName: string
  vacationType: Item
  startDate: string
  endDate: string
  totalDays: number
  status: Item
  createdAt: string
}
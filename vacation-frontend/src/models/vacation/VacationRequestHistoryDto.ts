export interface VacationRequestHistoryDto {
  id: number
  startDate: string
  endDate: string
  totalDays: number
  vacationType: number
  status: number
  createdBy: string
  createdAt: string
  lastModifiedBy?: string | null
  lastModifiedAt?: string | null
  approvedBy?: string | null
  approvedAt?: string | null
}
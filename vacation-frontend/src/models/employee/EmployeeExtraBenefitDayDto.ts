export interface EmployeeExtraBenefitDayDto {
  id: number
  extraBenefitDayName: string
  year: number
  usedDays: number
  remainingDays: number
  isAvailable: boolean
}
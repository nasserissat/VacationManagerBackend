export interface ExtraBenefitDayDataDto {
  name: string
  daysGranted: number
  validFrom: string | null
  validTo: string | null
  status: number
}
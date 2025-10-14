namespace vacation_backend.Application.DTOs.Employee
{
    public class EmployeeExtraBenefitDayDto
    {
        public int Id { get; set; }
        public string ExtraBenefitDayName { get; set; } = null!;
        public int Year { get; set; }
        public int UsedDays { get; set; }
        public int RemainingDays { get; set; }
        public bool IsAvailable { get; set; }

    }
}

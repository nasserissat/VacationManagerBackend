namespace vacation_backend.Domain.Entities
{
    public class EmployeeExtraBenefitDay
    {
        public int Id { get; set; }

        // Relaciones
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;

        public int ExtraBenefitDayId { get; set; }
        public virtual ExtraBenefitDay ExtraBenefitDay { get; set; } = null!;

        // Información del uso
        public int UsedDays { get; set; } = 0;
        public DateTime? AssignedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ConsumedAt { get; set; } // cuándo lo usó
        public int Year { get; set; } = DateTime.UtcNow.Year; // control anual

        public bool IsExpired => Year < DateTime.UtcNow.Year;
        public int RemainingDays => Math.Max(ExtraBenefitDay.DaysGranted - UsedDays, 0);
        public bool IsAvailable => RemainingDays > 0 && !IsExpired;
    }
}

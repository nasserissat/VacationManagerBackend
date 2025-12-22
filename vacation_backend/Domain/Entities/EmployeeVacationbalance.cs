namespace vacation_backend.Domain.Entities
{
    public class EmployeeVacationbalance
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public int Year { get; set; }

        public int AvailableDays { get; set; }
        public int UsedDays { get; set; }

        public int RemainingDays => AvailableDays - UsedDays;
        public bool HasAvailableDays => RemainingDays > 0;

        public virtual Employee Employee { get; set; } = null!;
    }
}

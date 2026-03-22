namespace vacation_backend.Domain.Entities
{
    public class VacationBalanceLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DaysChanged { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public int? VacationRequestId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual VacationRequest? VacationRequest { get; set; }
    }
}

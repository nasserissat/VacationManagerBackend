using System;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationBalanceLogDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DaysChanged { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public int? VacationRequestId { get; set; }
    }
}

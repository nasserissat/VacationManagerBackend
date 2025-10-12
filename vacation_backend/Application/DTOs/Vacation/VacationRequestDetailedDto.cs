using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationRequestDetailedDto
    {
        public int Id { get; set; }
        public VacationType VacationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public VacationStatusEnum Status { get; set; }

        // Relación principal
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!; // FirstName + LastName
        public string? EmployeeEmail { get; set; }

        // Extra benefit (si aplica)
        public int? ExtraBenefitDayId { get; set; }
        public string? ExtraBenefitDayName { get; set; }
        public int? EmployeeExtraBenefitDayId { get; set; } // cuota anual concreta

        // Auditoría
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }

        public DateTime? DecisionDate { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public int? LastModifiedById { get; set; }
        public string? LastModifiedByName { get; set; }
    }
}

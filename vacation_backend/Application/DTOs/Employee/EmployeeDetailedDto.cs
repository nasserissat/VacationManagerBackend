using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Employee
{
    public class EmployeeDetailedDto
    {
        public int Id { get; set; }

        // Información personal
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public string? Email { get; set; }

        // Días de vacaciones
        public int AvailableDays { get; set; }
        public int UsedDays { get; set; }
        public int RemainingDays { get; set; }
        public bool HasAvailableDays { get; set; }

        // Beneficios extras
        public int RemainingExtraBenefitDays { get; set; }
        public bool HasAvailableExtraBenefitDays { get; set; }

        // Información laboral
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public EmployeeStatusEnum Status { get; set; }

        // Días extra asignados
        public List<EmployeeExtraBenefitDayDto>? ExtraBenefitDays { get; set; }

        // Solicitudes de vacaciones previas
        public List<VacationRequestHistoryDto>? VacationRequestHistory { get; set; }
    }
}

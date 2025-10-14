using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Employee
{
    public class EmployeeFilterDto
    {
        // Búsqueda por nombre/apellido/email
        public string? Query { get; set; }

        // Filtros
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public EmployeeStatusEnum? Status { get; set; }

        public bool? HasAvailableDays { get; set; }            // RemainingDays > 0
        public bool? HasAvailableExtraBenefitDays { get; set; } // RemainingExtraBenefitDays > 0

        // Ordenamiento
        public string? OrderBy { get; set; } // Ej: "FirstName", "LastName", "RemainingDays"
        public bool Desc { get; set; } = false; // true = descendente

    }
}

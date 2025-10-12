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

        // Aquí paso el id según de como quiera ordenar ej: 1. Orden alfabetico, 2. Dias disponibles ascendiente, etc...
        public int? OrderBy { get; set; }

    }
}

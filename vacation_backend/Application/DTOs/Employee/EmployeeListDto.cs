namespace vacation_backend.Application.DTOs.Employee
{
    public class EmployeeListDto
    {
        public int Id { get; set; }

        // Información básica
        public string FullName { get; set; } = null!;

        // Información laboral
        public string DepartmentName { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string Status { get; set; } = null!;

        // Días de vacaciones
        public int AvailableDays { get; set; }
        public int UsedDays { get; set; }
        public int RemainingDays { get; set; }

        // Días extra de beneficio
        public int RemainingExtraBenefitDays { get; set; }

    }
}

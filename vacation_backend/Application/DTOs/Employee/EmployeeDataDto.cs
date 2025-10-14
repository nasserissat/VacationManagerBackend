using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Employee
{
    public class EmployeeDataDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public int AvailableDays { get; set; } = 14;   // Días de vacaciones disponibles
        public int UsedDays { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }

        public EmployeeStatusEnum Status { get; set; } = EmployeeStatusEnum.Active;
    }
}

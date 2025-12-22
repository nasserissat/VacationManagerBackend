using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
  
    public class Employee
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }

        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public EmployeeStatusEnum Status { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<EmployeeExtraBenefitDay>? EmployeeExtraBenefitDays { get; set; }
        public virtual ICollection<VacationRequest>? VacationRequests { get; set; }

        // Cálculos para saber si tiene días extras disponibles
        public int RemainingExtraBenefitDays =>
            EmployeeExtraBenefitDays?
                .Where(x => x.IsAvailable)
                .Sum(x => x.RemainingDays) ?? 0;

        // Cálculo para saber si tiene vacaciones disponibles

        public bool HasAvailableExtraBenefitDays => RemainingExtraBenefitDays > 0;

    }
}

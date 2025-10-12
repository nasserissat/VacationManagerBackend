using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public int AvailableDays { get; set; }
        public int UsedDays { get; set; }

        // Cálculo de días vacaciones disponibles
        public int RemainingDays => AvailableDays - UsedDays;
        public bool HasAvailableDays => RemainingDays > 0;

        // Cálculo de días de vacaciones extras disponibles
        public ICollection<EmployeeExtraBenefitDay>? EmployeeExtraBenefitDays { get; set; }
        public int RemainingExtraBenefitDays =>
            EmployeeExtraBenefitDays?
                .Where(x => x.IsAvailable)
                .Sum(x => x.RemainingDays) ?? 0;
        public bool HasAvailableExtraBenefitDays => RemainingExtraBenefitDays > 0;

        public int? EmployeeExtraBenefitDayId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public EmployeeStatusEnum Status { get; set; }



        // FK hacia Department
        public virtual Department Department { get; set; } = null!;

        // FK hacia Role
        public virtual Role Role { get; set; } = null!;

        // referencia a la asignación concreta de ese empleado en ese año
        public virtual EmployeeExtraBenefitDay? EmployeeExtraBenefitDay { get; set; }

        public ICollection<VacationRequest>? VacationRequests { get; set; }



    }
}

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
        public int RemainingDays => AvailableDays - UsedDays;
        public bool HasAvailableDays => RemainingDays > 0;
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public StatusEnum Status { get; set; }

        // FK hacia Department
        public virtual Department Department { get; set; } = null!;
        // FK hacia Role
        public virtual Role Role { get; set; } = null!;
        public ICollection<ExtraBenefitDay>? ExtraBenefitDays { get; set; }
        public ICollection<VacationRequest>? VacationRequests { get; set; }



    }
}

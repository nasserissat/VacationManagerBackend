using vacation_backend.Domain.Enums;

namespace vacation_backend.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public StatusEnum Status { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!; // Indica su posición en la gerencia

        public int? EmployeeId { get; set; }  // Puede ser null si el usuario no es empleado (ej. Admin)
        public virtual Employee? Employee { get; set; }
    }

}

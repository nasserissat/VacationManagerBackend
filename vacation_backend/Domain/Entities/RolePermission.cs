namespace vacation_backend.Domain.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        // Campos opcionales útiles a futuro
        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        public int? GrantedByUserId { get; set; }
    }
}

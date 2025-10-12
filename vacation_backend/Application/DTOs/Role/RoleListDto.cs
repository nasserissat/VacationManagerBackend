namespace vacation_backend.Application.DTOs.Role
{
    public class RoleListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PermissionsCount { get; set; }
    }
}

namespace vacation_backend.Application.DTOs.Role
{
    public class RoleDataDto
    {
        public string Name { get; set; }
        public List<int> PermissionIds { get; set; } = new();

    }
}

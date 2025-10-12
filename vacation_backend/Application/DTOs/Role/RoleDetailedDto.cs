namespace vacation_backend.Application.DTOs.Role
{
    public class RoleDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<PermissionListDto> Permissions { get; set; } = new();
    }
}

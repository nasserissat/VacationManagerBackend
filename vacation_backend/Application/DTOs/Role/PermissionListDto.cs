namespace vacation_backend.Application.DTOs.Role
{
    public class PermissionListDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string? Description { get; set; }

    }
}

namespace vacation_backend.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }
}

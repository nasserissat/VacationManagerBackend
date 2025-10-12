using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationRequestListDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public VacationType VacationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public VacationStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

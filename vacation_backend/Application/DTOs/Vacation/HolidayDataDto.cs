using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class HolidayDataDto
    {
        public string Name { get; set; } = null!;
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public StatusEnum Status { get; set; }
    }
}

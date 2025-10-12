using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class ExtraBenefitDayListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DaysGranted { get; set; } = 1;
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public StatusEnum Status { get; set; }
    }
}

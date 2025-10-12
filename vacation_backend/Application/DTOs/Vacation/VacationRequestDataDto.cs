using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationRequestDataDto
    {
        public int EmployeeId { get; set; }
        public VacationType VacationType { get; set; }  // Vacation | ExtraBenefitDay
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Auditoría de creación
        public int CreatedBy { get; set; }

        // Solo requerido si VacationType == ExtraBenefitDay
        public int? ExtraBenefitDayId { get; set; }
    }
}

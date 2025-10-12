using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationRequestFiltersDto
    {
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public VacationStatusEnum? Status { get; set; }
        public VacationType? VacationType { get; set; }

        // Rango por fechas de inicio
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
    }
}

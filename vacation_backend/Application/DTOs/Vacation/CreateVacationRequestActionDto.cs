using System.ComponentModel.DataAnnotations;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class CreateVacationRequestActionDto
    {
        [Required]
        public int VacationRequestId { get; set; }

        [Required]
        public int ActionByUserId { get; set; }

        [Required]
        public string ActionType { get; set; } = string.Empty;

        public string? Comments { get; set; }
    }
}

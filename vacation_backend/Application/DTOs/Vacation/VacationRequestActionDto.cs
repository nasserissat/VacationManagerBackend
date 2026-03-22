using System;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationRequestActionDto
    {
        public int Id { get; set; }
        public int VacationRequestId { get; set; }
        public int ActionByUserId { get; set; }
        public string ActionByUserName { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

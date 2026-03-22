using System;

namespace vacation_backend.Domain.Entities
{
    public class VacationRequestAction
    {
        public int Id { get; set; }
        public int VacationRequestId { get; set; }
        public int ActionByUserId { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual VacationRequest VacationRequest { get; set; } = null!;
        public virtual User ActionByUser { get; set; } = null!;
    }
}

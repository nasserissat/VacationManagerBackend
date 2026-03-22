using System;

namespace vacation_backend.Domain.Entities
{
    public class VacationRequestAttachment
    {
        public int Id { get; set; }
        public int VacationRequestId { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public virtual VacationRequest VacationRequest { get; set; } = null!;
    }
}

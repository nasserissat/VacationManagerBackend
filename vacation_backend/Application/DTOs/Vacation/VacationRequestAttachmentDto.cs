using System;

namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationRequestAttachmentDto
    {
        public int Id { get; set; }
        public int VacationRequestId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public string FileUrl { get; set; } = string.Empty;
    }
}

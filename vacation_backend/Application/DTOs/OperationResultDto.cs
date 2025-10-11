namespace vacation_backend.Application.DTOs
{
    public class OperationResultDto
    {
        public bool Success { get; set; }         
        public string? Message { get; set; }      
        public int? AffectedRows { get; set; }
    }
}

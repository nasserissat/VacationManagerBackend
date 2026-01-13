namespace vacation_backend.Application.DTOs.Vacation
{
    public class VacationBalanceDto
    {
        public int Year { get; set; }
        public int AvailableDays { get; set; }
        public int UsedDays { get; set; }
        public int RemainingDays { get; set; }
        public bool HasAvailableDays { get; set; }
    }

}

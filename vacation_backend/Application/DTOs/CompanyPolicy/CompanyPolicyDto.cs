namespace vacation_backend.Application.DTOs.CompanyPolicy
{
    public class CompanyPolicyDto
    {
        public int Id { get; set; }
        public bool WorksOnSaturdays { get; set; }
        public bool WorksOnSundays { get; set; }
        public int DailyWorkHours { get; set; }
    }
}

namespace vacation_backend.Domain.Entities
{
    public class CompanyPolicy
    {
        public int Id { get; set; }
        public bool WorksOnSaturdays { get; set; }
        public bool WorksOnSundays { get; set; }
        public int DailyWorkHours { get; set; }
    }
}

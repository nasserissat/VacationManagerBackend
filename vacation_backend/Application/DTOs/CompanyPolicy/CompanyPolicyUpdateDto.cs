using System.ComponentModel.DataAnnotations;

namespace vacation_backend.Application.DTOs.CompanyPolicy
{
    public class CompanyPolicyUpdateDto
    {
        [Required]
        public bool WorksOnSaturdays { get; set; }
        
        [Required]
        public bool WorksOnSundays { get; set; }
        
        [Required]
        [Range(1, 24)]
        public int DailyWorkHours { get; set; }
    }
}

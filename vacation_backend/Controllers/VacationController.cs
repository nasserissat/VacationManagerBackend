using Microsoft.AspNetCore.Mvc;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vacation_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;
        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;

        }
        [HttpGet]
        public async Task<ActionResult<VacationBalanceDto>> GetCurrentVacationBalance(int employeeId)
        {
            var result = await _vacationService.GetCurrentVacationBalanceAsync(employeeId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // GET api/<VacationRequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VacationRequestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VacationRequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

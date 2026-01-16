using Microsoft.AspNetCore.Mvc;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.Interfases.IServices;


namespace vacation_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeListDto>>> GetAllEmployees([FromQuery] EmployeeFilterDto filters)
        {
            var result = await _employeeService.GetAllEmployeesAsync(filters);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailedDto>> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateEmployee([FromBody] EmployeeDataDto data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _employeeService.CreateEmployeeAsync(data);
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<OperationResultDto>> UpdateEmployee(int id, EmployeeDataDto data)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, data);
            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return NoContent();
        }
    }
}

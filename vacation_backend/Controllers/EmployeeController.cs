using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Application.Services;

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
        public async Task<IActionResult> GetAllEmployees([FromQuery] EmployeeFilterDto filters)
        {
            var result = await _employeeService.GetAllEmployeesAsync(filters);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Application.Services;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingsService;
        public SettingController(ISettingService settingsService)
        {
            _settingsService = settingsService;

        }

        [HttpGet("departments")]
        public async Task<ActionResult<List<DepartmentListDto>>> GetAllDepartment()
        {
            var result = await _settingsService.GetAllDepartmentsAsync();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("department/{id}")]
        public async Task<ActionResult<DepartmentListDto>> GetDepartmentById(int id)
        {
            var result = await _settingsService.GetDepartmentByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("department")]
        public async Task<ActionResult<int>> CreateDepartment(DepartmentDataDto data)
        {
            var result = await _settingsService.CreateDepartmentAsync(data);
            if (result == 0)
                return BadRequest("No se pudo crear el departamento.");
            ;
            return CreatedAtAction(nameof(GetDepartmentById), new { id = result}, result);

        }
        [HttpPut("department/{id}")]
        public async Task<ActionResult<OperationResultDto>> UpdateDepartment(int id, DepartmentDataDto data)
        {
            var result = await _settingsService.UpdateDepartmentAsync(id, data);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);

        }

    }
}

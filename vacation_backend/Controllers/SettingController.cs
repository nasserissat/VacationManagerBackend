using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.DTOs.Role;
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

        [HttpDelete("department/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {

            var result = await _settingsService.DeleteDepartmentAsync(id);
            if (!result.Success)
                return BadRequest();

            return NoContent();
        }
        [HttpGet("Roles")]
        public async Task<ActionResult<List<RoleListDto>>> GetAllRoles()
        {
            var roles = await _settingsService.GetAllRolesAsync();
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }
        [HttpGet("Role/{id}")]
        public async Task<ActionResult<RoleListDto>> GetRoleById(int id)
        {
            var result = await _settingsService.GetRoleByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);

        }
        [HttpPost("Roles")]
        public async Task<ActionResult<int>> CreateRole(RoleDataDto data)
        {
            var roleId = await _settingsService.CreateRoleAsync(data);
            if (roleId == 0)
                return BadRequest("No se pudo crear el role");
            return CreatedAtAction(nameof(GetRoleById), new {id = roleId}, roleId);
        }
        [HttpGet("Permissions")]
        public async Task<ActionResult<List<PermissionListDto>>> GetAllPermissions()
        {
            var permissions = await _settingsService.GetAllPermissionsAsync();
            if (permissions == null)
                return NotFound();
            return Ok(permissions);
        }
        [HttpGet("Permissions/{id}")] 
        public async Task<ActionResult<PermissionListDto>> GetPermissionById(int id)
        {
            var permission = await _settingsService.GetPermissionByIdAsync(id);
            if (permission == null)
                return NotFound();
            return Ok(permission);
        }
        [HttpPost("Permission")]
        public async Task<ActionResult<int>> CreatePermission(PermissionDataDto data)
        {
            var permissionId = await _settingsService.CreatePermission(data);
            if (permissionId == 0)
                return BadRequest("No pudo crearse el permiso");

            return CreatedAtAction(nameof(GetPermissionById), new { id = permissionId }, permissionId);
        }
        [HttpPut("pernission/{id}")]
        public async Task<ActionResult<OperationResultDto>> UpdatePermission(int id, PermissionDataDto data)
        {
            var result = await _settingsService.UpdatePermissionAsync(id, data);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);

        }
    }
}

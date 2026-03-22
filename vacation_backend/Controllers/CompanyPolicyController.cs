using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.CompanyPolicy;
using vacation_backend.Application.Interfaces.IServices;

namespace vacation_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyPolicyController : ControllerBase
    {
        private readonly ICompanyPolicyService _companyPolicyService;

        public CompanyPolicyController(ICompanyPolicyService companyPolicyService)
        {
            _companyPolicyService = companyPolicyService;
        }

        [HttpGet]
        public async Task<ActionResult<CompanyPolicyDto>> GetPolicy()
        {
            var result = await _companyPolicyService.GetPolicyAsync();
            if (result == null) return NotFound("Policy not found.");
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResultDto>> UpdatePolicy([FromBody] CompanyPolicyUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _companyPolicyService.UpdatePolicyAsync(dto);
            return Ok(result);
        }
    }
}

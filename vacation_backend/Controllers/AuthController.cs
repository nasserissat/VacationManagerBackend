using Microsoft.AspNetCore.Mvc;
using vacation_backend.Application.DTOs.Auth;
using vacation_backend.Application.Interfases.IServices;

namespace vacation_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(loginRequest);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}

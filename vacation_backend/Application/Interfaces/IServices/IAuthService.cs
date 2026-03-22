using vacation_backend.Application.DTOs.Auth;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
    }
}

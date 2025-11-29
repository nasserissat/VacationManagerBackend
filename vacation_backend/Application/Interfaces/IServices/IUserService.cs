using System.Threading.Tasks;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Auth;
using vacation_backend.Application.DTOs.Role;
using vacation_backend.Application.DTOs.User;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface IUserService
    {
        // Login
        Task<AuthResponseDto> LoginUserAsync(LoginRequestDto loginRequest);
        Task<AuthResponseDto> RegisterUserAsync(RegisterRequestDto registerRequest);
        Task<OperationResultDto> ValidateCredentialsAsync(string username, string password);

        // Users
        Task<List<UserListDto>> GetAllUsersAsync();
        Task<UserDetailedDto> GetUserByIdAsync(int id);
        Task<OperationResultDto> DeactivateUserAsync(int userId);

    }
}

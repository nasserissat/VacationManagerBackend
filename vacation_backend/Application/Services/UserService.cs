using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Auth;
using vacation_backend.Application.DTOs.Role;
using vacation_backend.Application.DTOs.User;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Services
{
    public class UserService : IUserService
    {

        public Task<OperationResultDto> DeactivateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserListDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailedDto> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> LoginUserAsync(LoginRequestDto loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> RegisterUserAsync(RegisterRequestDto registerRequest)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> ValidateCredentialsAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}

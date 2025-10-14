using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Auth;
using vacation_backend.Application.DTOs.Role;
using vacation_backend.Application.DTOs.User;
using vacation_backend.Application.Interfases.IServices;

namespace vacation_backend.Application.Services
{
    public class UserService : IUserService
    {
        public Task<int> CreateRoleAsync(RoleDataDto roleDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeactivatePermissionAsync(int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeactivateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeleteRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PermissionListDto>> GetAllPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleListDto>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserListDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PermissionListDto>> GetPermissionsByRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDetailedDto> GetRoleByIdAsync(int roleId)
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

        public Task<OperationResultDto> UpdateRoleAsync(int roleId, RoleDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> ValidateCredentialsAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}

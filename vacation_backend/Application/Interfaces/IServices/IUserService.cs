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


        // Roles
        Task<List<RoleListDto>> GetAllRolesAsync();
        Task<RoleDetailedDto> GetRoleByIdAsync(int roleId);
        Task<int> CreateRoleAsync(RoleDataDto roleDto);
        Task<OperationResultDto> UpdateRoleAsync(int roleId, RoleDataDto data);
        Task<OperationResultDto> DeleteRoleAsync(int roleId);

        //Permissions
        Task<List<PermissionListDto>> GetAllPermissionsAsync();
        Task<List<PermissionListDto>> GetPermissionsByRoleAsync(int roleId);
        Task<OperationResultDto> DeactivatePermissionAsync(int permissionId);
    }
}

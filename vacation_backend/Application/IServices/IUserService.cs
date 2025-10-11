using System.Threading.Tasks;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Auth;
using vacation_backend.Application.DTOs.Role;
using vacation_backend.Application.DTOs.User;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.IServices
{
    public interface IUserService
    {
        // Login
        Task<AuthResponseDto> LoginUserAsync(LoginRequestDto loginRequest);
        Task<AuthResponseDto> RegisterUserAsync(RegisterRequestDto registerRequest);
        Task<bool> ValidateCredentialsAsync(string username, string password);

        // Users
        Task<List<UserListDto>> GetAllUsersAsync(FiltersDto filters); // filtrar por nombre, status, posicion etc
        Task<UserDetailedDto> GetUserByIdAsync(int id);
        Task<int> DeactivateUserAsync(int userId);


        // Roles
        Task<List<RoleListDto>> GetAllRolesAsync();
        Task<RoleDetailedDto> GetRoleByIdAsync(int roleId);
        Task<int> AssignRoleToUserAsync(int userId, int roleId);
        Task RemoveRoleFromUserAsync(int userId, int roleId);
        Task<int> CreateRoleAsync(RoleDataDto roleDto);
        Task DeleteRoleAsync(int roleId);

        //Permissions
        Task<List<PermissionListDto>> GetAllPermissionsAsync();
        Task<List<PermissionListDto>> GetPermissionsByRoleAsync(int roleId);
        Task DeactivatePermissionAsync(int permissionId);
        Task<int> AssignPermissionsToRoleAsyncAsync(List<int> permissionIds, int roleId);
        Task<int> RemovePermissionsFromRoleAsync(List<int> permissionId, int roleId);
    }
}

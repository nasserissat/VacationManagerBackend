using System.Threading.Tasks;
using vacation_backend.Application.DTOs;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.IServices
{
    public interface IUserService
    {
        // Login
        Task<AuthResponseDto> LoginUser(LoginRequestDto loginRequest);
        Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequest);
        Task<bool> ValidateCredentialsAsync(string username, string password);

        // Users
        Task<List<UserListDto>> GetAllUsers(FiltersDto filters); // filtrar por nombre, status, posicion etc
        Task<UserViewDto> GetUserById(int id);
        Task<int> DeactivateUser(int userId);


        // Roles
        Task<List<RoleListDto>> GetAllRoles();
        Task<RoleViewDto> GetRoleById(int roleId);
        Task<int> AssignRoleToUser(int userId, int roleId);
        Task RemoveRoleFromUser(int userId, int roleId);
        Task<int> CreateRole(RoleDataDto roleDto);
        Task DeleteRole(int roleId);

        //Permissions
        Task<List<PermissionListDto>> GetAllPermissions();
        Task<List<PermissionListDto>> GetPermissionsByRole(int roleId);
        Task DeactivatePermission(int permissionId);
        Task<int> AssignPermissionsToRole(List<int> permissionIds, int roleId);
        Task<int> RemovePermissionsFromRole(List<int> permissionId, int roleId);
    }
}

using vacation_backend.Application.DTOs.Department;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface ISettingRepository
    {
        #region Department
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(Department data);
        Task<bool> UpdateDepartmentAsync(Department data);
        Task<bool> DeleteDepartmentAsync(Department department);
        #endregion

        #region Permission
        Task<List<Permission>> GetAllPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
        Task<int> CreatePermissionAsync(Permission data);
        Task<bool> UpdatePermissionAsync(Permission data);
        Task<bool> DeletePermissionAsync(Permission department);
        #endregion

        #region Role
        Task<List<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<int> CreateRoleAsync(Role data);
        Task<bool> UpdateRoleAsync(Role data);
        Task<bool> DeleteRoleAsync(Role department);
        #endregion
    }
}

using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.DTOs.Vacation;
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

        #region ExtraBenefitDay
        Task<List<ExtraBenefitDay>> GetAllExtraBenefitDaysAsync();
        Task<List<EmployeeExtraBenefitDay>> GetPendingEmployeeExtraBenefitDaysAsync(int employeeId);
        Task<int> CreateExtraBenefitDayAsync(ExtraBenefitDay data);
        Task<bool> UpdateExtraBenefitDayAsync(int id, ExtraBenefitDay data);
        Task<bool> DeleteExtraBenefitDayAsync(int id);
        #endregion

        #region Holidays
        Task<List<Holiday>> GetAllHolidaysAsync();
        Task<int> CreateHolidayAsync(Holiday data);
        Task<bool> UpdateHolidayAsync(int id, Holiday data);
        Task<bool> DeleteHolidayAsync(int id);
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

        #region Role-Permissions
        // Role-Permission
        Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId);
        Task<List<Permission>> GetPermissionsByRoleIdAsync(int roleId);
        #endregion
    }
}

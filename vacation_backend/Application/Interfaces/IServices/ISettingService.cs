using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.DTOs.Role;
using vacation_backend.Application.DTOs.Vacation;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface ISettingService
    {
        #region Department
        Task<int> CreateDepartmentAsync(DepartmentDataDto data);
        Task<OperationResultDto> UpdateDepartmentAsync(int id, DepartmentDataDto data);
        Task<OperationResultDto> DeleteDepartmentAsync(int id);
        Task<List<DepartmentListDto>> GetAllDepartmentsAsync();
        Task<DepartmentListDto?> GetDepartmentByIdAsync(int id);
        #endregion

        #region ExtraBenefitDay
        Task<List<ExtraBenefitDayListDto>> GetAllExtraBenefitDaysAsync();
        Task<ExtraBenefitDayDataDto?> GetExtraBenefitDayByIdAsync(int id);
        Task<OperationResultDto> CreateExtraBenefitDayAsync(ExtraBenefitDayDataDto data);
        Task<OperationResultDto> UpdateExtraBenefitDayAsync(int id, ExtraBenefitDayDataDto data);
        Task<OperationResultDto> DeleteExtraBenefitDayAsync(int id);
        Task<List<ExtraBenefitDayDataDto>> GetAllExtraBenefitDaysAsync();
        Task<ExtraBenefitDayDataDto?> GetExtraBenefitDayByIdAsync(int id);
        #endregion

        #region Holidays
        Task<int> CreateHolidayAsync(HolidayDataDto data);
        Task<OperationResultDto> UpdateHolidayAsync(int id, HolidayDataDto data);
        Task<OperationResultDto> DeleteHolidayAsync(int id);
        Task<List<HolidayDataDto>> GetAllHolidaysAsync();
        #endregion

        #region Role
        Task<RoleListDto> GetRoleByIdAsync(int id);
        Task<List<RoleListDto>> GetAllRolesAsync();
        Task<int> CreateRoleAsync(RoleDataDto data);
        Task<OperationResultDto> UpdateRoleAsync(int id, RoleDataDto data);
        #endregion

        #region Permisisons
        Task<PermissionListDto> GetPermissionByIdAsync(int id);
        Task<List<PermissionListDto>> GetAllPermissionsAsync();
        Task<int> CreatePermission(PermissionDataDto data);
        Task<OperationResultDto> UpdatePermissionAsync(int id, PermissionDataDto data);
        #endregion

        #region Role-Permissions
        Task<OperationResultDto> AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task<OperationResultDto> RemovePermissionFromRoleAsync(int roleId, int permissionId);
        Task<List<PermissionListDto>> GetPermissionsByRoleIdAsync(int roleId);
        #endregion
    }
}

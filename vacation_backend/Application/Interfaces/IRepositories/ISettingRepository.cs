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
        Task<bool> DeleteDepartmentAsync(int id);
        #endregion
    }
}

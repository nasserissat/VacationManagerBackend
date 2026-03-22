using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface IVacationRepository
    {
        #region VacationBalance

        Task<EmployeeVacationBalance?> GetCurrentVacationBalanceAsync(int employeeId, int year);
        Task<List<EmployeeVacationBalance>> GetVacationBalanceHistoryAsync(int employeeId);
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(Department data);
        Task<bool> UpdateDepartmentAsync(Department data);
        Task<bool> DeleteDepartmentAsync(Department department);

        #endregion
    }
}

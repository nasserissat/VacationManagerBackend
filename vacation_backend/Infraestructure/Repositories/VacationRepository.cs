using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Infraestructure.Repositories
{
    public class VacationRepository : IVacationRepository
    {
        public Task<int> CreateDepartmentAsync(Department data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeVacationBalance?> GetCurrentVacationBalanceAsync(int employeeId, int year)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeVacationBalance>> GetVacationBalanceHistoryAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDepartmentAsync(Department data)
        {
            throw new NotImplementedException();
        }
    }
}

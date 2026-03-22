using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.DTOs.Vacation;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface IEmployeeService
    {
        Task<List<EmployeeListDto>> GetAllEmployeesAsync(EmployeeFilterDto filters);
        Task<EmployeeDetailedDto?> GetEmployeeByIdAsync(int employeeId);
        Task<List<VacationBalanceLogDto>> GetEmployeeBalanceLogsAsync(int employeeId);

        Task<int> CreateEmployeeAsync(EmployeeDataDto data);
        Task<OperationResultDto> UpdateEmployeeAsync(int employeeId, EmployeeDataDto data);
        Task<OperationResultDto> DeleteEmployeeAsync(int employeeId);
    }
}

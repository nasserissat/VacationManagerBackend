using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfases.IServices;

namespace vacation_backend.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<int> CreateEmployeeAsync(EmployeeDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeleteEmployeeAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployeeListDto>> GetAllEmployeesAsync(EmployeeFilterDto filters)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(filters);
            var employeeList = employees.Select(e => new EmployeeListDto
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                DepartmentName = e.Department.Name,
                RoleName = e.Role.Position,
                AvailableDays = e.AvailableDays,
                UsedDays = e.UsedDays,
                RemainingDays = e.RemainingDays,
                RemainingExtraBenefitDays = e.RemainingExtraBenefitDays,
                Status = e.Status.ToString()
            }).ToList();

            return employeeList;
        }

        public async Task<EmployeeDetailedDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDetailedDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                AvailableDays = employee.AvailableDays,
                UsedDays = employee.UsedDays,
                RemainingDays = employee.RemainingDays,
                HasAvailableDays = employee.HasAvailableDays,
                RemainingExtraBenefitDays = employee.RemainingExtraBenefitDays,
                HasAvailableExtraBenefitDays = employee.HasAvailableExtraBenefitDays,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.Name,
                RoleId = employee.RoleId,
                RoleName = employee.Role.Position,
                Status = employee.Status,
                ExtraBenefitDays = employee.EmployeeExtraBenefitDays?
                    .Select(x => new EmployeeExtraBenefitDayDto
                    {
                        Id = x.Id,
                        ExtraBenefitDayName = x.ExtraBenefitDay.Name,
                        Year = x.Year,
                        UsedDays = x.UsedDays,
                        RemainingDays = x.RemainingDays,
                        IsAvailable = x.IsAvailable
                    }).ToList(),
                VacationRequestHistory = employee.VacationRequests?
                    .Select(v => new VacationRequestHistoryDto
                    {
                        Id = v.Id,
                        StartDate = v.StartDate,
                        EndDate = v.EndDate,
                        TotalDays = v.TotalDays,
                        VacationType = v.VacationType,
                        Status = v.Status,
                        CreatedBy = v.CreatedBy.ToString(),
                        CreatedAt = v.CreatedAt,
                        LastModifiedBy = v.LastModifiedBy != null ? $"{v.LastModifiedBy.Employee?.FirstName} {v.LastModifiedBy.Employee?.LastName}" : null,
                        LastModifiedAt = v.LastModifiedAt,
                        ApprovedBy = v.ApprovedBy != null ? $"{v.ApprovedBy.Employee?.FirstName} {v.ApprovedBy.Employee?.LastName}" : null,
                        ApprovedAt = v.DecisionDate
                    }).ToList()
            };
        }


        public Task<OperationResultDto> UpdateEmployeeAsync(int employeeId, EmployeeDataDto data)
        {
            throw new NotImplementedException();
        }
    }
}

using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Domain.Entities;
using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
        public async Task<int> CreateEmployeeAsync(EmployeeDataDto data)
        {
            // 🔹 Validaciones básicas antes de crear
            if (string.IsNullOrWhiteSpace(data.FirstName))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(data.LastName))
                throw new ArgumentException("El apellido es obligatorio.");
            if(data.AvailableDays < 0 || data.AvailableDays > 28)
                throw new ArgumentException("El rango permitido de días disponibles es entre 0 y 28 días");

            var newEmployee = new Employee
            {
                FirstName = data.FirstName.Trim(),
                LastName = data.LastName.Trim(),
                Email = data.Email?.Trim(),
                AvailableDays = data.AvailableDays,
                UsedDays = data.UsedDays, 
                DepartmentId = data.DepartmentId,
                RoleId = data.RoleId,
                Status = EmployeeStatusEnum.Active
            };
            var employeeId = await _employeeRepository.CreateAsync(newEmployee);
            return employeeId;
        }

        public Task<OperationResultDto> UpdateEmployeeAsync(int employeeId, EmployeeDataDto data)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResultDto> DeleteEmployeeAsync(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}

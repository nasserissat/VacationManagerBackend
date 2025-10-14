using Microsoft.EntityFrameworkCore;
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;
using vacation_backend.Infrastructure;

namespace vacation_backend.Infraestructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly VacationDbContext _context;
        public EmployeeRepository(VacationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los empleados
        public async Task<List<Employee>> GetEmployeesAsync(EmployeeFilterDto filters)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.EmployeeExtraBenefitDays)
                    .ThenInclude(x => x.ExtraBenefitDay)
                .AsQueryable();

            // Filtros opcionales
            if (filters.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == filters.DepartmentId.Value);

            if (filters.RoleId.HasValue)
                query = query.Where(e => e.RoleId == filters.RoleId.Value);

            if (!string.IsNullOrWhiteSpace(filters.Query))
                query = query.Where(e =>
                    e.FirstName.Contains(filters.Query) ||
                    e.LastName.Contains(filters.Query) ||
                    (e.Email != null && e.Email.Contains(filters.Query)));

            if (filters.HasAvailableDays.HasValue)
            {
                if (filters.HasAvailableDays.Value)
                    query = query.Where(e => e.RemainingDays > 0);
                else
                    query = query.Where(e => e.RemainingDays <= 0);
            }

            if (filters.HasAvailableExtraBenefitDays.HasValue)
            {
                if (filters.HasAvailableExtraBenefitDays.Value)
                    query = query.Where(e => e.EmployeeExtraBenefitDays!.Any(x => x.IsAvailable));
                else
                    query = query.Where(e => !e.EmployeeExtraBenefitDays!.Any(x => x.IsAvailable));
            }

            // Ordenamiento
            if (!string.IsNullOrWhiteSpace(filters.OrderBy))
            {
                query = filters.OrderBy switch
                {
                    "FirstName" => filters.Desc ? query.OrderByDescending(e => e.FirstName) : query.OrderBy(e => e.FirstName),
                    "LastName" => filters.Desc ? query.OrderByDescending(e => e.LastName) : query.OrderBy(e => e.LastName),
                    "RemainingDays" => filters.Desc ? query.OrderByDescending(e => e.RemainingDays) : query.OrderBy(e => e.RemainingDays),
                    _ => query.OrderBy(e => e.Id)
                };
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }

            return await query.ToListAsync();
        }

        // Obtener detalle por ID
        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Include(e => e.EmployeeExtraBenefitDays)
                    .ThenInclude(x => x.ExtraBenefitDay)
                .Include(e => e.VacationRequests)
                    .ThenInclude(v => v.ApprovedBy)
                .Include(e => e.VacationRequests)
                    .ThenInclude(v => v.LastModifiedBy)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        // Crear un nuevo empleado
        public async Task<int> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        // Actualizar un empleado existente
        public async Task<int> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return await _context.SaveChangesAsync();
        }

        // Eliminar un empleado        
        public async Task<int> DeleteAsync(int employeeId)
        {
            var existing = await _context.Employees.FindAsync(employeeId);
            if (existing == null)
                return 0;

            _context.Employees.Remove(existing);
            return await _context.SaveChangesAsync();
        }
    }
}
using vacation_backend.Application.DTOs.Employee;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface IEmployeeRepository
    {
        // Obtener lista filtrada de empleados
        Task<List<Employee>> GetEmployeesAsync(EmployeeFilterDto filters);

        // Obtener un empleado por su ID
        Task<Employee?> GetByIdAsync(int employeeId);

        // Crear un nuevo empleado
        Task<int> CreateAsync(Employee employee);

        // Actualizar un empleado existente
        Task<int> UpdateAsync(Employee employee);

        // Eliminar un empleado
        Task<int> DeleteAsync(int employeeId);
    }
}
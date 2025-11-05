using Microsoft.EntityFrameworkCore;
using System;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;
using vacation_backend.Infrastructure;

namespace vacation_backend.Infraestructure.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly VacationDbContext _context;

        public SettingRepository(VacationDbContext context)
        {
            _context = context;

        }
        public async Task<int> CreateDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }

        public Task<bool> DeleteDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            return _context.Departments.ToListAsync();
        }

        public Task<Department?> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDepartmentAsync(Department data)
        {
            throw new NotImplementedException();
        }
    }
}

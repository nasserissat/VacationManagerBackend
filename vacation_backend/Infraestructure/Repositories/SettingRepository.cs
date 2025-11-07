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

        public async Task<bool> DeleteDepartmentAsync(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            return _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }
        public async Task<bool> UpdateDepartmentAsync(Department data)
        {
            var existingDepartment = await _context.Departments.FindAsync(data.Id);
            if (existingDepartment == null)
                return false;
            existingDepartment.Name = data.Name;
            return await _context.SaveChangesAsync() > 0;


        }
    }
}

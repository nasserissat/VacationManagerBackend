using Microsoft.EntityFrameworkCore;
using System;
using System.Security;
using vacation_backend.Application.DTOs;
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

        #region Departments
        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            return _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }
        public async Task<int> CreateDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }
        public async Task<bool> UpdateDepartmentAsync(Department data)
        {
            var existingDepartment = await _context.Departments.FindAsync(data.Id);
            if (existingDepartment == null)
                return false;
            existingDepartment.Name = data.Name;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteDepartmentAsync(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region ExtraBenefitDays
        public Task<List<ExtraBenefitDay>> GetAllExtraBenefitDaysAsync()
        {
            return _context.ExtraBenefitDays.ToListAsync();
        }
        public async Task<List<EmployeeExtraBenefitDay>> GetPendingEmployeeExtraBenefitDaysAsync(int employeeId)
        {
            var currentYear = DateTime.UtcNow.Year;

            return await _context.EmployeeExtraBenefitDays
                .Include(e => e.ExtraBenefitDay)
                .Where(e =>
                    e.EmployeeId == employeeId &&
                    e.Year == currentYear &&
                    e.IsAvailable)
                .ToListAsync();
        }

        public async Task<int> CreateExtraBenefitDayAsync(ExtraBenefitDay data)
        {
            _context.ExtraBenefitDays.Add(data);
            await _context.SaveChangesAsync();
            return data.Id;
        }
        public async Task<bool> UpdateExtraBenefitDayAsync(int id, ExtraBenefitDay data)
        {
            var existingExtraBenefitDay = await _context.ExtraBenefitDays.FindAsync(data.Id);
            if (existingExtraBenefitDay == null)
                return false;
            existingExtraBenefitDay.Name = data.Name;
            existingExtraBenefitDay.DaysGranted = data.DaysGranted;
            existingExtraBenefitDay.ValidFrom = data.ValidFrom;
            existingExtraBenefitDay.ValidTo = data.ValidTo;
            existingExtraBenefitDay.Status = data.Status;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteExtraBenefitDayAsync(int id)
        {
            var existing = await _context.ExtraBenefitDays.FindAsync(id);

            if (existing == null)
                return false;

            _context.ExtraBenefitDays.Remove(existing);

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Holidays
        public Task<List<Holiday>> GetAllHolidaysAsync()
        {
            return _context.Holidays.ToListAsync();
        }
        public async Task<int> CreateHolidayAsync(Holiday data)
        {
            _context.Holidays.Add(data);
            await _context.SaveChangesAsync();
            return data.Id;
        }
        public async Task<bool> UpdateHolidayAsync(int id, Holiday data)
        {
            var existingHoliday = await _context.Holidays.FindAsync(data.Id);
            if (existingHoliday == null)
                return false;

            existingHoliday.Name = data.Name;
            existingHoliday.Year = data.Year;
            existingHoliday.StartDate = data.StartDate;
            existingHoliday.EndDate = data.EndDate;
            existingHoliday.Status = data.Status;

            _context.Holidays.Update(existingHoliday);
            return await _context.SaveChangesAsync() > 0;
        }
        public async  Task<bool> DeleteHolidayAsync(int id)
        {
            var existing = await _context.Holidays.FindAsync(id);

            if (existing == null)
                return false;

            _context.Holidays.Remove(existing);

            return await _context.SaveChangesAsync() > 0;

        }

        #endregion 

        #region Role-Permissions
        public async Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId)
        {
            var exists = await _context.RolePermissions
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);
            if (exists) return true;

            _context.RolePermissions.Add(new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            });
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            var link = await _context.RolePermissions
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);
            if (link == null) return false;

            _context.RolePermissions.Remove(link);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Permission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(x => x.RoleId == roleId)
                .Select(x => x.Permission)
                .ToListAsync();
        }
        #endregion

        #region Permission
        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<int> CreatePermissionAsync(Permission data)
        {
            _context.Permissions.Add(data);
            await _context.SaveChangesAsync();
            return data.Id;
        }

        public async Task<bool> UpdatePermissionAsync(Permission data)
        {
            var existingPermissions = await _context.Permissions.FindAsync(data.Id);
            if (existingPermissions == null)
                return false;
            existingPermissions.Key = data.Key;
            existingPermissions.Description = data.Description;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePermissionAsync(Permission permission)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Roles
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<int> CreateRoleAsync(Role data)
        {
            _context.Roles.Add(data);
            await _context.SaveChangesAsync();
            return data.Id;
        }

        public async Task<bool> UpdateRoleAsync(Role data)
        {
            var existingRoles = await _context.Roles.FindAsync(data.Id);
            if (existingRoles == null)
                return false;
            existingRoles.Position = data.Position;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoleAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}

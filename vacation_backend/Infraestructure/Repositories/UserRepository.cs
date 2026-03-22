using Microsoft.EntityFrameworkCore;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;
using vacation_backend.Infrastructure;

namespace vacation_backend.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VacationDbContext _context;

        public UserRepository(VacationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Employee)
                .ToListAsync();
        }

        public async Task<int> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null)
                return false;

            existing.Username = user.Username;
            existing.PasswordHash = user.PasswordHash;
            existing.RoleId = user.RoleId;
            existing.Status = user.Status;
            existing.EmployeeId = user.EmployeeId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

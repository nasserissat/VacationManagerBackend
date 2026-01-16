using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<int> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
    }
}

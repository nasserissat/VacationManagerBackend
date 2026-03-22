using System.Collections.Generic;
using System.Threading.Tasks;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface IVacationBalanceLogRepository
    {
        Task<List<VacationBalanceLog>> GetLogsByEmployeeIdAsync(int employeeId);
        Task<int> CreateLogAsync(VacationBalanceLog log);
    }
}

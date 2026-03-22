using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Infrastructure.Repositories
{
    public class VacationBalanceLogRepository : IVacationBalanceLogRepository
    {
        private readonly VacationDbContext _context;
        public VacationBalanceLogRepository(VacationDbContext context) { _context = context; }

        public async Task<List<VacationBalanceLog>> GetLogsByEmployeeIdAsync(int employeeId)
        {
            return await _context.VacationBalanceLogs
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync();
        }

        public async Task<int> CreateLogAsync(VacationBalanceLog log)
        {
            _context.VacationBalanceLogs.Add(log);
            await _context.SaveChangesAsync();
            return log.Id;
        }
    }
}

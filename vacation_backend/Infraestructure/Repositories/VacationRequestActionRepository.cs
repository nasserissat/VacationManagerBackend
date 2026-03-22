using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Infrastructure.Repositories
{
    public class VacationRequestActionRepository : IVacationRequestActionRepository
    {
        private readonly VacationDbContext _context;
        public VacationRequestActionRepository(VacationDbContext context) { _context = context; }

        public async Task<List<VacationRequestAction>> GetActionsByRequestIdAsync(int vacationRequestId)
        {
            return await _context.VacationRequestActions
                .Include(x => x.ActionByUser)
                .Where(x => x.VacationRequestId == vacationRequestId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> CreateActionAsync(VacationRequestAction action)
        {
            _context.VacationRequestActions.Add(action);
            await _context.SaveChangesAsync();
            return action.Id;
        }
    }
}

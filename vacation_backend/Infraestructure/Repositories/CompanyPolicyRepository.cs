using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Infrastructure.Repositories
{
    public class CompanyPolicyRepository : ICompanyPolicyRepository
    {
        private readonly VacationDbContext _context;
        public CompanyPolicyRepository(VacationDbContext context) { _context = context; }

        public async Task<CompanyPolicy?> GetPolicyAsync()
        {
            return await _context.CompanyPolicies.FirstOrDefaultAsync();
        }

        public async Task UpdatePolicyAsync(CompanyPolicy policy)
        {
            if (policy.Id == 0)
                _context.CompanyPolicies.Add(policy);
            else
                _context.CompanyPolicies.Update(policy);
            
            await _context.SaveChangesAsync();
        }
    }
}

using System.Threading.Tasks;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface ICompanyPolicyRepository
    {
        Task<CompanyPolicy?> GetPolicyAsync();
        Task UpdatePolicyAsync(CompanyPolicy policy);
    }
}

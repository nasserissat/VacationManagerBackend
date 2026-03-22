using System.Threading.Tasks;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.CompanyPolicy;

namespace vacation_backend.Application.Interfaces.IServices
{
    public interface ICompanyPolicyService
    {
        Task<CompanyPolicyDto?> GetPolicyAsync();
        Task<OperationResultDto> UpdatePolicyAsync(CompanyPolicyUpdateDto dto);
    }
}

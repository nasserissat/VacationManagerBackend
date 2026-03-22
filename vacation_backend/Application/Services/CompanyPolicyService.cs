using System.Threading.Tasks;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.CompanyPolicy;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfaces.IServices;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Services
{
    public class CompanyPolicyService : ICompanyPolicyService
    {
        private readonly ICompanyPolicyRepository _repository;

        public CompanyPolicyService(ICompanyPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyPolicyDto?> GetPolicyAsync()
        {
            var policy = await _repository.GetPolicyAsync();
            if (policy == null) return null;

            return new CompanyPolicyDto
            {
                Id = policy.Id,
                WorksOnSaturdays = policy.WorksOnSaturdays,
                WorksOnSundays = policy.WorksOnSundays,
                DailyWorkHours = policy.DailyWorkHours
            };
        }

        public async Task<OperationResultDto> UpdatePolicyAsync(CompanyPolicyUpdateDto dto)
        {
            var policy = await _repository.GetPolicyAsync();
            if (policy == null)
            {
                policy = new CompanyPolicy();
            }

            policy.WorksOnSaturdays = dto.WorksOnSaturdays;
            policy.WorksOnSundays = dto.WorksOnSundays;
            policy.DailyWorkHours = dto.DailyWorkHours;

            await _repository.UpdatePolicyAsync(policy);

            return new OperationResultDto { Success = true, Message = "Company policy updated successfully" };
        }
    }
}

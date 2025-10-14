using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfases.IServices;

namespace vacation_backend.Application.Services
{
    public class VacationService : IVacationService
    {
        public Task<OperationResultDto> ApproveVacationRequestAsync(int id, int approvedById)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> CancelVacationRequestAsync(int id, int canceledById, string? reason)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateVacationRequest(VacationRequestDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<List<VacationRequestListDto>> GetAllVacationRequestsAsync(VacationRequestFiltersDto filters)
        {
            throw new NotImplementedException();
        }

        public Task<VacationRequestDetailedDto?> GetVacationRequestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> RejectVacationRequestAsync(int id, int rejectedById, string? reason)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> UpdateVacationRequestDatesAsync(int id, DateTime startDate, DateTime endDate, int modifiedById)
        {
            throw new NotImplementedException();
        }
    }
}

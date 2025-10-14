using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Vacation;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface IVacationService
    {
        //GET
        Task<VacationRequestDetailedDto?> GetVacationRequestByIdAsync(int id);
        Task<List<VacationRequestListDto>> GetAllVacationRequestsAsync(VacationRequestFiltersDto filters);

        // POST
        Task<int> CreateVacationRequest(VacationRequestDataDto data);

        // PUT
        Task<OperationResultDto> UpdateVacationRequestDatesAsync(int id, DateTime startDate, DateTime endDate, int modifiedById);

        // STATE CHANGES
        Task<OperationResultDto> ApproveVacationRequestAsync(int id, int approvedById);
        Task<OperationResultDto> RejectVacationRequestAsync(int id, int rejectedById, string? reason);
        Task<OperationResultDto> CancelVacationRequestAsync(int id, int canceledById, string? reason);



    }
}

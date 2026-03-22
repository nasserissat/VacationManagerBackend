using Microsoft.EntityFrameworkCore;
using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfases.IServices;

namespace vacation_backend.Application.Services
{
    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IVacationRequestActionRepository _actionRepository;
        private readonly IVacationRequestAttachmentRepository _attachmentRepository;

        public VacationService(
            IVacationRepository vacationRepository,
            IVacationRequestActionRepository actionRepository,
            IVacationRequestAttachmentRepository attachmentRepository)
        {
            _vacationRepository = vacationRepository;
            _actionRepository = actionRepository;
            _attachmentRepository = attachmentRepository;
        }

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

        public Task<VacationBalanceDto> GetCurrentVacationBalanceAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VacationBalanceDto>> GetVacationBalanceHistoryAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<VacationRequestDetailedDto?> GetVacationRequestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VacationRequestListDto>> GetVacationRequestsByEmployeeAsync(int employeeId, VacationRequestFiltersDto filters)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> HasAvailableVacationDaysAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasOverlappingVacationAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> InitializeVacationBalanceForYearAsync(int employeeId, int year)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> RejectVacationRequestAsync(int id, int rejectedById, string? reason)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> UpdateVacationRequestDatesAsync(int id, System.DateTime startDate, System.DateTime endDate, int modifiedById)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OperationResultDto> AddVacationRequestActionAsync(CreateVacationRequestActionDto dto)
        {
            var action = new vacation_backend.Domain.Entities.VacationRequestAction
            {
                VacationRequestId = dto.VacationRequestId,
                ActionByUserId = dto.ActionByUserId,
                ActionType = dto.ActionType,
                Comments = dto.Comments,
                CreatedAt = System.DateTime.UtcNow
            };
            await _actionRepository.CreateActionAsync(action);
            return new OperationResultDto { Success = true, Message = "Action added successfully" };
        }

        public async Task<int> AddVacationRequestAttachmentAsync(int vacationRequestId, string fileName, string fileUrl)
        {
            var attachment = new vacation_backend.Domain.Entities.VacationRequestAttachment
            {
                VacationRequestId = vacationRequestId,
                FileName = fileName,
                FilePath = fileUrl,
                UploadedAt = System.DateTime.UtcNow
            };
            return await _attachmentRepository.AddAttachmentAsync(attachment);
        }
    }
}

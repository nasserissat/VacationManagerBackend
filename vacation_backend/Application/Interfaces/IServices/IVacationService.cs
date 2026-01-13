using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Vacation;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface IVacationService
    {
        #region VacationBalance
        // Obtiene el balance actual de vacaciones para un empleado específico.
        Task<VacationBalanceDto> GetCurrentVacationBalanceAsync(int employeeId);
        // Obtiene el historial de balances de vacaciones para un empleado específico.
        Task<List<VacationBalanceDto>> GetVacationBalanceHistoryAsync(int employeeId);
        // Verifica si un empleado tiene días de vacaciones disponibles.
        Task<OperationResultDto> HasAvailableVacationDaysAsync(int employeeId);
        // Inicializa el balance de vacaciones para un empleado en un año específico.
        Task<OperationResultDto> InitializeVacationBalanceForYearAsync(int employeeId, int year);

        #endregion
     

        #region VacationRequest

        // Obtiene una solicitud de vacaciones por su ID.
        Task<VacationRequestDetailedDto?> GetVacationRequestByIdAsync(int id);
        // Obtiene todas las solicitudes de vacaciones con filtros opcionales.
        Task<List<VacationRequestListDto>> GetAllVacationRequestsAsync(VacationRequestFiltersDto filters);
        // Obtiene las solicitudes de vacaciones de un empleado específico con filtros opcionales.
        Task<List<VacationRequestListDto>> GetVacationRequestsByEmployeeAsync(int employeeId, VacationRequestFiltersDto filters);
        // Crea una nueva solicitud de vacaciones.
        Task<int> CreateVacationRequest(VacationRequestDataDto data);
        // Actualiza las fechas de una solicitud de vacaciones existente.
        Task<OperationResultDto> UpdateVacationRequestDatesAsync(int id, DateTime startDate, DateTime endDate, int modifiedById);
        Task<bool> HasOverlappingVacationAsync(int employeeId, DateTime startDate, DateTime endDate);

        // STATE CHANGES:

        // Aprueba una solicitud de vacaciones.
        Task<OperationResultDto> ApproveVacationRequestAsync(int id, int approvedById);
        // Rechaza una solicitud de vacaciones.
        Task<OperationResultDto> RejectVacationRequestAsync(int id, int rejectedById, string? reason);
        // Cancela una solicitud de vacaciones.
        Task<OperationResultDto> CancelVacationRequestAsync(int id, int canceledById, string? reason);
        #endregion


    }
}
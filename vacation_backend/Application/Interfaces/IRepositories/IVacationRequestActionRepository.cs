using System.Collections.Generic;
using System.Threading.Tasks;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface IVacationRequestActionRepository
    {
        Task<List<VacationRequestAction>> GetActionsByRequestIdAsync(int vacationRequestId);
        Task<int> CreateActionAsync(VacationRequestAction action);
    }
}

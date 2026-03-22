using System.Collections.Generic;
using System.Threading.Tasks;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Interfaces.IRepositories
{
    public interface IVacationRequestAttachmentRepository
    {
        Task<List<VacationRequestAttachment>> GetAttachmentsByRequestIdAsync(int vacationRequestId);
        Task<int> AddAttachmentAsync(VacationRequestAttachment attachment);
    }
}

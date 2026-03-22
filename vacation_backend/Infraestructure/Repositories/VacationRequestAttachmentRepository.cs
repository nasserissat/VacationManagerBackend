using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Infrastructure.Repositories
{
    public class VacationRequestAttachmentRepository : IVacationRequestAttachmentRepository
    {
        private readonly VacationDbContext _context;
        public VacationRequestAttachmentRepository(VacationDbContext context) { _context = context; }

        public async Task<List<VacationRequestAttachment>> GetAttachmentsByRequestIdAsync(int vacationRequestId)
        {
            return await _context.VacationRequestAttachments
                .Where(x => x.VacationRequestId == vacationRequestId)
                .OrderByDescending(x => x.UploadedAt)
                .ToListAsync();
        }

        public async Task<int> AddAttachmentAsync(VacationRequestAttachment attachment)
        {
            _context.VacationRequestAttachments.Add(attachment);
            await _context.SaveChangesAsync();
            return attachment.Id;
        }
    }
}

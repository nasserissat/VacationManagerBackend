using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using vacation_backend.Domain.Entities;
using vacation_backend.Infrastructure;

namespace VacationSeeder
{
    public static class DatabaseSeederNewEntities
    {
        public static async Task SeedNewEntitiesAsync(VacationDbContext context)
        {
            Console.WriteLine();
            Console.WriteLine("🌱 Seeding Nuevas Características...");

            // 1. Company Policy
            if (!await context.CompanyPolicies.AnyAsync())
            {
                await context.CompanyPolicies.AddAsync(new CompanyPolicy
                {
                    WorksOnSaturdays = false,
                    WorksOnSundays = false,
                    DailyWorkHours = 8
                });
                Console.WriteLine("   ✓ Política de Empresa creada");
            }

            var employees = await context.Employees.Take(5).ToListAsync();
            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Username == "admin");
            var requests = await context.VacationRequests.Take(3).ToListAsync();

            if (adminUser != null && employees.Any())
            {
                // 2. Vacation Balance Logs
                if (!await context.VacationBalanceLogs.AnyAsync())
                {
                    foreach (var emp in employees)
                    {
                        await context.VacationBalanceLogs.AddAsync(new VacationBalanceLog
                        {
                            EmployeeId = emp.Id,
                            DaysChanged = emp.AvailableDays,
                            Reason = "Asignación inicial (Seeder)",
                            TransactionDate = DateTime.UtcNow
                        });
                    }
                    Console.WriteLine($"   ✓ {employees.Count} Historiales de Balance (Logs) creados");
                }
            }

            if (adminUser != null && requests.Any())
            {
                // 3. Vacation Request Actions
                if (!await context.VacationRequestActions.AnyAsync())
                {
                    foreach (var req in requests)
                    {
                        await context.VacationRequestActions.AddAsync(new VacationRequestAction
                        {
                            VacationRequestId = req.Id,
                            ActionByUserId = adminUser.Id,
                            ActionType = "Comentario",
                            Comments = "Esto es un comentario generado automáticamente por el seeder",
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                    Console.WriteLine($"   ✓ {requests.Count} Acciones/Comentarios en Solicitudes creados");
                }

                // 4. Vacation Request Attachments
                if (!await context.VacationRequestAttachments.AnyAsync())
                {
                    await context.VacationRequestAttachments.AddAsync(new VacationRequestAttachment
                    {
                        VacationRequestId = requests.First().Id,
                        FileName = "certificado_medico.pdf",
                        FilePath = "/uploads/certificado_medico.pdf",
                        UploadedAt = DateTime.UtcNow
                    });
                    Console.WriteLine("   ✓ 1 Archivo Adjunto de Solicitud (Attachment) creado");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using vacation_backend.Domain.Entities;
using vacation_backend.Domain.Enums;
using vacation_backend.Infrastructure;

namespace VacationSeeder
{
    public class DatabaseSeeder
    {
        private readonly VacationDbContext _context;

        public DatabaseSeeder(VacationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            Console.WriteLine("?? Iniciando seed de datos...");

            // Verificar si ya hay datos
            if (await _context.Departments.AnyAsync())
            {
                Console.WriteLine("??  La base de datos ya contiene datos. Saltando seed.");
                return;
            }

            await SeedDepartments();
            await SeedRoles();
            await SeedPermissions();
            await SeedRolePermissions();
            await SeedExtraBenefitDays();
            await SeedHolidays();
            await SeedEmployees();
            await SeedUsers();
            await SeedEmployeeExtraBenefitDays();
            await SeedVacationRequests();

            Console.WriteLine("? Seed completado exitosamente!");
        }

        private async Task SeedDepartments()
        {
            Console.WriteLine("?? Seeding Departments...");
            var departments = new[]
            {
                new Department { Name = "Recursos Humanos" },
                new Department { Name = "Tecnología" },
                new Department { Name = "Finanzas" },
                new Department { Name = "Marketing" },
                new Department { Name = "Ventas" },
                new Department { Name = "Operaciones" }
            };

            await _context.Departments.AddRangeAsync(departments);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {departments.Length} departamentos creados");
        }

        private async Task SeedRoles()
        {
            Console.WriteLine("?? Seeding Roles...");
            var roles = new[]
            {
                new Role { Position = "Gerente General" },
                new Role { Position = "Gerente de Departamento" },
                new Role { Position = "Supervisor" },
                new Role { Position = "Empleado" },
                new Role { Position = "Pasante" }
            };

            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {roles.Length} roles creados");
        }

        private async Task SeedPermissions()
        {
            Console.WriteLine("?? Seeding Permissions...");
            var permissions = new[]
            {
                new Permission { Key = "VIEW_EMPLOYEES", Description = "Ver lista de empleados" },
                new Permission { Key = "CREATE_EMPLOYEE", Description = "Crear nuevos empleados" },
                new Permission { Key = "EDIT_EMPLOYEE", Description = "Editar empleados" },
                new Permission { Key = "DELETE_EMPLOYEE", Description = "Eliminar empleados" },
                new Permission { Key = "VIEW_VACATIONS", Description = "Ver solicitudes de vacaciones" },
                new Permission { Key = "CREATE_VACATION", Description = "Crear solicitudes de vacaciones" },
                new Permission { Key = "APPROVE_VACATION", Description = "Aprobar solicitudes de vacaciones" },
                new Permission { Key = "REJECT_VACATION", Description = "Rechazar solicitudes de vacaciones" },
                new Permission { Key = "MANAGE_SETTINGS", Description = "Gestionar configuraciones del sistema" },
                new Permission { Key = "VIEW_REPORTS", Description = "Ver reportes" }
            };

            await _context.Permissions.AddRangeAsync(permissions);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {permissions.Length} permisos creados");
        }

        private async Task SeedRolePermissions()
        {
            Console.WriteLine("?? Seeding Role-Permissions...");
            var roles = await _context.Roles.ToListAsync();
            var permissions = await _context.Permissions.ToListAsync();

            var rolePermissions = new List<RolePermission>();

            // Gerente General - Todos los permisos
            var gerenteGeneral = roles.First(r => r.Position == "Gerente General");
            rolePermissions.AddRange(permissions.Select(p => new RolePermission
            {
                RoleId = gerenteGeneral.Id,
                PermissionId = p.Id
            }));

            // Gerente de Departamento - Permisos de gestión
            var gerenteDepartamento = roles.First(r => r.Position == "Gerente de Departamento");
            var managementPermissions = permissions.Where(p =>
                p.Key.Contains("VIEW") || p.Key.Contains("APPROVE") || p.Key.Contains("REJECT")).ToList();
            rolePermissions.AddRange(managementPermissions.Select(p => new RolePermission
            {
                RoleId = gerenteDepartamento.Id,
                PermissionId = p.Id
            }));

            // Supervisor - Permisos de visualización y aprobación
            var supervisor = roles.First(r => r.Position == "Supervisor");
            var supervisorPermissions = permissions.Where(p =>
                p.Key.Contains("VIEW") || p.Key == "APPROVE_VACATION").ToList();
            rolePermissions.AddRange(supervisorPermissions.Select(p => new RolePermission
            {
                RoleId = supervisor.Id,
                PermissionId = p.Id
            }));

            // Empleado - Solo visualización y crear sus propias vacaciones
            var empleado = roles.First(r => r.Position == "Empleado");
            var employeePermissions = permissions.Where(p =>
                p.Key == "VIEW_VACATIONS" || p.Key == "CREATE_VACATION").ToList();
            rolePermissions.AddRange(employeePermissions.Select(p => new RolePermission
            {
                RoleId = empleado.Id,
                PermissionId = p.Id
            }));

            await _context.RolePermissions.AddRangeAsync(rolePermissions);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {rolePermissions.Count} relaciones role-permission creadas");
        }

        private async Task SeedExtraBenefitDays()
        {
            Console.WriteLine("?? Seeding Extra Benefit Days...");
            var extraBenefits = new[]
            {
                new ExtraBenefitDay
                {
                    Name = "Día de Cumpleaños",
                    DaysGranted = 1,
                    ValidFrom = new DateTime(DateTime.Now.Year, 1, 1),
                    ValidTo = new DateTime(DateTime.Now.Year, 12, 31),
                    Status = StatusEnum.Active
                },
                new ExtraBenefitDay
                {
                    Name = "Día de Verano",
                    DaysGranted = 1,
                    ValidFrom = new DateTime(DateTime.Now.Year, 6, 1),
                    ValidTo = new DateTime(DateTime.Now.Year, 8, 31),
                    Status = StatusEnum.Active
                },
                new ExtraBenefitDay
                {
                    Name = "Día Puente",
                    DaysGranted = 1,
                    ValidFrom = new DateTime(DateTime.Now.Year, 1, 1),
                    ValidTo = new DateTime(DateTime.Now.Year, 12, 31),
                    Status = StatusEnum.Active
                },
                new ExtraBenefitDay
                {
                    Name = "Día de Graduación",
                    DaysGranted = 2,
                    ValidFrom = new DateTime(DateTime.Now.Year, 1, 1),
                    ValidTo = new DateTime(DateTime.Now.Year, 12, 31),
                    Status = StatusEnum.Active
                }
            };

            await _context.ExtraBenefitDays.AddRangeAsync(extraBenefits);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {extraBenefits.Length} días de beneficio extra creados");
        }

        private async Task SeedHolidays()
        {
            Console.WriteLine("?? Seeding Holidays...");
            var currentYear = DateTime.Now.Year;
            var holidays = new[]
            {
                new Holiday
                {
                    Name = "Año Nuevo",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 1, 1),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de los Reyes Magos",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 1, 6),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de la Altagracia",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 1, 21),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de Duarte",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 1, 26),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de la Independencia",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 2, 27),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Viernes Santo",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 4, 18),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día del Trabajo",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 5, 1),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Corpus Christi",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 6, 19),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de la Restauración",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 8, 16),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de las Mercedes",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 9, 24),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Día de la Constitución",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 11, 6),
                    Status = StatusEnum.Active
                },
                new Holiday
                {
                    Name = "Navidad",
                    Year = currentYear,
                    StartDate = new DateTime(currentYear, 12, 25),
                    Status = StatusEnum.Active
                }
            };

            await _context.Holidays.AddRangeAsync(holidays);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {holidays.Length} días feriados creados");
        }

        private async Task SeedEmployees()
        {
            Console.WriteLine("?? Seeding Employees...");
            var departments = await _context.Departments.ToListAsync();
            var roles = await _context.Roles.ToListAsync();

            var employees = new[]
            {
                new Employee
                {
                    EmployeeCode = 1001,
                    FirstName = "Juan",
                    LastName = "Pérez",
                    Email = "juan.perez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Tecnología").Id,
                    RoleId = roles.First(r => r.Position == "Gerente de Departamento").Id,
                    AvailableDays = 20,
                    UsedDays = 5,
                    Status = EmployeeStatusEnum.Active
                },
                new Employee
                {
                    EmployeeCode = 1002,
                    FirstName = "María",
                    LastName = "González",
                    Email = "maria.gonzalez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Recursos Humanos").Id,
                    RoleId = roles.First(r => r.Position == "Gerente General").Id,
                    AvailableDays = 24,
                    UsedDays = 0,
                    Status = EmployeeStatusEnum.Active
                },
                new Employee
                {
                    EmployeeCode = 1003,
                    FirstName = "Carlos",
                    LastName = "Rodríguez",
                    Email = "carlos.rodriguez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Finanzas").Id,
                    RoleId = roles.First(r => r.Position == "Supervisor").Id,
                    AvailableDays = 18,
                    UsedDays = 10,
                    Status = EmployeeStatusEnum.Active
                },
                new Employee
                {
                    EmployeeCode = 1004,
                    FirstName = "Ana",
                    LastName = "Martínez",
                    Email = "ana.martinez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Marketing").Id,
                    RoleId = roles.First(r => r.Position == "Empleado").Id,
                    AvailableDays = 14,
                    UsedDays = 7,
                    Status = EmployeeStatusEnum.Active
                },
                new Employee
                {
                    EmployeeCode = 1005,
                    FirstName = "Luis",
                    LastName = "Hernández",
                    Email = "luis.hernandez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Ventas").Id,
                    RoleId = roles.First(r => r.Position == "Empleado").Id,
                    AvailableDays = 14,
                    UsedDays = 0,
                    Status = EmployeeStatusEnum.Active
                },
                new Employee
                {
                    EmployeeCode = 1006,
                    FirstName = "Laura",
                    LastName = "López",
                    Email = "laura.lopez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Operaciones").Id,
                    RoleId = roles.First(r => r.Position == "Supervisor").Id,
                    AvailableDays = 18,
                    UsedDays = 3,
                    Status = EmployeeStatusEnum.Active
                },
                new Employee
                {
                    EmployeeCode = 1007,
                    FirstName = "Pedro",
                    LastName = "Sánchez",
                    Email = "pedro.sanchez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Tecnología").Id,
                    RoleId = roles.First(r => r.Position == "Empleado").Id,
                    AvailableDays = 14,
                    UsedDays = 14,
                    Status = EmployeeStatusEnum.OnVacation
                },
                new Employee
                {
                    EmployeeCode = 1008,
                    FirstName = "Sofía",
                    LastName = "Ramírez",
                    Email = "sofia.ramirez@empresa.com",
                    DepartmentId = departments.First(d => d.Name == "Marketing").Id,
                    RoleId = roles.First(r => r.Position == "Pasante").Id,
                    AvailableDays = 10,
                    UsedDays = 2,
                    Status = EmployeeStatusEnum.Active
                }
            };

            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {employees.Length} empleados creados");
        }

        private async Task SeedUsers()
        {
            Console.WriteLine("?? Seeding Users...");
            var employees = await _context.Employees.ToListAsync();
            var roles = await _context.Roles.ToListAsync();

            var users = new[]
            {
                new User
                {
                    Username = "juan.perez",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = roles.First(r => r.Position == "Gerente de Departamento").Id,
                    EmployeeId = employees.First(e => e.Email == "juan.perez@empresa.com").Id,
                    Status = StatusEnum.Active
                },
                new User
                {
                    Username = "maria.gonzalez",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = roles.First(r => r.Position == "Gerente General").Id,
                    EmployeeId = employees.First(e => e.Email == "maria.gonzalez@empresa.com").Id,
                    Status = StatusEnum.Active
                },
                new User
                {
                    Username = "carlos.rodriguez",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = roles.First(r => r.Position == "Supervisor").Id,
                    EmployeeId = employees.First(e => e.Email == "carlos.rodriguez@empresa.com").Id,
                    Status = StatusEnum.Active
                },
                new User
                {
                    Username = "ana.martinez",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = roles.First(r => r.Position == "Empleado").Id,
                    EmployeeId = employees.First(e => e.Email == "ana.martinez@empresa.com").Id,
                    Status = StatusEnum.Active
                },
                new User
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = roles.First(r => r.Position == "Gerente General").Id,
                    EmployeeId = null,
                    Status = StatusEnum.Active
                }
            };

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {users.Length} usuarios creados");
        }

        private async Task SeedEmployeeExtraBenefitDays()
        {
            Console.WriteLine("???? Seeding Employee Extra Benefit Days...");
            var employees = await _context.Employees.ToListAsync();
            var extraBenefits = await _context.ExtraBenefitDays.ToListAsync();
            var currentYear = DateTime.Now.Year;

            var employeeBenefits = new List<EmployeeExtraBenefitDay>();

            // Asignar día de cumpleaños a algunos empleados
            var cumpleanos = extraBenefits.First(eb => eb.Name == "Día de Cumpleaños");
            employeeBenefits.Add(new EmployeeExtraBenefitDay
            {
                EmployeeId = employees.First(e => e.FirstName == "Juan").Id,
                ExtraBenefitDayId = cumpleanos.Id,
                Year = currentYear,
                UsedDays = 0,
                AssignedAt = DateTime.UtcNow
            });

            employeeBenefits.Add(new EmployeeExtraBenefitDay
            {
                EmployeeId = employees.First(e => e.FirstName == "María").Id,
                ExtraBenefitDayId = cumpleanos.Id,
                Year = currentYear,
                UsedDays = 1,
                AssignedAt = DateTime.UtcNow,
                ConsumedAt = DateTime.UtcNow.AddDays(-30)
            });

            // Asignar día de verano
            var verano = extraBenefits.First(eb => eb.Name == "Día de Verano");
            employeeBenefits.Add(new EmployeeExtraBenefitDay
            {
                EmployeeId = employees.First(e => e.FirstName == "Ana").Id,
                ExtraBenefitDayId = verano.Id,
                Year = currentYear,
                UsedDays = 0,
                AssignedAt = DateTime.UtcNow
            });

            await _context.EmployeeExtraBenefitDays.AddRangeAsync(employeeBenefits);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {employeeBenefits.Count} asignaciones de beneficios extra creadas");
        }

        private async Task SeedVacationRequests()
        {
            Console.WriteLine("?? Seeding Vacation Requests...");
            var employees = await _context.Employees.ToListAsync();
            var users = await _context.Users.Where(u => u.EmployeeId != null).ToListAsync();

            var vacationRequests = new[]
            {
                // Solicitud aprobada
                new VacationRequest
                {
                    EmployeeId = employees.First(e => e.FirstName == "Juan").Id,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.AddDays(30),
                    EndDate = DateTime.Now.AddDays(37),
                    Status = VacationStatusEnum.Approved,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    CreatedBy = users.First(u => u.Username == "juan.perez").Id,
                    ApprovedById = users.First(u => u.Username == "maria.gonzalez").Id,
                    DecisionDate = DateTime.UtcNow.AddDays(-5)
                },
                // Solicitud pendiente
                new VacationRequest
                {
                    EmployeeId = employees.First(e => e.FirstName == "Ana").Id,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.AddDays(15),
                    EndDate = DateTime.Now.AddDays(22),
                    Status = VacationStatusEnum.Pending,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    CreatedBy = users.First(u => u.Username == "ana.martinez").Id
                },
                // Solicitud rechazada
                new VacationRequest
                {
                    EmployeeId = employees.First(e => e.FirstName == "Carlos").Id,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(10),
                    Status = VacationStatusEnum.Rejected,
                    CreatedAt = DateTime.UtcNow.AddDays(-7),
                    CreatedBy = users.First(u => u.Username == "carlos.rodriguez").Id,
                    ApprovedById = users.First(u => u.Username == "maria.gonzalez").Id,
                    DecisionDate = DateTime.UtcNow.AddDays(-3)
                },
                // Solicitud en curso (empleado de vacaciones)
                new VacationRequest
                {
                    EmployeeId = employees.First(e => e.FirstName == "Pedro").Id,
                    VacationType = VacationType.Vacation,
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(9),
                    Status = VacationStatusEnum.Approved,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    CreatedBy = users.First(u => u.Username == "juan.perez").Id,
                    ApprovedById = users.First(u => u.Username == "maria.gonzalez").Id,
                    DecisionDate = DateTime.UtcNow.AddDays(-10)
                }
            };

            await _context.VacationRequests.AddRangeAsync(vacationRequests);
            await _context.SaveChangesAsync();
            Console.WriteLine($"   ? {vacationRequests.Length} solicitudes de vacaciones creadas");
        }
    }
}

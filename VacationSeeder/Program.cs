using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using vacation_backend.Infrastructure;
using VacationSeeder;

Console.WriteLine("===========================================");
Console.WriteLine("   SEEDER DE DATOS - VACATION MANAGER");
Console.WriteLine("===========================================");
Console.WriteLine();

// Cargar configuración
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("❌ Error: No se encontró la cadena de conexión.");
    return;
}

Console.WriteLine($"📡 Conectando a la base de datos...");
Console.WriteLine($"   Server: {connectionString.Split(';')[0].Replace("Server=", "")}");
Console.WriteLine();

// Configurar DbContext
var optionsBuilder = new DbContextOptionsBuilder<VacationDbContext>();
optionsBuilder.UseSqlServer(connectionString);

try
{
    using var context = new VacationDbContext(optionsBuilder.Options);

    // Verificar conexión
    if (!await context.Database.CanConnectAsync())
    {
        Console.WriteLine("❌ Error: No se puede conectar a la base de datos.");
        return;
    }

    Console.WriteLine("✅ Conexión establecida exitosamente!");
    Console.WriteLine();

    // Ejecutar el seeder
    var seeder = new DatabaseSeeder(context);
    await seeder.SeedAsync();

    Console.WriteLine();
    Console.WriteLine("===========================================");
    Console.WriteLine("   PROCESO COMPLETADO");
    Console.WriteLine("===========================================");
    Console.WriteLine();
    Console.WriteLine("📊 Resumen de datos creados:");
    Console.WriteLine($"   • Departamentos: {await context.Departments.CountAsync()}");
    Console.WriteLine($"   • Roles: {await context.Roles.CountAsync()}");
    Console.WriteLine($"   • Permisos: {await context.Permissions.CountAsync()}");
    Console.WriteLine($"   • Empleados: {await context.Employees.CountAsync()}");
    Console.WriteLine($"   • Usuarios: {await context.Users.CountAsync()}");
    Console.WriteLine($"   • Días Extra: {await context.ExtraBenefitDays.CountAsync()}");
    Console.WriteLine($"   • Días Feriados: {await context.Holidays.CountAsync()}");
    Console.WriteLine($"   • Solicitudes de Vacaciones: {await context.VacationRequests.CountAsync()}");
    Console.WriteLine();
    Console.WriteLine("Usuarios de prueba:");
    Console.WriteLine("   • admin / admin123 (Gerente General)");
    Console.WriteLine("   • maria.gonzalez / password123 (Gerente General)");
    Console.WriteLine("   • juan.perez / password123 (Gerente de Departamento)");
    Console.WriteLine("   • carlos.rodriguez / password123 (Supervisor)");
    Console.WriteLine("   • ana.martinez / password123 (Empleado)");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("❌ ERROR DURANTE EL PROCESO:");
    Console.WriteLine($"   {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"   Detalle: {ex.InnerException.Message}");
    }
    Console.WriteLine();
    Console.WriteLine("Stack Trace:");
    Console.WriteLine(ex.StackTrace);
}

Console.WriteLine();
Console.WriteLine("Presiona cualquier tecla para salir...");
Console.ReadKey();

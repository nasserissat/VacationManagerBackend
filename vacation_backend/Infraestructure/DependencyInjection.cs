using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Application.Services;
using vacation_backend.Infraestructure.Repositories;

namespace vacation_backend.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}

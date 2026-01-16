using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Application.Services;

namespace vacation_backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IVacationService, VacationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;

        }
    }
}

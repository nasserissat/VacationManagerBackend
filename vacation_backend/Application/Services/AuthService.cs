using vacation_backend.Application.DTOs.Auth;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Domain.Enums;

namespace vacation_backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            // Buscar el usuario por nombre de usuario
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Usuario o contraseña incorrectos"
                };
            }

            // Verificar que el usuario esté activo
            if (user.Status != StatusEnum.Active)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "El usuario está inactivo"
                };
            }

            // Verificación simple de contraseña (sin hash por ahora, ya que es temporal)
            // NOTA: Esto será reemplazado por Active Directory
            if (user.PasswordHash != loginRequest.Password)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Usuario o contraseña incorrectos"
                };
            }

            // Login exitoso
            return new AuthResponseDto
            {
                Success = true,
                Message = "Login exitoso",
                UserId = user.Id,
                Username = user.Username,
                RoleId = user.RoleId,
                RoleName = user.Role?.Position,
                EmployeeId = user.EmployeeId,
                EmployeeName = user.Employee != null 
                    ? $"{user.Employee.FirstName} {user.Employee.LastName}" 
                    : null
            };
        }
    }
}

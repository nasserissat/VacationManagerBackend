using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.DTOs.Role;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfaces.IRepositories;
using vacation_backend.Application.Interfases.IServices;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Application.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        #region Departments
        public async Task<List<DepartmentListDto>> GetAllDepartmentsAsync()
        {
            var departments = await _settingRepository.GetAllDepartmentsAsync();
            if (departments != null && departments.Count == 0)
                return new List<DepartmentListDto>();

            var result = departments.Select(d => new DepartmentListDto
            {
                Name = d.Name
            }).ToList();

            return result;
        }
        public async Task<DepartmentListDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _settingRepository.GetDepartmentByIdAsync(id);
            var departmentDto = new DepartmentListDto
            {
                Name = department.Name
            };
            return departmentDto;
        }
        public async Task<int> CreateDepartmentAsync(DepartmentDataDto data)
        {
            var department = new Department
            {
                Name = data.Name
            };
            var result = await _settingRepository.CreateDepartmentAsync(department);

            return result;
        }

        public async Task<OperationResultDto> UpdateDepartmentAsync(int id, DepartmentDataDto data)
        {
            if (string.IsNullOrEmpty(data.Name))
                throw new Exception("El nombre es obligatorio");

            var department = new Department
            {
                Id = id,
                Name = data.Name
            };
            var result = await _settingRepository.UpdateDepartmentAsync(department);

            return new OperationResultDto
            {
                Success = result,
                Message = result ? "Departamento actualizado correctamente." : "No se encontró el departamento.",
                AffectedRows = result ? 1 : 0
            };
        }
        public async Task<OperationResultDto> DeleteDepartmentAsync(int id)
        {
            var department = await _settingRepository.GetDepartmentByIdAsync(id);
            if (department == null)
                return new OperationResultDto
                {
                    Success = false,
                    Message = $"El departamento con el id {id} no existe",
                    AffectedRows = 0
                };
            var deleted = await _settingRepository.DeleteDepartmentAsync(department);
            return new OperationResultDto
            {
                Success = deleted,
                Message = deleted ? "Departamento eliminado correctamente" : "Error al eliminar el departamento.",
                AffectedRows = deleted ? 1 : 0
            };
        }
        #endregion 

        #region Permissions 
        public async Task<List<PermissionListDto>> GetAllPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionListDto> GetPermissionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreatePermission(PermissionDataDto data)
        {
            throw new NotImplementedException();
        }
        

        public async Task<OperationResultDto> UpdatePermissionAsync(int id, PermissionDataDto data)
        {
            if (string.IsNullOrEmpty(data.Name))
                throw new Exception("El nombre es obligatorio");

            var permission = new Permission
            {
                Id = id,
                Key = data.Name,
                Description = data.Description
            };
            var result = await _settingRepository.UpdatePermissionAsync(permission);

            return new OperationResultDto
            {
                Success = result,
                Message = result ? "Permiso actualizado correctamente." : "No se encontró el permiso.",
                AffectedRows = result ? 1 : 0
            };
        }
        #endregion

        #region Roles

        public async Task<List<RoleListDto>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<RoleListDto> GetRoleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateRoleAsync(RoleDataDto data)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto> UpdateRoleAsync(int id, RoleDataDto data)
        {
            if (string.IsNullOrEmpty(data.Name))
                throw new Exception("El nombre es obligatorio");

            var role = new Role
            {
                Id = id,
                Position = data.Name
            };
            var result = await _settingRepository.UpdateRoleAsync(role);

            return new OperationResultDto
            {
                Success = result,
                Message = result ? "Role actualizado correctamente." : "No se encontró el Role",
                AffectedRows = result ? 1 : 0
            };
        }
        #endregion

        #region ExtraBenefitDays
        public Task<List<ExtraBenefitDayDataDto>> GetAllExtraBenefitDaysAsync()
        {
            throw new NotImplementedException();
        }
        public Task<ExtraBenefitDayDataDto?> GetExtraBenefitDayByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResultDto> CreateExtraBenefitDayAsync(ExtraBenefitDayDataDto data)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResultDto> UpdateExtraBenefitDayAsync(int id, ExtraBenefitDayDataDto data)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResultDto> DeleteExtraBenefitDayAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion 

        #region Holidays
        public Task<List<HolidayDataDto>> GetAllHolidaysAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> CreateHolidayAsync(HolidayDataDto data)
        {
            throw new NotImplementedException();
        }
        public Task<OperationResultDto> UpdateHolidayAsync(int id, HolidayDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeleteHolidayAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

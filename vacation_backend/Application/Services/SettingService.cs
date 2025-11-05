using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
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
        public async Task<int> CreateDepartmentAsync(DepartmentDataDto data)
        {
            var department = new Department
            {
                Name = data.Name
            };
            var result = await _settingRepository.CreateDepartmentAsync(department);

            return result;
        }

        public Task<OperationResultDto> CreateExtraBenefitDayAsync(ExtraBenefitDayDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateHolidayAsync(HolidayDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeleteDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeleteExtraBenefitDayAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> DeleteHolidayAsync(int id)
        {
            throw new NotImplementedException();
        }

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

        public Task<List<ExtraBenefitDayDataDto>> GetAllExtraBenefitDaysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<HolidayDataDto>> GetAllHolidaysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentListDto?> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ExtraBenefitDayDataDto?> GetExtraBenefitDayByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> UpdateDepartmentAsync(int id, DepartmentDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> UpdateExtraBenefitDayAsync(int id, ExtraBenefitDayDataDto data)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultDto> UpdateHolidayAsync(int id, HolidayDataDto data)
        {
            throw new NotImplementedException();
        }
    }
}

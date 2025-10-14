using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.DTOs.Vacation;
using vacation_backend.Application.Interfases.IServices;

namespace vacation_backend.Application.Services
{
    public class SettingService : ISettingService
    {
        public Task<int> CreateDepartmentAsync(DepartmentDataDto data)
        {
            throw new NotImplementedException();
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

        public Task<List<DepartmentDataDto>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ExtraBenefitDayDataDto>> GetAllExtraBenefitDaysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<HolidayDataDto>> GetAllHolidaysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDataDto?> GetDepartmentByIdAsync(int id)
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

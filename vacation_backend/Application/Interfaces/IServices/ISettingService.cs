using vacation_backend.Application.DTOs;
using vacation_backend.Application.DTOs.Department;
using vacation_backend.Application.DTOs.Vacation;

namespace vacation_backend.Application.Interfases.IServices
{
    public interface ISettingService
    {
        #region Department
        Task<int> CreateDepartmentAsync(DepartmentDataDto data);
        Task<OperationResultDto> UpdateDepartmentAsync(int id, DepartmentDataDto data);
        Task<OperationResultDto> DeleteDepartmentAsync(int id);
        Task<List<DepartmentDataDto>> GetAllDepartmentsAsync();
        Task<DepartmentDataDto?> GetDepartmentByIdAsync(int id);
        #endregion

        #region ExtraBenefitDay
        Task<OperationResultDto> CreateExtraBenefitDayAsync(ExtraBenefitDayDataDto data);
        Task<OperationResultDto> UpdateExtraBenefitDayAsync(int id, ExtraBenefitDayDataDto data);
        Task<OperationResultDto> DeleteExtraBenefitDayAsync(int id);
        Task<List<ExtraBenefitDayDataDto>> GetAllExtraBenefitDaysAsync();
        Task<ExtraBenefitDayDataDto?> GetExtraBenefitDayByIdAsync(int id);
        #endregion

        #region Holidays
        Task<int> CreateHolidayAsync(HolidayDataDto data);
        Task<OperationResultDto> UpdateHolidayAsync(int id, HolidayDataDto data);
        Task<OperationResultDto> DeleteHolidayAsync(int id);
        Task<List<HolidayDataDto>> GetAllHolidaysAsync();
        #endregion

    }
}

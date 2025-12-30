using GreatSoft.Be.Application.DTOs.ResidentPreference;

namespace GreatSoft.Be.Application.Interfaces;

public interface IResidentPreferenceService
{
    Task<IEnumerable<ResidentPreferenceDto>> GetAllAsync();
    Task<ResidentPreferenceDto?> GetByIdAsync(int id);
    Task<IEnumerable<ResidentPreferenceDto>> GetByResidentIdAsync(int residentId);
    Task<ResidentPreferenceDto> CreateAsync(CreateResidentPreferenceDto createResidentPreferenceDto);
    Task<ResidentPreferenceDto?> UpdateAsync(int id, UpdateResidentPreferenceDto updateResidentPreferenceDto);
    Task<bool> DeleteAsync(int id);
}



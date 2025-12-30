using GreatSoft.Be.Application.DTOs.ResidentPreference;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class ResidentPreferenceService : IResidentPreferenceService
{
    private readonly IResidentPreferenceRepository _residentPreferenceRepository;
    private readonly IUserRepository _userRepository;

    public ResidentPreferenceService(
        IResidentPreferenceRepository residentPreferenceRepository,
        IUserRepository userRepository)
    {
        _residentPreferenceRepository = residentPreferenceRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ResidentPreferenceDto>> GetAllAsync()
    {
        var preferences = await _residentPreferenceRepository.GetAllAsync();
        return preferences.Select(MapToDto);
    }

    public async Task<ResidentPreferenceDto?> GetByIdAsync(int id)
    {
        var preference = await _residentPreferenceRepository.GetByIdAsync(id);
        if (preference == null) return null;
        return MapToDto(preference);
    }

    public async Task<IEnumerable<ResidentPreferenceDto>> GetByResidentIdAsync(int residentId)
    {
        var preferences = await _residentPreferenceRepository.GetByResidentIdAsync(residentId);
        return preferences.Select(MapToDto);
    }

    public async Task<ResidentPreferenceDto> CreateAsync(CreateResidentPreferenceDto createResidentPreferenceDto)
    {
        // Verify resident exists
        var resident = await _userRepository.GetByIdAsync(createResidentPreferenceDto.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        // Check if preference with same name already exists for this resident
        var existingPreference = await _residentPreferenceRepository.GetByResidentIdAndNameAsync(
            createResidentPreferenceDto.ResidentId, 
            createResidentPreferenceDto.Name);
        
        if (existingPreference != null)
        {
            throw new InvalidOperationException($"Preference '{createResidentPreferenceDto.Name}' already exists for this resident");
        }

        var preference = new ResidentPreference
        {
            ResidentId = createResidentPreferenceDto.ResidentId,
            Name = createResidentPreferenceDto.Name,
            Value = createResidentPreferenceDto.Value,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _residentPreferenceRepository.AddAsync(preference);
        var createdPreference = await _residentPreferenceRepository.GetByIdAsync(preference.Id);
        return MapToDto(createdPreference!);
    }

    public async Task<ResidentPreferenceDto?> UpdateAsync(int id, UpdateResidentPreferenceDto updateResidentPreferenceDto)
    {
        var preference = await _residentPreferenceRepository.GetByIdAsync(id);
        if (preference == null) return null;

        preference.Value = updateResidentPreferenceDto.Value;
        preference.IsActive = updateResidentPreferenceDto.IsActive;
        preference.UpdatedAt = DateTime.UtcNow;

        await _residentPreferenceRepository.UpdateAsync(preference);
        var updatedPreference = await _residentPreferenceRepository.GetByIdAsync(preference.Id);
        return MapToDto(updatedPreference!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var preference = await _residentPreferenceRepository.GetByIdAsync(id);
        if (preference == null) return false;

        await _residentPreferenceRepository.DeleteAsync(preference);
        return true;
    }

    private static ResidentPreferenceDto MapToDto(ResidentPreference preference)
    {
        return new ResidentPreferenceDto
        {
            Id = preference.Id,
            ResidentId = preference.ResidentId,
            ResidentName = preference.Resident != null ? $"{preference.Resident.FirstName} {preference.Resident.LastName}" : string.Empty,
            Name = preference.Name,
            Value = preference.Value,
            IsActive = preference.IsActive,
            CreatedAt = preference.CreatedAt
        };
    }
}



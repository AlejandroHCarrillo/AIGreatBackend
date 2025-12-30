using GreatSoft.Be.Application.DTOs.ResidentProvider;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class ResidentProviderService : IResidentProviderService
{
    private readonly IResidentProviderRepository _residentProviderRepository;
    private readonly ICommunityRepository _communityRepository;

    public ResidentProviderService(
        IResidentProviderRepository residentProviderRepository,
        ICommunityRepository communityRepository)
    {
        _residentProviderRepository = residentProviderRepository;
        _communityRepository = communityRepository;
    }

    public async Task<IEnumerable<ResidentProviderDto>> GetAllAsync()
    {
        var providers = await _residentProviderRepository.GetAllAsync();
        return providers.Select(MapToDto);
    }

    public async Task<ResidentProviderDto?> GetByIdAsync(int id)
    {
        var provider = await _residentProviderRepository.GetByIdAsync(id);
        if (provider == null) return null;
        return MapToDto(provider);
    }

    public async Task<ResidentProviderDto> CreateAsync(CreateResidentProviderDto createResidentProviderDto)
    {
        // Verify community exists
        var community = await _communityRepository.GetByIdAsync(createResidentProviderDto.CommunityId);
        if (community == null)
        {
            throw new InvalidOperationException("Community not found");
        }

        var provider = new ResidentProvider
        {
            Name = createResidentProviderDto.Name,
            ServiceType = createResidentProviderDto.ServiceType,
            Phone = createResidentProviderDto.Phone,
            Email = createResidentProviderDto.Email,
            CommunityId = createResidentProviderDto.CommunityId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _residentProviderRepository.AddAsync(provider);
        var createdProvider = await _residentProviderRepository.GetByIdAsync(provider.Id);
        return MapToDto(createdProvider!);
    }

    public async Task<ResidentProviderDto?> UpdateAsync(int id, UpdateResidentProviderDto updateResidentProviderDto)
    {
        var provider = await _residentProviderRepository.GetByIdAsync(id);
        if (provider == null) return null;

        provider.Name = updateResidentProviderDto.Name;
        provider.ServiceType = updateResidentProviderDto.ServiceType;
        provider.Phone = updateResidentProviderDto.Phone;
        provider.Email = updateResidentProviderDto.Email;
        provider.IsActive = updateResidentProviderDto.IsActive;
        provider.UpdatedAt = DateTime.UtcNow;

        await _residentProviderRepository.UpdateAsync(provider);
        var updatedProvider = await _residentProviderRepository.GetByIdAsync(provider.Id);
        return MapToDto(updatedProvider!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var provider = await _residentProviderRepository.GetByIdAsync(id);
        if (provider == null) return false;

        await _residentProviderRepository.DeleteAsync(provider);
        return true;
    }

    private static ResidentProviderDto MapToDto(ResidentProvider provider)
    {
        return new ResidentProviderDto
        {
            Id = provider.Id,
            Name = provider.Name,
            ServiceType = provider.ServiceType,
            Phone = provider.Phone,
            Email = provider.Email,
            CommunityId = provider.CommunityId,
            CommunityName = provider.Community?.Name ?? string.Empty,
            IsActive = provider.IsActive
        };
    }
}


using GreatSoft.Be.Application.DTOs.Community;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class CommunityService : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly ICompanyRepository _companyRepository;

    public CommunityService(
        ICommunityRepository communityRepository,
        ICompanyRepository companyRepository)
    {
        _communityRepository = communityRepository;
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<CommunityDto>> GetAllAsync()
    {
        var communities = await _communityRepository.GetAllAsync();
        return communities.Select(MapToDto);
    }

    public async Task<CommunityDto?> GetByIdAsync(int id)
    {
        var community = await _communityRepository.GetByIdWithDetailsAsync(id);
        if (community == null) return null;
        return MapToDto(community);
    }

    public async Task<CommunityDto> CreateAsync(CreateCommunityDto createCommunityDto)
    {
        // Verify company exists
        var company = await _companyRepository.GetByIdAsync(createCommunityDto.CompanyId);
        if (company == null)
        {
            throw new InvalidOperationException("Company not found");
        }

        var community = new Community
        {
            Name = createCommunityDto.Name,
            Address = createCommunityDto.Address,
            City = createCommunityDto.City,
            State = createCommunityDto.State,
            ZipCode = createCommunityDto.ZipCode,
            CompanyId = createCommunityDto.CompanyId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _communityRepository.AddAsync(community);
        var createdCommunity = await _communityRepository.GetByIdWithDetailsAsync(community.Id);
        return MapToDto(createdCommunity!);
    }

    public async Task<CommunityDto?> UpdateAsync(int id, UpdateCommunityDto updateCommunityDto)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null) return null;

        community.Name = updateCommunityDto.Name;
        community.Address = updateCommunityDto.Address;
        community.City = updateCommunityDto.City;
        community.State = updateCommunityDto.State;
        community.ZipCode = updateCommunityDto.ZipCode;
        community.IsActive = updateCommunityDto.IsActive;
        community.UpdatedAt = DateTime.UtcNow;

        await _communityRepository.UpdateAsync(community);
        var updatedCommunity = await _communityRepository.GetByIdWithDetailsAsync(community.Id);
        return MapToDto(updatedCommunity!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null) return false;

        await _communityRepository.DeleteAsync(community);
        return true;
    }

    private static CommunityDto MapToDto(Community community)
    {
        return new CommunityDto
        {
            Id = community.Id,
            Name = community.Name,
            Address = community.Address,
            City = community.City,
            State = community.State,
            ZipCode = community.ZipCode,
            CompanyId = community.CompanyId,
            CompanyName = community.Company?.Name ?? string.Empty,
            IsActive = community.IsActive,
            CreatedAt = community.CreatedAt
        };
    }
}


using GreatSoft.Be.Application.DTOs.Community;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class CommunityService : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IRepository<CommunityType> _communityTypeRepository;

    public CommunityService(
        ICommunityRepository communityRepository,
        IRepository<CommunityType> communityTypeRepository)
    {
        _communityRepository = communityRepository;
        _communityTypeRepository = communityTypeRepository;
    }

    public async Task<IEnumerable<CommunityDto>> GetAllCommunitiesAsync()
    {
        var communities = await _communityRepository.GetAllAsync();
        return communities.Select(c => new CommunityDto
        {
            Id = c.Id,
            CommunityTypeId = c.CommunityTypeId,
            CommunityTypeName = c.CommunityType?.Name ?? string.Empty,
            Name = c.Name,
            Location = c.Location,
            Lat = c.Lat,
            Lng = c.Lng,
            HousingCount = c.HousingCount,
            ContactPhone = c.ContactPhone,
            ContactEmail = c.ContactEmail,
            CreatedAt = c.CreatedAt
        });
    }

    public async Task<CommunityDto?> GetCommunityByIdAsync(Guid id)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null) return null;

        return new CommunityDto
        {
            Id = community.Id,
            CommunityTypeId = community.CommunityTypeId,
            CommunityTypeName = community.CommunityType?.Name ?? string.Empty,
            Name = community.Name,
            Location = community.Location,
            Lat = community.Lat,
            Lng = community.Lng,
            HousingCount = community.HousingCount,
            ContactPhone = community.ContactPhone,
            ContactEmail = community.ContactEmail,
            CreatedAt = community.CreatedAt
        };
    }

    public async Task<CommunityDto> CreateCommunityAsync(CreateCommunityRequest request)
    {
        if (await _communityRepository.GetByNameAsync(request.Name) != null)
        {
            throw new InvalidOperationException("Community name already exists");
        }

        var communityType = await _communityTypeRepository.GetByIdAsync(request.CommunityTypeId);
        if (communityType == null)
        {
            throw new InvalidOperationException("CommunityType not found");
        }

        var community = new Community
        {
            Id = Guid.NewGuid(),
            CommunityTypeId = request.CommunityTypeId,
            Name = request.Name,
            Location = request.Location,
            Lat = request.Lat,
            Lng = request.Lng,
            HousingCount = request.HousingCount,
            ContactPhone = request.ContactPhone,
            ContactEmail = request.ContactEmail,
            CreatedAt = DateTime.UtcNow
        };

        await _communityRepository.AddAsync(community);

        return new CommunityDto
        {
            Id = community.Id,
            CommunityTypeId = community.CommunityTypeId,
            CommunityTypeName = communityType.Name,
            Name = community.Name,
            Location = community.Location,
            Lat = community.Lat,
            Lng = community.Lng,
            HousingCount = community.HousingCount,
            ContactPhone = community.ContactPhone,
            ContactEmail = community.ContactEmail,
            CreatedAt = community.CreatedAt
        };
    }

    public async Task<CommunityDto> UpdateCommunityAsync(Guid id, UpdateCommunityRequest request)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null)
        {
            throw new InvalidOperationException("Community not found");
        }

        // Check if name is being changed and if it already exists
        if (community.Name != request.Name && await _communityRepository.GetByNameAsync(request.Name) != null)
        {
            throw new InvalidOperationException("Community name already exists");
        }

        var communityType = await _communityTypeRepository.GetByIdAsync(request.CommunityTypeId);
        if (communityType == null)
        {
            throw new InvalidOperationException("CommunityType not found");
        }

        community.CommunityTypeId = request.CommunityTypeId;
        community.Name = request.Name;
        community.Location = request.Location;
        community.Lat = request.Lat;
        community.Lng = request.Lng;
        community.HousingCount = request.HousingCount;
        community.ContactPhone = request.ContactPhone;
        community.ContactEmail = request.ContactEmail;

        await _communityRepository.UpdateAsync(community);

        return new CommunityDto
        {
            Id = community.Id,
            CommunityTypeId = community.CommunityTypeId,
            CommunityTypeName = communityType.Name,
            Name = community.Name,
            Location = community.Location,
            Lat = community.Lat,
            Lng = community.Lng,
            HousingCount = community.HousingCount,
            ContactPhone = community.ContactPhone,
            ContactEmail = community.ContactEmail,
            CreatedAt = community.CreatedAt
        };
    }

    public async Task<bool> DeleteCommunityAsync(Guid id)
    {
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null)
        {
            return false;
        }

        await _communityRepository.DeleteAsync(community);
        return true;
    }
}



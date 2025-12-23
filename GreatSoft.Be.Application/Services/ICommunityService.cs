using GreatSoft.Be.Application.DTOs.Community;

namespace GreatSoft.Be.Application.Services;

public interface ICommunityService
{
    Task<IEnumerable<CommunityDto>> GetAllCommunitiesAsync();
    Task<CommunityDto?> GetCommunityByIdAsync(Guid id);
    Task<CommunityDto> CreateCommunityAsync(CreateCommunityRequest request);
    Task<CommunityDto> UpdateCommunityAsync(Guid id, UpdateCommunityRequest request);
    Task<bool> DeleteCommunityAsync(Guid id);
}



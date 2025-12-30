using GreatSoft.Be.Application.DTOs.Community;

namespace GreatSoft.Be.Application.Interfaces;

public interface ICommunityService
{
    Task<IEnumerable<CommunityDto>> GetAllAsync();
    Task<CommunityDto?> GetByIdAsync(int id);
    Task<CommunityDto> CreateAsync(CreateCommunityDto createCommunityDto);
    Task<CommunityDto?> UpdateAsync(int id, UpdateCommunityDto updateCommunityDto);
    Task<bool> DeleteAsync(int id);
}


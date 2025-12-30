using GreatSoft.Be.Application.DTOs.Amenity;

namespace GreatSoft.Be.Application.Interfaces;

public interface IAmenityService
{
    Task<IEnumerable<AmenityDto>> GetAllAsync();
    Task<AmenityDto?> GetByIdAsync(int id);
    Task<IEnumerable<AmenityDto>> GetByCommunityIdAsync(int communityId);
    Task<AmenityDto> CreateAsync(CreateAmenityDto createAmenityDto);
    Task<AmenityDto?> UpdateAsync(int id, UpdateAmenityDto updateAmenityDto);
    Task<bool> DeleteAsync(int id);
}


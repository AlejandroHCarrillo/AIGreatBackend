using GreatSoft.Be.Application.DTOs.Amenity;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class AmenityService : IAmenityService
{
    private readonly IAmenityRepository _amenityRepository;
    private readonly ICommunityRepository _communityRepository;

    public AmenityService(
        IAmenityRepository amenityRepository,
        ICommunityRepository communityRepository)
    {
        _amenityRepository = amenityRepository;
        _communityRepository = communityRepository;
    }

    public async Task<IEnumerable<AmenityDto>> GetAllAsync()
    {
        var amenities = await _amenityRepository.GetAllAsync();
        return amenities.Select(MapToDto);
    }

    public async Task<AmenityDto?> GetByIdAsync(int id)
    {
        var amenity = await _amenityRepository.GetByIdWithDetailsAsync(id);
        if (amenity == null) return null;
        return MapToDto(amenity);
    }

    public async Task<IEnumerable<AmenityDto>> GetByCommunityIdAsync(int communityId)
    {
        var amenities = await _amenityRepository.GetByCommunityIdAsync(communityId);
        return amenities.Select(MapToDto);
    }

    public async Task<AmenityDto> CreateAsync(CreateAmenityDto createAmenityDto)
    {
        // Verify community exists
        var community = await _communityRepository.GetByIdAsync(createAmenityDto.CommunityId);
        if (community == null)
        {
            throw new InvalidOperationException("Community not found");
        }

        var amenity = new Amenity
        {
            CommunityId = createAmenityDto.CommunityId,
            Name = createAmenityDto.Name,
            Description = createAmenityDto.Description,
            Rules = createAmenityDto.Rules,
            Cost = createAmenityDto.Cost,
            Image = createAmenityDto.Image,
            MaxCapacity = createAmenityDto.MaxCapacity,
            SimultaneousReservations = createAmenityDto.SimultaneousReservations,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _amenityRepository.AddAsync(amenity);
        var createdAmenity = await _amenityRepository.GetByIdWithDetailsAsync(amenity.Id);
        return MapToDto(createdAmenity!);
    }

    public async Task<AmenityDto?> UpdateAsync(int id, UpdateAmenityDto updateAmenityDto)
    {
        var amenity = await _amenityRepository.GetByIdAsync(id);
        if (amenity == null) return null;

        amenity.Name = updateAmenityDto.Name;
        amenity.Description = updateAmenityDto.Description;
        amenity.Rules = updateAmenityDto.Rules;
        amenity.Cost = updateAmenityDto.Cost;
        amenity.Image = updateAmenityDto.Image;
        amenity.MaxCapacity = updateAmenityDto.MaxCapacity;
        amenity.SimultaneousReservations = updateAmenityDto.SimultaneousReservations;
        amenity.IsActive = updateAmenityDto.IsActive;
        amenity.UpdatedAt = DateTime.UtcNow;

        await _amenityRepository.UpdateAsync(amenity);
        var updatedAmenity = await _amenityRepository.GetByIdWithDetailsAsync(amenity.Id);
        return MapToDto(updatedAmenity!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var amenity = await _amenityRepository.GetByIdAsync(id);
        if (amenity == null) return false;

        await _amenityRepository.DeleteAsync(amenity);
        return true;
    }

    private static AmenityDto MapToDto(Amenity amenity)
    {
        return new AmenityDto
        {
            Id = amenity.Id,
            CommunityId = amenity.CommunityId,
            CommunityName = amenity.Community?.Name ?? string.Empty,
            Name = amenity.Name,
            Description = amenity.Description,
            Rules = amenity.Rules,
            Cost = amenity.Cost,
            Image = amenity.Image,
            MaxCapacity = amenity.MaxCapacity,
            SimultaneousReservations = amenity.SimultaneousReservations,
            IsActive = amenity.IsActive,
            CreatedAt = amenity.CreatedAt
        };
    }
}


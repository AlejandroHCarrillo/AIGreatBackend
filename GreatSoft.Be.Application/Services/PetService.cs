using GreatSoft.Be.Application.DTOs.Pet;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserRepository _userRepository;

    public PetService(
        IPetRepository petRepository,
        ICommunityRepository communityRepository,
        IUserRepository userRepository)
    {
        _petRepository = petRepository;
        _communityRepository = communityRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<PetDto>> GetAllAsync()
    {
        var pets = await _petRepository.GetAllAsync();
        return pets.Select(MapToDto);
    }

    public async Task<PetDto?> GetByIdAsync(int id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null) return null;
        return MapToDto(pet);
    }

    public async Task<PetDto> CreateAsync(CreatePetDto createPetDto)
    {
        // Verify community exists
        var community = await _communityRepository.GetByIdAsync(createPetDto.CommunityId);
        if (community == null)
        {
            throw new InvalidOperationException("Community not found");
        }

        // Verify owner exists
        var owner = await _userRepository.GetByIdAsync(createPetDto.OwnerId);
        if (owner == null)
        {
            throw new InvalidOperationException("Owner not found");
        }

        var pet = new Pet
        {
            Name = createPetDto.Name,
            Type = createPetDto.Type,
            Breed = createPetDto.Breed,
            Color = createPetDto.Color,
            CommunityId = createPetDto.CommunityId,
            OwnerId = createPetDto.OwnerId,
            RegistrationDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _petRepository.AddAsync(pet);
        var createdPet = await _petRepository.GetByIdAsync(pet.Id);
        return MapToDto(createdPet!);
    }

    public async Task<PetDto?> UpdateAsync(int id, UpdatePetDto updatePetDto)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null) return null;

        pet.Name = updatePetDto.Name;
        pet.Type = updatePetDto.Type;
        pet.Breed = updatePetDto.Breed;
        pet.Color = updatePetDto.Color;
        pet.IsActive = updatePetDto.IsActive;
        pet.UpdatedAt = DateTime.UtcNow;

        await _petRepository.UpdateAsync(pet);
        var updatedPet = await _petRepository.GetByIdAsync(pet.Id);
        return MapToDto(updatedPet!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null) return false;

        await _petRepository.DeleteAsync(pet);
        return true;
    }

    private static PetDto MapToDto(Pet pet)
    {
        return new PetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Type = pet.Type,
            Breed = pet.Breed,
            Color = pet.Color,
            CommunityId = pet.CommunityId,
            CommunityName = pet.Community?.Name ?? string.Empty,
            OwnerId = pet.OwnerId,
            OwnerName = pet.Owner != null ? $"{pet.Owner.FirstName} {pet.Owner.LastName}" : string.Empty,
            RegistrationDate = pet.RegistrationDate,
            IsActive = pet.IsActive
        };
    }
}


using GreatSoft.Be.Application.DTOs.Pet;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IRepository<Resident> _residentRepository;

    public PetService(
        IPetRepository petRepository,
        IRepository<Resident> residentRepository)
    {
        _petRepository = petRepository;
        _residentRepository = residentRepository;
    }

    public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
    {
        var pets = await _petRepository.GetAllAsync();
        return pets.Select(p => new PetDto
        {
            Id = p.Id,
            ResidentId = p.ResidentId,
            ResidentName = p.Resident?.FullName ?? string.Empty,
            Name = p.Name,
            Species = p.Species,
            Breed = p.Breed,
            Age = p.Age,
            Color = p.Color,
            CreatedAt = p.CreatedAt
        });
    }

    public async Task<PetDto?> GetPetByIdAsync(Guid id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null) return null;

        return new PetDto
        {
            Id = pet.Id,
            ResidentId = pet.ResidentId,
            ResidentName = pet.Resident?.FullName ?? string.Empty,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            Age = pet.Age,
            Color = pet.Color,
            CreatedAt = pet.CreatedAt
        };
    }

    public async Task<IEnumerable<PetDto>> GetPetsByResidentIdAsync(Guid residentId)
    {
        var pets = await _petRepository.GetAllAsync();
        return pets
            .Where(p => p.ResidentId == residentId)
            .Select(p => new PetDto
            {
                Id = p.Id,
                ResidentId = p.ResidentId,
                ResidentName = p.Resident?.FullName ?? string.Empty,
                Name = p.Name,
                Species = p.Species,
                Breed = p.Breed,
                Age = p.Age,
                Color = p.Color,
                CreatedAt = p.CreatedAt
            });
    }

    public async Task<PetDto> CreatePetAsync(CreatePetRequest request)
    {
        var resident = await _residentRepository.GetByIdAsync(request.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        var pet = new Pet
        {
            Id = Guid.NewGuid(),
            ResidentId = request.ResidentId,
            Name = request.Name,
            Species = request.Species,
            Breed = request.Breed,
            Age = request.Age,
            Color = request.Color,
            CreatedAt = DateTime.UtcNow
        };

        await _petRepository.AddAsync(pet);

        return new PetDto
        {
            Id = pet.Id,
            ResidentId = pet.ResidentId,
            ResidentName = resident.FullName,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            Age = pet.Age,
            Color = pet.Color,
            CreatedAt = pet.CreatedAt
        };
    }

    public async Task<PetDto> UpdatePetAsync(Guid id, UpdatePetRequest request)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null)
        {
            throw new InvalidOperationException("Pet not found");
        }

        var resident = await _residentRepository.GetByIdAsync(request.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        pet.ResidentId = request.ResidentId;
        pet.Name = request.Name;
        pet.Species = request.Species;
        pet.Breed = request.Breed;
        pet.Age = request.Age;
        pet.Color = request.Color;

        await _petRepository.UpdateAsync(pet);

        return new PetDto
        {
            Id = pet.Id,
            ResidentId = pet.ResidentId,
            ResidentName = resident.FullName,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            Age = pet.Age,
            Color = pet.Color,
            CreatedAt = pet.CreatedAt
        };
    }

    public async Task<bool> DeletePetAsync(Guid id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null)
        {
            return false;
        }

        await _petRepository.DeleteAsync(pet);
        return true;
    }
}



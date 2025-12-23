using GreatSoft.Be.Application.DTOs.Pet;

namespace GreatSoft.Be.Application.Services;

public interface IPetService
{
    Task<IEnumerable<PetDto>> GetAllPetsAsync();
    Task<PetDto?> GetPetByIdAsync(Guid id);
    Task<IEnumerable<PetDto>> GetPetsByResidentIdAsync(Guid residentId);
    Task<PetDto> CreatePetAsync(CreatePetRequest request);
    Task<PetDto> UpdatePetAsync(Guid id, UpdatePetRequest request);
    Task<bool> DeletePetAsync(Guid id);
}



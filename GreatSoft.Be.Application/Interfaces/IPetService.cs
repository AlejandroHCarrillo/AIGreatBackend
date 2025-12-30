using GreatSoft.Be.Application.DTOs.Pet;

namespace GreatSoft.Be.Application.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetDto>> GetAllAsync();
    Task<PetDto?> GetByIdAsync(int id);
    Task<PetDto> CreateAsync(CreatePetDto createPetDto);
    Task<PetDto?> UpdateAsync(int id, UpdatePetDto updatePetDto);
    Task<bool> DeleteAsync(int id);
}


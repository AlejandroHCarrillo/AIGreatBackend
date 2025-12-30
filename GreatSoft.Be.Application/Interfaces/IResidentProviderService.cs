using GreatSoft.Be.Application.DTOs.ResidentProvider;

namespace GreatSoft.Be.Application.Interfaces;

public interface IResidentProviderService
{
    Task<IEnumerable<ResidentProviderDto>> GetAllAsync();
    Task<ResidentProviderDto?> GetByIdAsync(int id);
    Task<ResidentProviderDto> CreateAsync(CreateResidentProviderDto createResidentProviderDto);
    Task<ResidentProviderDto?> UpdateAsync(int id, UpdateResidentProviderDto updateResidentProviderDto);
    Task<bool> DeleteAsync(int id);
}


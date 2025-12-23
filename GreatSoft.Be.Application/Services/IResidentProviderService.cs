using GreatSoft.Be.Application.DTOs.ResidentProvider;

namespace GreatSoft.Be.Application.Services;

public interface IResidentProviderService
{
    Task<IEnumerable<ResidentProviderDto>> GetAllProvidersAsync();
    Task<ResidentProviderDto?> GetProviderByIdAsync(Guid id);
    Task<IEnumerable<ResidentProviderDto>> GetProvidersByServiceTypeAsync(Guid serviceTypeId);
    Task<ResidentProviderDto> CreateProviderAsync(CreateResidentProviderRequest request);
    Task<ResidentProviderDto> UpdateProviderAsync(Guid id, UpdateResidentProviderRequest request);
    Task<bool> DeleteProviderAsync(Guid id);
}



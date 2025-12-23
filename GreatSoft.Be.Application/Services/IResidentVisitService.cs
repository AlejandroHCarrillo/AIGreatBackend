using GreatSoft.Be.Application.DTOs.ResidentVisit;

namespace GreatSoft.Be.Application.Services;

public interface IResidentVisitService
{
    Task<IEnumerable<ResidentVisitDto>> GetAllVisitsAsync();
    Task<ResidentVisitDto?> GetVisitByIdAsync(Guid id);
    Task<IEnumerable<ResidentVisitDto>> GetVisitsByResidentIdAsync(Guid residentId);
    Task<ResidentVisitDto> CreateVisitAsync(CreateResidentVisitRequest request);
    Task<ResidentVisitDto> UpdateVisitAsync(Guid id, UpdateResidentVisitRequest request);
    Task<bool> DeleteVisitAsync(Guid id);
}



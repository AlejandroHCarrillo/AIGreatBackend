using GreatSoft.Be.Application.DTOs.ResidentVisit;

namespace GreatSoft.Be.Application.Interfaces;

public interface IResidentVisitService
{
    Task<IEnumerable<ResidentVisitDto>> GetAllAsync();
    Task<ResidentVisitDto?> GetByIdAsync(int id);
    Task<ResidentVisitDto> CreateAsync(CreateResidentVisitDto createResidentVisitDto);
    Task<ResidentVisitDto?> UpdateAsync(int id, UpdateResidentVisitDto updateResidentVisitDto);
    Task<bool> DeleteAsync(int id);
}


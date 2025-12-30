using GreatSoft.Be.Application.DTOs.Company;

namespace GreatSoft.Be.Application.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<CompanyDto?> GetByIdAsync(int id);
    Task<CompanyDto> CreateAsync(CreateCompanyDto createCompanyDto);
    Task<CompanyDto?> UpdateAsync(int id, UpdateCompanyDto updateCompanyDto);
    Task<bool> DeleteAsync(int id);
}


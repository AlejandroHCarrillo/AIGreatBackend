using GreatSoft.Be.Application.DTOs.Company;

namespace GreatSoft.Be.Application.Services;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
    Task<CompanyDto?> GetCompanyByIdAsync(Guid id);
    Task<CompanyDto> CreateCompanyAsync(CreateCompanyRequest request);
    Task<CompanyDto> UpdateCompanyAsync(Guid id, UpdateCompanyRequest request);
    Task<bool> DeleteCompanyAsync(Guid id);
}



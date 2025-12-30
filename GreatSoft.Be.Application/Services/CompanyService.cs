using GreatSoft.Be.Application.DTOs.Company;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(MapToDto);
    }

    public async Task<CompanyDto?> GetByIdAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null) return null;
        return MapToDto(company);
    }

    public async Task<CompanyDto> CreateAsync(CreateCompanyDto createCompanyDto)
    {
        var company = new Company
        {
            Name = createCompanyDto.Name,
            Address = createCompanyDto.Address,
            Phone = createCompanyDto.Phone,
            Email = createCompanyDto.Email,
            TaxId = createCompanyDto.TaxId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _companyRepository.AddAsync(company);
        return MapToDto(company);
    }

    public async Task<CompanyDto?> UpdateAsync(int id, UpdateCompanyDto updateCompanyDto)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null) return null;

        company.Name = updateCompanyDto.Name;
        company.Address = updateCompanyDto.Address;
        company.Phone = updateCompanyDto.Phone;
        company.Email = updateCompanyDto.Email;
        company.TaxId = updateCompanyDto.TaxId;
        company.IsActive = updateCompanyDto.IsActive;
        company.UpdatedAt = DateTime.UtcNow;

        await _companyRepository.UpdateAsync(company);
        return MapToDto(company);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null) return false;

        await _companyRepository.DeleteAsync(company);
        return true;
    }

    private static CompanyDto MapToDto(Company company)
    {
        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            Phone = company.Phone,
            Email = company.Email,
            TaxId = company.TaxId,
            IsActive = company.IsActive,
            CreatedAt = company.CreatedAt
        };
    }
}


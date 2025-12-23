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

    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(c => new CompanyDto
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address,
            ContactName = c.ContactName,
            Phone = c.Phone,
            Email = c.Email,
            CreatedAt = c.CreatedAt
        });
    }

    public async Task<CompanyDto?> GetCompanyByIdAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null) return null;

        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            ContactName = company.ContactName,
            Phone = company.Phone,
            Email = company.Email,
            CreatedAt = company.CreatedAt
        };
    }

    public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyRequest request)
    {
        if (await _companyRepository.GetByNameAsync(request.Name) != null)
        {
            throw new InvalidOperationException("Company name already exists");
        }

        if (await _companyRepository.GetByEmailAsync(request.Email) != null)
        {
            throw new InvalidOperationException("Company email already exists");
        }

        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Address = request.Address,
            ContactName = request.ContactName,
            Phone = request.Phone,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        await _companyRepository.AddAsync(company);

        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            ContactName = company.ContactName,
            Phone = company.Phone,
            Email = company.Email,
            CreatedAt = company.CreatedAt
        };
    }

    public async Task<CompanyDto> UpdateCompanyAsync(Guid id, UpdateCompanyRequest request)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
        {
            throw new InvalidOperationException("Company not found");
        }

        var existingCompanyByName = await _companyRepository.GetByNameAsync(request.Name);
        if (existingCompanyByName != null && existingCompanyByName.Id != id)
        {
            throw new InvalidOperationException("Company name already exists");
        }

        var existingCompanyByEmail = await _companyRepository.GetByEmailAsync(request.Email);
        if (existingCompanyByEmail != null && existingCompanyByEmail.Id != id)
        {
            throw new InvalidOperationException("Company email already exists");
        }

        company.Name = request.Name;
        company.Address = request.Address;
        company.ContactName = request.ContactName;
        company.Phone = request.Phone;
        company.Email = request.Email;

        await _companyRepository.UpdateAsync(company);

        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            ContactName = company.ContactName,
            Phone = company.Phone,
            Email = company.Email,
            CreatedAt = company.CreatedAt
        };
    }

    public async Task<bool> DeleteCompanyAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
        {
            return false;
        }

        await _companyRepository.DeleteAsync(company);
        return true;
    }
}



using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface ICompanyUserRepository
{
    Task<CompanyUser> AddAsync(CompanyUser companyUser);
    Task<bool> ExistsAsync(Guid companyId, Guid userId);
}



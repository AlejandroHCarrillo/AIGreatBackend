using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class CompanyUserRepository : ICompanyUserRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CompanyUser> AddAsync(CompanyUser companyUser)
    {
        await _context.CompanyUsers.AddAsync(companyUser);
        await _context.SaveChangesAsync();
        return companyUser;
    }

    public async Task<bool> ExistsAsync(Guid companyId, Guid userId)
    {
        return await _context.CompanyUsers
            .AnyAsync(cu => cu.CompanyId == companyId && cu.UserId == userId);
    }
}



using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface ICommunityRepository : IRepository<Community>
{
    Task<IEnumerable<Community>> GetByCompanyIdAsync(int companyId);
    Task<Community?> GetByIdWithDetailsAsync(int id);
}


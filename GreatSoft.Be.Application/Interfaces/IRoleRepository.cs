using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
}


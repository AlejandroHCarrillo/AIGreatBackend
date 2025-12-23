using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface ICommunityRepository : IRepository<Community>
{
    Task<Community?> GetByNameAsync(string name);
}



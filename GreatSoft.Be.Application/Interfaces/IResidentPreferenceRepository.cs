using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IResidentPreferenceRepository : IRepository<ResidentPreference>
{
    Task<IEnumerable<ResidentPreference>> GetByResidentIdAsync(int residentId);
    Task<ResidentPreference?> GetByResidentIdAndNameAsync(int residentId, string name);
}



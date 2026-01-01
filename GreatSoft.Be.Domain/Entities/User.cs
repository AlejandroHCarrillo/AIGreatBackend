using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int RoleId { get; set; }
    public int? CompanyId { get; set; }

    // Navigation properties
    public virtual Role Role { get; set; } = null!;
    public virtual Company? Company { get; set; }
    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual ICollection<ResidentVisit> ResidentVisits { get; set; } = new List<ResidentVisit>();
    public virtual ICollection<ResidentPreference> ResidentPreferences { get; set; } = new List<ResidentPreference>();
}


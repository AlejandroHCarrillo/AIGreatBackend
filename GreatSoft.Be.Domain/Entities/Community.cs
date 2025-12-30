using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class Community : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public int CompanyId { get; set; }

    // Navigation properties
    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual ICollection<ResidentProvider> ResidentProviders { get; set; } = new List<ResidentProvider>();
    public virtual ICollection<ResidentVisit> ResidentVisits { get; set; } = new List<ResidentVisit>();
    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}


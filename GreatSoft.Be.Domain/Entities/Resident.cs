namespace GreatSoft.Be.Domain.Entities;

public class Resident
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string HouseNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid CommunityId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public Community Community { get; set; } = null!;
    public Role Role { get; set; } = null!;
    
    // ResidentUser relationship (one-to-one)
    public ResidentUser? ResidentUser { get; set; }
    
    // Vehicles relationship (one-to-many)
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    
    // Pets relationship (one-to-many)
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    
    // Visits relationship (one-to-many)
    public ICollection<ResidentVisit> Visits { get; set; } = new List<ResidentVisit>();
}


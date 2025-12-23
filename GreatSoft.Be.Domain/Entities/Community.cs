namespace GreatSoft.Be.Domain.Entities;

public class Community
{
    public Guid Id { get; set; }
    public Guid CommunityTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Lat { get; set; }
    public int Lng { get; set; }
    public int HousingCount { get; set; }
    public string ContactPhone { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public CommunityType CommunityType { get; set; } = null!;
    public ICollection<Resident> Residents { get; set; } = new List<Resident>();
}


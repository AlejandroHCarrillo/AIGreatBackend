namespace GreatSoft.Be.Domain.Entities;

public class CommunityType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // COLONIA, FRACCIONAMIENTO, etc.
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public ICollection<Community> Communities { get; set; } = new List<Community>();
}


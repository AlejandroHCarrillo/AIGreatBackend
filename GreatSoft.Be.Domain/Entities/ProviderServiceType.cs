namespace GreatSoft.Be.Domain.Entities;

public class ProviderServiceType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // COMIDA, ASEO, JARDINERIA, etc.
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public ICollection<ResidentProvider> Providers { get; set; } = new List<ResidentProvider>();
}



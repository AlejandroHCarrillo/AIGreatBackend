namespace GreatSoft.Be.Domain.Entities;

public class VehicleType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // AUTO, MOTOCICLETA, etc.
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}



namespace GreatSoft.Be.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public Guid ResidentId { get; set; }
    public string Brand { get; set; } = string.Empty; // Marca
    public Guid VehicleTypeId { get; set; }
    public string Model { get; set; } = string.Empty; // Modelo
    public int Year { get; set; } // Año
    public string Color { get; set; } = string.Empty; // Color
    public string LicensePlate { get; set; } = string.Empty; // Placas
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public Resident Resident { get; set; } = null!;
    public VehicleType VehicleType { get; set; } = null!;
}



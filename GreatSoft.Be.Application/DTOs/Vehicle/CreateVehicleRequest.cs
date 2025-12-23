namespace GreatSoft.Be.Application.DTOs.Vehicle;

public class CreateVehicleRequest
{
    public Guid ResidentId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public Guid VehicleTypeId { get; set; }
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
}



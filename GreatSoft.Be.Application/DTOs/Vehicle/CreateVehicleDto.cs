namespace GreatSoft.Be.Application.DTOs.Vehicle;

public class CreateVehicleDto
{
    public string LicensePlate { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public int? Year { get; set; }
    public int CommunityId { get; set; }
    public int OwnerId { get; set; }
}


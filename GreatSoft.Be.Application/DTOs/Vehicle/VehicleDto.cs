namespace GreatSoft.Be.Application.DTOs.Vehicle;

public class VehicleDto
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public int? Year { get; set; }
    public int CommunityId { get; set; }
    public string CommunityName { get; set; } = string.Empty;
    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}


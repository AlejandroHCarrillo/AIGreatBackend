namespace GreatSoft.Be.Application.DTOs.Vehicle;

public class UpdateVehicleDto
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public int? Year { get; set; }
    public bool IsActive { get; set; }
}


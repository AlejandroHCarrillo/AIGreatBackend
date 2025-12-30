namespace GreatSoft.Be.Application.DTOs.Amenity;

public class UpdateAmenityDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Rules { get; set; }
    public decimal? Cost { get; set; }
    public string? Image { get; set; }
    public int? MaxCapacity { get; set; }
    public int? SimultaneousReservations { get; set; }
    public bool IsActive { get; set; }
}


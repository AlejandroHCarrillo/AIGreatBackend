namespace GreatSoft.Be.Application.DTOs.Amenity;

public class CreateAmenityDto
{
    public int CommunityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Rules { get; set; }
    public decimal? Cost { get; set; }
    public string? Image { get; set; }
    public int? MaxCapacity { get; set; }
    public int? SimultaneousReservations { get; set; }
}


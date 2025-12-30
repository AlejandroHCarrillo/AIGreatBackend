namespace GreatSoft.Be.Application.DTOs.Amenity;

public class AmenityDto
{
    public int Id { get; set; }
    public int CommunityId { get; set; }
    public string CommunityName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Rules { get; set; }
    public decimal? Cost { get; set; }
    public string? Image { get; set; }
    public int? MaxCapacity { get; set; }
    public int? SimultaneousReservations { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}


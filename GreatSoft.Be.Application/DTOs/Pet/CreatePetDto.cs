namespace GreatSoft.Be.Application.DTOs.Pet;

public class CreatePetDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public int CommunityId { get; set; }
    public int OwnerId { get; set; }
}


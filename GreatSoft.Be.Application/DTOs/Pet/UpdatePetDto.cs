namespace GreatSoft.Be.Application.DTOs.Pet;

public class UpdatePetDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
}


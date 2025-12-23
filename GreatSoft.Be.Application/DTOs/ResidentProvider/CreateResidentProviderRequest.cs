namespace GreatSoft.Be.Application.DTOs.ResidentProvider;

public class CreateResidentProviderRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ProviderServiceTypeId { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Image { get; set; }
}



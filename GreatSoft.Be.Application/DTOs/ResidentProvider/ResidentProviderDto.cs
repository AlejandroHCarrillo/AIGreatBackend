namespace GreatSoft.Be.Application.DTOs.ResidentProvider;

public class ResidentProviderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ProviderServiceTypeId { get; set; }
    public string ProviderServiceTypeName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
}



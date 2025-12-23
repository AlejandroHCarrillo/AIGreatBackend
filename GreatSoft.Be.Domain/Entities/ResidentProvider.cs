namespace GreatSoft.Be.Domain.Entities;

public class ResidentProvider
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Nombre
    public string Description { get; set; } = string.Empty; // Descripción
    public Guid ProviderServiceTypeId { get; set; } // Tipo de Servicio
    public string Phone { get; set; } = string.Empty; // Teléfono
    public string Email { get; set; } = string.Empty; // Email
    public string? Image { get; set; } // Imagen (ruta de la imagen)
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public ProviderServiceType ProviderServiceType { get; set; } = null!;
}



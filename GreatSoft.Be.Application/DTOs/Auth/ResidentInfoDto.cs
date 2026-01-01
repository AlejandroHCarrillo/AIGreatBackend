namespace GreatSoft.Be.Application.DTOs.Auth;

public class ResidentInfoDto
{
    public string? Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Number { get; set; } // HouseNumber
    public string Address { get; set; } = string.Empty;
    public List<string>? Comunidades { get; set; } // Lista de nombres de comunidades
}


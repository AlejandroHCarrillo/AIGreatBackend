namespace GreatSoft.Be.Application.DTOs.ResidentPreference;

public class CreateResidentPreferenceDto
{
    public int ResidentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}



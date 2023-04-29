namespace Application.DTO.Profile;

public class ProfileCreateDto
{
    public bool IsMentor { get; set; } = false;
    public bool IsBusy { get; set; } = false;
    public bool OnVacation { get; set; } = false;
    
    public SettingsDto Settings { get; set; } = null!;
    public List<Guid> AttacheIds { get; set; } = null!;
}
namespace Application.DTO.Profile;

public class ProfileDto
{
    public Guid Id { get; set; }
    public bool IsMentor { get; set; }
    public bool IsBusy { get; set; }
    public bool OnVacation { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Guid UserId { get; set; }
    public SettingsDto Settings { get; set; } = null!;
    public List<Guid> AttacheIds { get; set; } = null!;
}
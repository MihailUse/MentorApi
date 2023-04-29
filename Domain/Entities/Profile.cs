namespace Domain.Entities;

public class Profile
{
    public Guid Id { get; set; }
    public bool IsMentor { get; set; }
    public bool IsBusy { get; set; }
    public bool OnVacation { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
    public Settings Settings { get; set; } = null!;
    public List<Attach> Attaches { get; set; } = null!;
}
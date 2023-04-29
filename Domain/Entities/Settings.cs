namespace Domain.Entities;

public class Settings
{
    public Guid Id { get; set; }
    public bool InMailingList { get; set; }
    public bool MobilePushNotification { get; set; }

    public Guid ProfileId { get; set; }

    public Profile Profile { get; set; } = null!;
}
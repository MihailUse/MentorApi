namespace Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Guid ChatId { get; set; }
    public Guid AuthorId { get; set; }

    public Chat Chat { get; set; } = null!;
    public User Author { get; set; } = null!;
}
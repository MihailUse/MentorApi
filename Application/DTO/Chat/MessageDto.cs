using Application.DTO.User;

namespace Application.DTO.Chat;

public class MessageDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public Guid ChatId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public UserBriefDto Author { get; set; } = null!;
}
namespace Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    public Guid? LogoId { get; set; }

    public Attach? Logo { get; set; }
    public List<User> Users { get; set; } = null!;
    public List<Message> Messages { get; set; } = null!;
}
namespace Application.DTO.Chat;

public class ChatDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid? LogoId { get; set; }
}
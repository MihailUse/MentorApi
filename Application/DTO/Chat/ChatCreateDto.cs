namespace Application.DTO.Chat;

public class ChatCreateDto
{
    public string Title { get; set; } = null!;
    public Guid? LogoId { get; set; }
}
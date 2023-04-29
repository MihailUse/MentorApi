namespace Application.DTO.User;

public class UserBriefDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public Guid? AvatarId { get; set; }
}
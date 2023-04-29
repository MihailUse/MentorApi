namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? About { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    public Guid? AvatarId { get; set; }

    public Attach? Avatar { get; set; }
    public List<Chat> Chats { get; set; } = null!;
    public List<Profile> Profiles { get; set; } = null!;
}
using Application.DTO.Profile;

namespace Application.DTO.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? About { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Guid? AvatarId { get; set; }
    public List<ProfileDto> Profiles { get; set; } = null!;
}
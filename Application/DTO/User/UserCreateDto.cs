namespace Application.DTO.User;

public class UserCreateDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
}
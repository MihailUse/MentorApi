using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Configs;

public class AuthConfig
{
    public const string Position = nameof(AuthConfig);
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int LifeTime { get; set; }
    public int RefreshLifeTime { get; set; }

    public SymmetricSecurityKey SymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}
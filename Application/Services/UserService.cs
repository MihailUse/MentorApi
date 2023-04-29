using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Configs;
using Application.DTO.Common;
using Application.DTO.User;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Interfaces;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IDatabaseContext _database;
    private readonly AuthConfig _authConfig;

    public UserService(IMapper mapper, IDatabaseContext database, IOptions<AuthConfig> authConfig)
    {
        _mapper = mapper;
        _database = database;
        _authConfig = authConfig.Value;
    }

    public async Task<PaginatedList<UserBriefDto>> Search(UserSearchDto searchDto)
    {
        IQueryable<User> query = _database.Users;

        if (string.IsNullOrEmpty(searchDto.Login))
            query = query.Where(x => EF.Functions.ILike(x.Login, $"%{searchDto.Login}%"));

        return await query.ProjectToPaginatedList<User, UserBriefDto>(_mapper.ConfigurationProvider, searchDto);
    }

    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _database.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == default)
            throw new NotFoundException("User not Found");

        return user;
    }

    public async Task<Guid> Create(UserCreateDto createDto)
    {
        var user = _mapper.Map<User>(createDto);

        var isExists = await _database.Users.AnyAsync(x => x.Login == user.Login);
        if (isExists)
            throw new ConflictException("Login already exists");

        await _database.Users.AddAsync(user);
        await _database.SaveChangesAsync();
        return user.Id;
    }

    public async Task<string> Authorize(UserAuthorizeDto authorizeDto)
    {
        var user = await _database.Users.FirstOrDefaultAsync(x => x.Login == authorizeDto.Login);
        if (user == default)
            throw new NotFoundException("User not Found");

        if (!Argon2.Verify(user.PasswordHash, authorizeDto.Password))
            throw new ForbiddenException("Invalid password");

        var tokenClaims = new[]
        {
            new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Login),
        };

        return GenerateEncodedToken(tokenClaims, _authConfig.LifeTime);
    }

    public async Task Update(Guid id, UserUpdateDto updateDto)
    {
        var user = await _database.Users.FindAsync(id);
        if (user == default)
            throw new NotFoundException("User not Found");

        user = _mapper.Map(updateDto, user);

        _database.Users.Update(user);
        await _database.SaveChangesAsync();
    }

    private string GenerateEncodedToken(IEnumerable<Claim> claims, int lifeTime)
    {
        var dateTime = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: _authConfig.Issuer,
            audience: _authConfig.Audience,
            claims: claims,
            notBefore: dateTime,
            expires: dateTime.AddMinutes(lifeTime),
            signingCredentials: new SigningCredentials(_authConfig.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
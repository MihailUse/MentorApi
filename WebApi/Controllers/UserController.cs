using Application.DTO.Common;
using Application.DTO.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("API/[controller]/")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<UserDto> GetById(Guid userId)
    {
        return await _userService.GetById(userId);
    }

    [HttpGet("search")]
    public async Task<PaginatedList<UserBriefDto>> Search([FromQuery] UserSearchDto searchDto)
    {
        return await _userService.Search(searchDto);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<Guid> Create(UserCreateDto createDto)
    {
        return await _userService.Create(createDto);
    }

    [AllowAnonymous]
    [HttpPost("Authorize")]
    public async Task<string> Authorize(UserAuthorizeDto authorizeDto)
    {
        return await _userService.Authorize(authorizeDto);
    }

    [HttpPut("{id:guid}")]
    public async Task Update(Guid id, UserUpdateDto updateDto)
    {
        await _userService.Update(id, updateDto);
    }
}
using Application.DTO.Profile;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("API/[controller]/")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost]
    public async Task CreateProfile(ProfileCreateDto createDto)
    {
        await _profileService.Create(createDto);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateProfile(Guid id, ProfileUpdateDto updateDto)
    {
        await _profileService.Update(id, updateDto);
    }
}
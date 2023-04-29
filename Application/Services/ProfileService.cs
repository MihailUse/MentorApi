using Application.DTO.Profile;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Profile = Domain.Entities.Profile;

namespace Application.Services;

public class ProfileService : IProfileService
{
    private readonly IMapper _mapper;
    private readonly IDatabaseContext _database;
    private readonly User _currentUser;

    public ProfileService(IMapper mapper, IDatabaseContext database, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _database = database;
        _currentUser = currentUserService.User;
    }

    public async Task Create(ProfileCreateDto createDto)
    {
        var profile = _mapper.Map<Profile>(createDto);
        profile.Settings = new Settings();
        profile.UserId = _currentUser.Id;

        var attaches = await _database.Attaches
            .Where(x => createDto.AttacheIds.Contains(x.Id))
            .ToListAsync();
        if (attaches.Count != createDto.AttacheIds.Count)
            throw new NotFoundException("Some attaches not found");

        profile.Attaches = attaches;
        await _database.Profiles.AddAsync(profile);
        await _database.SaveChangesAsync();
    }

    public async Task Update(Guid id, ProfileUpdateDto updateDto)
    {
        var profile = await _database.Profiles.FindAsync(id);
        if (profile == default)
            throw new NotFoundException("Profile not found");

        profile = _mapper.Map(updateDto, profile);
        profile.UpdatedAt = DateTimeOffset.UtcNow;

        var attaches = await _database.Attaches
            .Where(x => updateDto.AttacheIds.Contains(x.Id))
            .ToListAsync();
        if (attaches.Count != updateDto.AttacheIds.Count)
            throw new NotFoundException("Some attaches not found");

        profile.Attaches = attaches;

        _database.Profiles.Update(profile);
        await _database.SaveChangesAsync();
    }
}
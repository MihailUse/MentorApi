using Application.DTO.Profile;

namespace Application.Interfaces;

public interface IProfileService
{
    Task Create(ProfileCreateDto createDto);
    Task Update(Guid id, ProfileUpdateDto updateDto);
}
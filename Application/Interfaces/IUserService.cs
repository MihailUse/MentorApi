using Application.DTO.Common;
using Application.DTO.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<PaginatedList<UserBriefDto>> Search(UserSearchDto searchDto);
    Task<UserDto> GetById(Guid id);
    Task<Guid> Create(UserCreateDto createDto);
    Task<string> Authorize(UserAuthorizeDto authorizeDto);
    Task Update(Guid id, UserUpdateDto updateDto);
}
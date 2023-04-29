using Application.DTO.Common;

namespace Application.DTO.User;

public class UserSearchDto : PaginatedListQuery
{
    public string? Login { get; set; }
}
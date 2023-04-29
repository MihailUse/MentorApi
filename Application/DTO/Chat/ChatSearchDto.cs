using Application.DTO.Common;

namespace Application.DTO.Chat;

public class ChatSearchDto : PaginatedListQuery
{
    public string? Title { get; set; }
}
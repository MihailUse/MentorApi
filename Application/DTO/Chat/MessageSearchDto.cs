using Application.DTO.Common;

namespace Application.DTO.Chat;

public class MessageSearchDto : PaginatedListQuery
{
    public string? Text { get; set; }
}
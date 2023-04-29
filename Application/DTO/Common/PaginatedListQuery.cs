namespace Application.DTO.Common;

public class PaginatedListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
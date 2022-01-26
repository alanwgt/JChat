namespace JChat.Application.Shared.CQRS;

public class PaginationData
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
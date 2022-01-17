using JChat.Application.Shared.Models;
using MediatR;

namespace JChat.Application.Shared.CQRS;

public class PaginatedQuery<T> : IRequest<PaginatedList<T>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
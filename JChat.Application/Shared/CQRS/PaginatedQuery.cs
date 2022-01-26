using JChat.Application.Shared.Models;
using MediatR;

namespace JChat.Application.Shared.CQRS;

public class PaginatedQuery<T> : PaginationData, IRequest<PaginatedList<T>>
{
}
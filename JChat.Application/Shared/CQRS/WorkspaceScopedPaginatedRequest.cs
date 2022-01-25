using JChat.Application.Shared.Interfaces;
using JChat.Domain.Interfaces;
using MediatR;

namespace JChat.Application.Shared.CQRS;

public class WorkspaceScopedPaginatedRequest<TRequest> : PaginatedQuery<TRequest>, IRequest<TRequest>, IHasUserSetter,
    IHasWorkspaceIdSetter
{
    public IUser User { get; set; }

    public Guid WorkspaceId { get; set; }
}
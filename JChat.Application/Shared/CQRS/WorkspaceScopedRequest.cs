using JChat.Application.Shared.Interfaces;

namespace JChat.Application.Shared.CQRS;

public class WorkspaceScopedRequest<TRequest> : BaseRequest<TRequest>, IHasWorkspaceIdSetter
{
    public Guid WorkspaceId { get; set; }
}
using JChat.Application.Enums;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Interfaces;

namespace JChat.Application.Extensions;

public static class AuthorizationExtensions
{
    public static Task<bool> CanReadWorkspace(this IAuthorizationService service, IUser user, Guid workspaceId,
        CancellationToken cancellationToken = new())
        => service.Can(AuthzNamespace.Workspaces.Str(), workspaceId.ToString(), AuthzRelation.Read.Str(),
            user.Id.ToString(),
            cancellationToken);

    public static Task<bool> CanWriteIntoWorkspace(this IAuthorizationService service, IUser user, Guid workspaceId,
        CancellationToken cancellationToken = new())
        => service.Can(AuthzNamespace.Workspaces.Str(), workspaceId.ToString(), AuthzRelation.Write.Str(),
            user.Id.ToString(),
            cancellationToken);

    public static Task<bool> CanManageWorkspace(this IAuthorizationService service, IUser user, Guid workspaceId,
        CancellationToken cancellationToken = new())
        => service.Can(AuthzNamespace.Workspaces.Str(), workspaceId.ToString(), AuthzRelation.Manage.Str(),
            user.Id.ToString(),
            cancellationToken);

    public static Task<bool> IsWorkspaceOwner(this IAuthorizationService service, IUser user, Guid workspaceId,
        CancellationToken cancellationToken = new())
        => service.Can(AuthzNamespace.Workspaces.Str(), workspaceId.ToString(), AuthzRelation.Ownership.Str(),
            user.Id.ToString(),
            cancellationToken);
}
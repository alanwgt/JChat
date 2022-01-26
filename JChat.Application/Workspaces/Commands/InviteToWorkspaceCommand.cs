using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using JChat.Domain.Entities.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;

namespace JChat.Application.Workspaces.Commands;

[Authorize(Namespace = "workspaces", ObjectIdFromProperty = "WorkspaceId", Relation = "manage")]
public class InviteToWorkspaceCommand : WorkspaceScopedRequest<Unit>
{
    public Guid UserId { get; set; }
}

public class InviteToWorkspaceCommandHandler : IRequestHandler<InviteToWorkspaceCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public InviteToWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(InviteToWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var userInvites = await _context.UserWorkspaces
            .Where(uw => uw.UserId == request.UserId)
            .Where(uw => uw.WorkspaceId == request.WorkspaceId)
            .ToListAsync(cancellationToken);

        if (userInvites.Any())
        {
            if (userInvites.Any(ui => ui.RejectedAt != null))
                throw new ApplicationException { Details = "workspace.invitations.rejected_invitation" };

            if (userInvites.Any(ui => ui.AcceptedAt == null))
                throw new ApplicationException { Details = "workspace.invitations.has_open_invitation" };
        }

        // TODO: send notification
        var workspace = await _context.Workspaces.FindAsync(new object?[] { request.WorkspaceId }, cancellationToken) ??
                        throw new NotFoundException("workspace", request.WorkspaceId);

        var invite = workspace.AddMember(request.UserId);
        invite.AcceptInvitation();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
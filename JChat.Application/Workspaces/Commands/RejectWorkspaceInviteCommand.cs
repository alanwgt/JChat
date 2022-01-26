using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Workspaces.Commands;

public class RejectWorkspaceInviteCommand : BaseRequest<Unit>
{
    public Guid InvitationId { get; set; }
}

public class RejectWorkspaceInviteCommandHandler : IRequestHandler<RejectWorkspaceInviteCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public RejectWorkspaceInviteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RejectWorkspaceInviteCommand request, CancellationToken cancellationToken)
    {
        var userWorkspace = await _context.UserWorkspaces
            .Where(uw => uw.UserId == request.User.Id)
            .Where(uw => uw.Id == request.InvitationId)
            .SingleAsync(cancellationToken);

        userWorkspace.RejectInvitation();
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
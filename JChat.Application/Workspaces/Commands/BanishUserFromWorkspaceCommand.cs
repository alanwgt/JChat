using FluentValidation;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Workspaces.Commands;

[Authorize(Namespace = "workspaces", ObjectIdFromProperty = "WorkspaceId", Relation = "manage")]
public class BanishUserFromWorkspaceCommand : WorkspaceScopedRequest<Unit>
{
    public Guid UserId { get; set; }
    public string Reason { get; set; }
}

public class BanishUserFromWorkspaceCommandValidator : AbstractValidator<BanishUserFromWorkspaceCommand>
{
    public BanishUserFromWorkspaceCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();
        RuleFor(c => c.Reason)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(300);
    }
}

public class BanUserFromWorkspaceCommandHandler : IRequestHandler<BanishUserFromWorkspaceCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public BanUserFromWorkspaceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(BanishUserFromWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var userWorkspace = await _context.UserWorkspaces
            .Where(uw => uw.WorkspaceId == request.WorkspaceId)
            .Where(uw => uw.UserId == request.UserId)
            .SingleAsync(cancellationToken);

        userWorkspace.Banish(request.User.Id, request.Reason);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
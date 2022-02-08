using AutoMapper;
using JChat.Application.Enums;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Workspaces.Queries;
using JChat.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Workspaces.Commands;

public class AcceptWorkspaceInviteCommand : BaseRequest<WorkspaceBriefDto>
{
    public Guid InvitationId { get; set; }
}

public class
    AcceptWorkspaceInvitationCommandHandler : IRequestHandler<AcceptWorkspaceInviteCommand, WorkspaceBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorization;
    private readonly IDomainEventService _eventService;

    public AcceptWorkspaceInvitationCommandHandler(IApplicationDbContext context, IMapper mapper,
        IAuthorizationService authorization, IDomainEventService eventService)
    {
        _context = context;
        _mapper = mapper;
        _authorization = authorization;
        _eventService = eventService;
    }

    public async Task<WorkspaceBriefDto> Handle(AcceptWorkspaceInviteCommand request,
        CancellationToken cancellationToken)
    {
        var userWorkspace = await _context.UserWorkspaces
            .Where(uw => uw.UserId == request.User.Id)
            .Where(uw => uw.Id == request.InvitationId)
            .Include(uw => uw.Workspace)
            .SingleAsync(cancellationToken);

        userWorkspace.AcceptInvitation();

        await _authorization.Authorize(
            AuthzNamespace.Workspaces.Str(),
            userWorkspace.WorkspaceId.ToString(),
            AuthzRelation.Member.Str(),
            userWorkspace.UserId.ToString(),
            cancellationToken
        );

        // TODO: send notification
        await _context.SaveChangesAsync(cancellationToken);
        await _eventService.Publish(new UserJoinedWorkspaceEvent(request.User.Id, userWorkspace.WorkspaceId));

        return _mapper.Map<WorkspaceBriefDto>(userWorkspace.Workspace);
    }
}
using JChat.Application.Enums;
using JChat.Application.Extensions;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using JChat.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Channels.Commands;

[Authorize(Namespace = "channels", ObjectIdFromProperty = "ChannelId", Relation = "ownership")]
public class DeleteChannelCommand : ChannelScopedRequest<Unit>
{
}

public class DeleteChannelCommandHandler : IRequestHandler<DeleteChannelCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthorizationService _authorization;
    private readonly IDomainEventService _eventService;

    public DeleteChannelCommandHandler(IApplicationDbContext context, IAuthorizationService authorization,
        IDomainEventService eventService)
    {
        _context = context;
        _authorization = authorization;
        _eventService = eventService;
    }

    public async Task<Unit> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
    {
        var members = await _context.ChannelUsers
            .Where(cu => cu.ChannelId == request.ChannelId)
            .ToListAsync(cancellationToken);
        var channel = await _context.Channels.FindAsync_(request.ChannelId, cancellationToken);

        foreach (var member in members)
        {
            string relation;

            if (member.IsAdmin)
                relation = AuthzRelation.Manage.Str();
            else if (channel.CreatedById == member.UserId)
                relation = AuthzRelation.Ownership.Str();
            else
                relation = AuthzRelation.Member.Str();

            await _authorization.RemoveAuthorization(
                AuthzNamespace.Channels.Str(),
                request.ChannelId.ToString(),
                relation,
                member.UserId.ToString(),
                cancellationToken
            );
        }

        await _eventService.Publish(new ChannelDeletedEvent(request.ChannelId));

        _context.ChannelUsers.RemoveRange(members);
        _context.Channels.Remove(channel);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
using JChat.Application.Extensions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Channels.EventHandlers;

public class ChannelDeletedEventHandler : INotificationHandler<DomainEventNotification<ChannelDeletedEvent>>
{
    private readonly INotificationCenter _notificationCenter;
    private readonly IApplicationDbContext _context;

    public ChannelDeletedEventHandler(INotificationCenter notificationCenter, IApplicationDbContext context)
    {
        _notificationCenter = notificationCenter;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<ChannelDeletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var channelId = domainEvent.ChannelId;
        var channel = await _context.Channels.FindAsync_(channelId, cancellationToken);
        var members = await _context.ChannelUsers
            .Where(cu => cu.ChannelId == channelId)
            .ToListAsync(cancellationToken);
        var promises = members
            .Select(member => _notificationCenter.ChannelDeleted(member.UserId, channelId, channel.Name))
            .ToArray();

        Task.WaitAll(promises, cancellationToken);
    }
}
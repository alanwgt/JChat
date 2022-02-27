using System.Text.Json;
using AutoMapper;
using JChat.Application.Channels.Queries;
using JChat.Application.Extensions;
using JChat.Application.Notifications.Queries;
using JChat.Application.Shared.Constants;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Entities.Message;
using JChat.Domain.Entities.Notifications;
using JChat.Domain.Enums;
using JChat.Domain.Events;
using MediatR;

namespace JChat.Application.Channels.EventHandlers;

public class UserAddedToChannelEventHandler : INotificationHandler<DomainEventNotification<UserAddedToChannel>>
{
    private readonly INotificationCenter _notificationCenter;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserAddedToChannelEventHandler(INotificationCenter notificationCenter, IApplicationDbContext context,
        IMapper mapper)
    {
        _notificationCenter = notificationCenter;
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(DomainEventNotification<UserAddedToChannel> notification,
        CancellationToken cancellationToken)
    {
        var (channelId, addedById, userId) = notification.DomainEvent;
        var user = await _context.Users.FindAsync_(userId, cancellationToken);
        var channel = await _context.Channels.FindAsync_(channelId, cancellationToken);
        var addedBy = await _context.Users.FindAsync_(addedById, cancellationToken);
        var notificationEntity = new Notification(addedById, userId, NotificationType.UserJoinedChannel,
            _mapper.Map<ChannelBriefDto>(channel));
        var message = new Message(
            MessageBodyTypeId.ChannelEvent,
            MessagePriorityId.Normal,
            "channels.user.joined",
            JsonSerializer.Serialize(new
                { addedById, userId, addedBy = addedBy.Username, username = user.Username })
        );
        var messageProjection = MessageProjection.From(message, channelId, addedBy!);

        await _context.Notifications.AddAsync(notificationEntity, cancellationToken);
        await _context.MessageProjections.AddAsync(messageProjection, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _notificationCenter.SendUserNotification(userId, _mapper.Map<NotificationDto>(notificationEntity));
    }
}
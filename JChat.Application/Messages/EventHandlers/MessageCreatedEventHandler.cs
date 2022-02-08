using JChat.Application.Notifications.Queries;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Enums;
using JChat.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Messages.EventHandlers;

public class MessageCreatedEventHandler : INotificationHandler<DomainEventNotification<MessageCreatedEvent>>
{
    private readonly INotificationCenter _notificationCenter;
    private readonly IApplicationDbContext _context;

    public MessageCreatedEventHandler(INotificationCenter notificationCenter, IApplicationDbContext context)
    {
        _notificationCenter = notificationCenter;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<MessageCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var (channelId, messageId, recipients) = notification.DomainEvent;
        var messages = await _context.MessageProjections
            .Include(mp => mp.Sender)
            .Where(mp => mp.ChannelId == channelId)
            .Where(mp => mp.MessageId == messageId)
            .Where(mp => mp.RecipientId.HasValue && recipients.Contains(mp.RecipientId.Value))
            .ToListAsync(cancellationToken);

        foreach (var message in messages.Where(message => message.RecipientId.HasValue))
        {
            await _notificationCenter.SendUserNotification(message.RecipientId.Value, new NotificationDto
            {
                Id = Guid.NewGuid(),
                Type = NotificationType.NewMessage,
                CreatedBy = message.Sender.Username,
                CreatedAt = message.CreatedAt
            });
        }
    }
}
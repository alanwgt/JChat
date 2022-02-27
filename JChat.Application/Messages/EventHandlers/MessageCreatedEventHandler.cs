using AutoMapper;
using AutoMapper.QueryableExtensions;
using JChat.Application.Messages.Queries;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Messages.EventHandlers;

public class MessageCreatedEventHandler : INotificationHandler<DomainEventNotification<MessageCreatedEvent>>
{
    private readonly INotificationCenter _notificationCenter;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MessageCreatedEventHandler(INotificationCenter notificationCenter, IApplicationDbContext context,
        IMapper mapper)
    {
        _notificationCenter = notificationCenter;
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(DomainEventNotification<MessageCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var (channelId, messageId, recipients) = notification.DomainEvent;
        var messages = await _context.MessageProjections
            .Include(mp => mp.Sender)
            .Where(mp => mp.ChannelId == channelId)
            .Where(mp => mp.MessageId == messageId)
            .Where(mp => mp.RecipientId != mp.SenderId)
            .Where(mp => mp.RecipientId.HasValue && recipients.Contains(mp.RecipientId.Value))
            .ProjectTo<MessageProjectionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
            await _notificationCenter.NewMessage(message.RecipientId, message);
    }
}
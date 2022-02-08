using System.Text.Json;
using JChat.Application.Notifications.Queries;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Enums;
using JChat.Domain.Events;
using MediatR;

namespace JChat.Application.Workspaces.EventHandlers;

public class UserJoinedWorkspaceEventHandler : INotificationHandler<DomainEventNotification<UserJoinedWorkspaceEvent>>
{
    private readonly INotificationCenter _notificationCenter;
    private readonly IApplicationDbContext _context;

    public UserJoinedWorkspaceEventHandler(INotificationCenter notificationCenter, IApplicationDbContext context)
    {
        _notificationCenter = notificationCenter;
        _context = context;
    }

    public async Task Handle(DomainEventNotification<UserJoinedWorkspaceEvent> notification,
        CancellationToken cancellationToken)
    {
        var (userId, workspaceId) = notification.DomainEvent;
        var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);

        await _notificationCenter.SendWorkspaceNotification(workspaceId, new NotificationDto
        {
            Id = Guid.NewGuid(),
            Type = NotificationType.UserJoinedWorkspace,
            Meta = JsonSerializer.Serialize(new { userId, username = user!.Username })
        });
    }
}
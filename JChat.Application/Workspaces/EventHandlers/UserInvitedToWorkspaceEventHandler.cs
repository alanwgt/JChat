using AutoMapper;
using JChat.Application.Extensions;
using JChat.Application.Notifications.Queries;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using JChat.Domain.Entities.Notifications;
using JChat.Domain.Enums;
using JChat.Domain.Events;
using MediatR;

namespace JChat.Application.Workspaces.EventHandlers;

public class
    UserInvitedToWorkspaceEventHandler : INotificationHandler<DomainEventNotification<UserInvitedToWorkspaceEvent>>
{
    private readonly INotificationCenter _notificationCenter;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserInvitedToWorkspaceEventHandler(IApplicationDbContext context, IMapper mapper,
        INotificationCenter notificationCenter)
    {
        _context = context;
        _mapper = mapper;
        _notificationCenter = notificationCenter;
    }

    public async Task Handle(DomainEventNotification<UserInvitedToWorkspaceEvent> notification,
        CancellationToken cancellationToken)
    {
        var (workspaceId, invitedById, userId, invitationId) = notification.DomainEvent;
        var workspace = await _context.Workspaces.FindAsync_(workspaceId, cancellationToken);
        var notificationEntity = new Notification(invitedById, userId, NotificationType.WorkspaceInvitation,
            new { workspaceId, workspaceName = workspace.Name, invitationId });

        await _context.Notifications.AddAsync(notificationEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _notificationCenter.SendUserNotification(userId, _mapper.Map<NotificationDto>(notificationEntity));
    }
}
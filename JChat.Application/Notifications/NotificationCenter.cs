using JChat.Application.Notifications.Queries;
using JChat.Application.Shared.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace JChat.Application.Notifications;

public class NotificationCenter : INotificationCenter
{
    private readonly IHubContext<ChatHub, IClientHub> _hub;

    public NotificationCenter(IHubContext<ChatHub, IClientHub> hub)
    {
        _hub = hub;
    }

    private IClientHub? GetUserHub(Guid userId)
        => !OnlineUserManager.HasUser(userId)
            ? null
            : _hub.Clients.Client(OnlineUserManager.GetConnectionString(userId));

    private Task ExecuteIfUserIsOnline(Guid userId, Func<IClientHub, Task> fn)
    {
        var hub = GetUserHub(userId);
        return hub == null ? Task.CompletedTask : fn(hub);
    }

    public Task SendUserNotification(Guid userId, NotificationDto notification)
        => ExecuteIfUserIsOnline(userId, hub => hub.NewNotification(notification));

    public Task SendWorkspaceNotification(Guid workspaceId, NotificationDto notification)
        => _hub.Clients.Group(workspaceId.ToString()).NewNotification(notification);
}
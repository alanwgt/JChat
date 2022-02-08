using JChat.Domain.Entities.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace JChat.Application.Extensions;

public static class ClientProxyExtensions
{
    public static Task SendNotification(this IClientProxy clientProxy, Notification notification)
        => clientProxy.SendAsync("notifications.new",
            new { notification.Id, notification.CreatedAt, notification.Type, notification.Meta });
}
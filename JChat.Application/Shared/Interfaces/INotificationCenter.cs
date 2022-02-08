using JChat.Application.Notifications.Queries;

namespace JChat.Application.Shared.Interfaces;

public interface INotificationCenter
{
    Task SendUserNotification(Guid userId, NotificationDto notification);
    Task SendWorkspaceNotification(Guid workspaceId, NotificationDto notification);
}
using JChat.Application.Notifications.Queries;

namespace JChat.Application.Notifications;

public interface IClientHub
{
    Task NewNotification(NotificationDto notification);
    Task ConnectedToWorkspace(Guid workspaceId);
    Task DisconnectedFromWorkspace(Guid workspaceId);
}
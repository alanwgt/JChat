using JChat.Application.Messages.Queries;
using JChat.Application.Notifications.Queries;

namespace JChat.Application.Notifications;

public interface IClientHub
{
    Task NewNotification(NotificationDto notification);
    Task NewMessage(MessageProjectionDto message);
    Task ConnectedToWorkspace(Guid workspaceId);
    Task DisconnectedFromWorkspace(Guid workspaceId);
    Task SetOnlineUsers(IEnumerable<Guid> users);
    Task UserConnected(Guid userId);
    Task UserDisconnected(Guid userId);
    Task ChannelDeleted(Guid channelId, string channelName);
}
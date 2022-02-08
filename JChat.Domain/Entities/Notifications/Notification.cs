using System.Text.Json;
using JChat.Domain.Enums;
using JChat.Domain.Interfaces;

namespace JChat.Domain.Entities.Notifications;

public class Notification : IEntity<Guid>
{
    public Guid Id { get; protected set; }
    public Guid CreatedById { get; protected set; }
    public Guid UserId { get; protected set; }
    public Guid? WorkspaceId { get; protected set; }

    public string Meta { get; protected set; }
    public NotificationType Type { get; protected set; }

    public DateTime CreatedAt { get; protected set; }
    public DateTime? ReadAt { get; protected set; }

    public User.User User { get; protected set; }
    public User.User CreatedBy { get; protected set; }
    public Workspace.Workspace? Workspace { get; protected set; }

    protected Notification()
    {
    }

    public Notification(Guid createdBydId, Guid userId, NotificationType type, object? meta = null)
    {
        UserId = userId;
        Type = type;
        CreatedAt = DateTime.Now.ToUniversalTime();
        WorkspaceId = null;
        CreatedById = createdBydId;

        if (meta != null)
            Meta = JsonSerializer.Serialize(meta);
    }

    public Notification(Guid createdById, Guid userId, Guid workspaceId, NotificationType type, object? meta = null)
        : this(createdById, userId, type, meta)
    {
        WorkspaceId = workspaceId;
    }

    public void MarkAsRead()
        => ReadAt = DateTime.Now.ToUniversalTime();
}
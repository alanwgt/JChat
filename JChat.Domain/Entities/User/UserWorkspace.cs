using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.User;

public class UserWorkspace : AuditableEntity
{
    public Guid WorkspaceId { get; protected set; }
    public Guid UserId { get; protected set; }
    public bool Admin { get; protected set; }

    [ForeignKey("WorkspaceId")]public Workspace.Workspace Workspace { get; }
    [ForeignKey("UserId")] public User User { get; }

    public UserWorkspace(Guid workspaceId, Guid userId, bool admin = false)
    {
        WorkspaceId = workspaceId;
        UserId = userId;
        Admin = admin;
    }
}
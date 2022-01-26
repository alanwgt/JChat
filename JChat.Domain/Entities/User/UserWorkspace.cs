using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.Exceptions;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.User;

public class UserWorkspace : AuditableEntity
{
    public Guid WorkspaceId { get; protected set; }
    public Guid UserId { get; protected set; }
    public bool Admin { get; protected set; }

    public string? BanishmentReason { get; protected set; }
    public Guid? BanishedById { get; protected set; }
    public DateTime? AcceptedAt { get; protected set; }
    public DateTime? RejectedAt { get; protected set; }

    [ForeignKey("WorkspaceId")] public Workspace.Workspace Workspace { get; }
    [ForeignKey("UserId")] public User User { get; }
    [ForeignKey("BanishedById")] public User? BanishedBy { get; }

    public UserWorkspace(Guid workspaceId, Guid userId, bool admin = false)
    {
        WorkspaceId = workspaceId;
        UserId = userId;
        Admin = admin;
        AcceptedAt = null;
        RejectedAt = null;
    }

    public void AcceptInvitation()
    {
        if (AcceptedAt != null)
            throw new DomainException("user workspace invitation already accepted")
                { Details = "workspaces.invitation.already_accepted" };

        if (RejectedAt != null)
            throw new DomainException("user workspace invitation rejected and cannot be accepted")
                { Details = "workspaces.invitation.rejected" };

        AcceptedAt = DateTime.UtcNow.ToUniversalTime();
    }

    public void RejectInvitation()
    {
        if (AcceptedAt != null)
            throw new DomainException("user workspace invitation already accepted and cannot be rejected")
                { Details = "workspaces.invitation.already_accepted" };

        if (RejectedAt != null)
            throw new DomainException("user workspace invitation rejected cannot be rejected again")
                { Details = "workspaces.invitation.rejected" };

        RejectedAt = DateTime.UtcNow.ToUniversalTime();
    }

    public void Banish(Guid banishedById, string reason)
    {
        if (banishedById == UserId)
            throw new DomainException("user cannot banish himself") { Details = "workspaces.cannot_banish_himself" };

        BanishedById = banishedById;
        BanishmentReason = reason;
    }
}
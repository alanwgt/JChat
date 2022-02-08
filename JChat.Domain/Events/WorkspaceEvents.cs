using JChat.Domain.SeedWork;

namespace JChat.Domain.Events;

public record WorkspaceCreatedEvent(Guid Id, string Name) : DomainEvent;

public record UserInvitedToWorkspaceEvent(Guid WorkspaceId, Guid InvitedById, Guid UserId, Guid InvitationId) : DomainEvent;

public record UserJoinedWorkspaceEvent(Guid UserId, Guid WorkspaceId) : DomainEvent;

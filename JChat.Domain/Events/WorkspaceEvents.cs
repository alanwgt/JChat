using JChat.Domain.SeedWork;

namespace JChat.Domain.Events;

public record WorkspaceCreatedEvent(Guid Id, string Name) : DomainEvent;

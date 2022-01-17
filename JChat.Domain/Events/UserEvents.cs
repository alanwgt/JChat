using JChat.Domain.SeedWork;

namespace JChat.Domain.Events;

public record UserCreatedEvent(Guid Id, string Username, string Firstname, string Lastname) : DomainEvent;
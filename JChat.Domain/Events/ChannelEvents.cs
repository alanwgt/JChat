using JChat.Domain.SeedWork;

namespace JChat.Domain.Events;

public record UserAddedToChannel(Guid ChannelId, Guid AddedById, Guid UserId) : DomainEvent;

public record MessageCreatedEvent(Guid ChannelId, Guid MessageId, IEnumerable<Guid> Recipients) : DomainEvent;

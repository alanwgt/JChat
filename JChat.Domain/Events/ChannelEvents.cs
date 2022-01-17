namespace JChat.Domain.Events;

public record ChannelCreatedEvent(Guid ChannelId, Guid UserId);

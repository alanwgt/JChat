using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageReaction : UserEntity
{
    public Guid MessageId { get; protected set; }
    public Guid ReactionId { get; protected set; }

    public Message Message { get; }
    public Reaction Reaction { get; }
}

using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageReaction : AuditableEntity
{
    public Guid MessageId { get; protected set; }
    public Guid ReactionId { get; protected set; }

    public Message Message { get; }
    public Reaction Reaction { get; }

    protected MessageReaction()
    {
    }

    public MessageReaction(Guid messageId, Guid reactionId)
    {
        MessageId = messageId;
        ReactionId = reactionId;
    }
}

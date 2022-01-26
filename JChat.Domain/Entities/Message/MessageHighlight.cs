using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageHighlight : AuditableEntity
{
    public Guid MessageId { get; protected set; }
    public Message Message { get; }

    protected MessageHighlight()
    {
    }

    public MessageHighlight(Guid messageId)
    {
        MessageId = messageId;
    }
}

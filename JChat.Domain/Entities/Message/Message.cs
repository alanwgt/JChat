using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class Message : AuditableEntity
{
    public Guid BodyTypeId { get; protected set; }
    public Guid PriorityId { get; protected set; }
    public Guid? ReplyingToId { get; protected set; }
    public Guid MessageBodyTypeId { get; protected set; }
    public string Body { get; protected set; }
    public string Meta { get; protected set; }
    public DateTime? ExpirationDate { get; protected set; }

    public Message? ReplyingTo { get; }
    public MessageBodyType BodyType { get; }
    public MessagePriority Priority { get; }

    public IEnumerable<MessageReaction> Reactions { get; } = new List<MessageReaction>();
    public IList<MessageRecipient> Recipients { get; } = new List<MessageRecipient>();

    protected Message()
    {
    }

    public Message(Guid messageTypeId, Guid messagePriorityId, string body, string meta, Guid? replyingToId = null,
        DateTime? expirationDate = null)
    {
        BodyTypeId = messageTypeId;
        PriorityId = messagePriorityId;
        ReplyingToId = replyingToId;
        Body = body;
        Meta = string.IsNullOrEmpty(meta) ? "{}" : meta;
        ExpirationDate = expirationDate;
    }

    public void AddRecipient(MessageRecipient recipient)
        => Recipients.Add(recipient);
}
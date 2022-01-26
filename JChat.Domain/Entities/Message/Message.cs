using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class Message : AuditableEntity
{
    public Guid MessageTypeId { get; protected set; }
    public Guid MessagePriorityId { get; protected set; }
    public Guid? ReplyingToId { get; protected set; }
    public string Body { get; protected set; }
    [Column(TypeName = "jsonb")] public string Meta { get; protected set; }
    public DateTime? ExpirationDate { get; protected set; }

    public Message? ReplyingTo { get; }
    public MessageType MessageType { get; }
    public MessagePriority MessagePriority { get; }

    public IEnumerable<MessageReaction> Reactions { get; } = new List<MessageReaction>();

    protected Message()
    {
    }

    public Message(Guid messageTypeId, Guid messagePriorityId, string body, string meta, Guid? replyingToId = null,
        DateTime? expirationDate = null)
    {
        MessageTypeId = messageTypeId;
        MessagePriorityId = messagePriorityId;
        ReplyingToId = replyingToId;
        Body = body;
        Meta = meta;
        ExpirationDate = expirationDate;
    }
}
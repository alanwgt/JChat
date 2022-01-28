using JChat.Domain.Enums;
using JChat.Domain.Interfaces;

namespace JChat.Domain.Entities.Message;

public class MessageProjection : IEntity<Guid>
{
    public Guid Id { get; protected set; } = new Guid();

    public Guid ChannelId { get; set; }
    public Guid RecipientId { get; set; }
    public Guid MessageId { get; set; }
    public Guid? ReplyingToId { get; set; }
    public Guid? ForwardedById { get; set; }

    public string Body { get; set; }
    public string Meta { get; set; }

    public MessageBody BodyType { get; set; }
    public MessagePriority Priority { get; set; }

    public string Reactions { get; set; }

    public DateTime? ReceivedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? ConfirmedVisualizationAt { get; set; }

    public Channel.Channel Channel { get; protected set; }
    public User.User Recipient { get; protected set; }
    public Message Message { get; protected set; }
    public Message? ReplyingTo { get; protected set; }
    public User.User ForwardedBy { get; protected set; }
}

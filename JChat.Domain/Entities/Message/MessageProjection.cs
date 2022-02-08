using JChat.Domain.Interfaces;

namespace JChat.Domain.Entities.Message;

public class MessageProjection : IEntity<Guid>
{
    public Guid Id { get; protected set; } = new Guid();

    public Guid ChannelId { get; set; }
    public Guid? RecipientId { get; set; }
    public Guid MessageId { get; set; }
    public Guid PriorityId { get; set; }
    public Guid? ReplyingToId { get; set; }
    public Guid? ForwardedById { get; set; }

    public Guid SenderId { get; set; }
    public string SenderName { get; set; }
    public bool IsInbound { get; set; }
    public string Body { get; set; }
    public string Meta { get; set; }

    public Guid BodyTypeId { get; set; }
    public MessagePriority Priority { get; set; }

    public string Reactions { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? ConfirmedVisualizationAt { get; set; }

    public Channel.Channel Channel { get; }
    public User.User Sender { get; }
    public User.User? Recipient { get; }
    public Message Message { get; }
    public Message? ReplyingTo { get; }
    public User.User ForwardedBy { get; }
    public MessageBodyType BodyType { get; protected set; }

    public static MessageProjection From(Message message, Guid channelId, IUser sender)
        => new()
        {
            ChannelId = channelId,
            MessageId = message.Id,
            PriorityId = message.PriorityId,
            Body = message.Body,
            Meta = message.Meta,
            BodyTypeId = message.BodyTypeId,
            SenderId = sender.Id,
            SenderName = sender.Username
        };
}

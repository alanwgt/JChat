using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageRecipient : AuditableEntity
{
    public Guid RecipientId { get; protected set; }
    public Guid MessageId { get; protected set; }
    public Guid ChannelId { get; protected set; }
    public Guid? ForwardedById { get; protected set; }
    public DateTime? ReceivedAt { get; protected set; }
    public DateTime? ReadAt { get; protected set; }
    public DateTime? ConfirmedVisualizationAt { get; protected set; }

    public User.User Recipient { get; }
    public MessageRecipient? ForwardedBy { get; protected set; }
    public Message Message { get; }
    public Channel.Channel Channel { get; }

    protected MessageRecipient()
    {
    }

    public MessageRecipient(Guid recipientId, Guid messageId, Guid channelId, Guid? forwardedById = null)
    {
        RecipientId = recipientId;
        MessageId = messageId;
        ChannelId = channelId;
        ForwardedById = forwardedById;
    }

    public void SetRead()
        => ReadAt = DateTime.Now.ToUniversalTime();

    public void SetReceived()
        => ReceivedAt = DateTime.Now.ToUniversalTime();

    public void ConfirmedVisualization()
        => ConfirmedVisualizationAt = DateTime.Now.ToUniversalTime();
}

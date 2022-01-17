using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageRecipient : UserEntity
{
    public Guid MessageId { get; protected set; }
    public Guid ChannelId { get; protected set; }
    public Guid? ForwardedById { get; protected set; }
    public DateTime? ReceivedAt { get; protected set; }
    public DateTime? ReadAt { get; protected set; }
    public DateTime? ConfirmedVisualizationAt { get; protected set; }

    public MessageRecipient? ForwardedBy { get; protected set; }
    public Entities.Message.Message Message { get; }
    public Channel.Channel Channel { get; }
}

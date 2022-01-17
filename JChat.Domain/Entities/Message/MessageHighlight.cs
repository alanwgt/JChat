using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageHighlight : UserEntity
{
    public Guid MessageId { get; protected set; }
    public Entities.Message.Message Message { get; }
}

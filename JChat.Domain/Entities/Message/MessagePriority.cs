using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessagePriority : Entity
{
    public string Name { get; protected set; }

    protected MessagePriority()
    {
    }

    public MessagePriority(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

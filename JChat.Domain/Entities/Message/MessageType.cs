using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageType : Entity
{
    public string Name { get; protected set; }

    protected MessageType()
    {
    }

    public MessageType(string name)
    {
        Name = name;
    }
}

using JChat.Domain.Enums;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageBodyType : Entity
{
    public string Name { get; protected set; }
    public MessageBody BodyType { get; protected set; }

    protected MessageBodyType()
    {
    }

    public MessageBodyType(string name, MessageBody bodyType)
    {
        Name = name;
        BodyType = bodyType;
    }
}

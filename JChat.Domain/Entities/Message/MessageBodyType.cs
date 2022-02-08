using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessageBodyType : Entity
{
    public string Name { get; protected set; }

    protected MessageBodyType()
    {
    }

    public MessageBodyType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

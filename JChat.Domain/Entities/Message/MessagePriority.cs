using JChat.Domain.Enums;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessagePriority : Entity
{
    public string Name { get; protected set; }
    public MessagePriorityType Priority { get; protected set; }

    protected MessagePriority()
    {
    }

    public MessagePriority(string name, MessagePriorityType priority)
    {
        Name = name;
        Priority = priority;
    }
}

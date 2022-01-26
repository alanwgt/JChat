using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.Enums;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class MessagePriority : Entity
{
    public string Name { get; protected set; }
    [Column(TypeName = "char")]
    public MessagePriorityType Priority { get; protected set; }

    protected MessagePriority()
    {
    }

    public MessagePriority(string name)
    {
        Name = name;
    }
}

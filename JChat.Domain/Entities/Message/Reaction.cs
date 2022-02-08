using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class Reaction : Entity
{
    public string Name { get; protected set; }
    public string Icon { get; protected set; }
    [Column(TypeName = "char(9)")]
    public string Color { get; protected set; }

    protected Reaction()
    {
    }

    public Reaction(Guid id, string name, string icon, string color)
    {
        Id = id;
        Name = name;
        Icon = icon;
        Color = color;
    }
}

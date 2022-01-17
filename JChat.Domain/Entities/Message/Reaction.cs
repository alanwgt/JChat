using System.ComponentModel.DataAnnotations.Schema;
using JChat.Domain.SeedWork;

namespace JChat.Domain.Entities.Message;

public class Reaction : Entity
{
    public string Name { get; protected set; }
    public string Icon { get; protected set; }
    [Column(TypeName = "char(9)")]
    public string Color { get; protected set; }

    public Reaction(string name, string icon, string color)
    {
        Name = name;
        Icon = icon;
        Color = color;
    }
}

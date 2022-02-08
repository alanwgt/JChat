using JChat.Domain.Interfaces;

namespace JChat.Domain.SeedWork;

public abstract class Entity : IEntity<Guid>, IEquatable<Entity>
{
    public Guid Id { get; protected set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    protected Entity()
    {
    }

    public override bool Equals(object obj)
        => Equals(obj as Entity);

    public bool Equals(Entity? other)
        => other != null && EqualityComparer<Guid>.Default.Equals(Id, other.Id);

    public override int GetHashCode()
        => HashCode.Combine(Id);

    public static bool operator ==(Entity left, Entity right)
        => EqualityComparer<Entity>.Default.Equals(left, right);

    public static bool operator !=(Entity left, Entity right)
        => !(left == right);
}

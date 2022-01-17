using JChat.Domain.SeedWork;

namespace JChat.Domain.Interfaces;

public interface IHasDomainEvents
{
    public ICollection<DomainEvent> DomainEvents { get; }
}

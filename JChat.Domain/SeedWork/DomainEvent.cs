using System.Text.Json.Serialization;
using JChat.Domain.Interfaces;

namespace JChat.Domain.SeedWork;

public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; }
    public string EventName { get; }
    public DateTime CreatedAt { get; }

    public Guid AggregateId { get; }

    public Guid CorrelationId { get; private set; }
    public Guid CausationId { get; private set; }

    [JsonIgnore] public bool IsPublished { get; set; }

    protected DomainEvent()
    {
    }

    public DomainEvent(Guid aggregateId)
    {
        EventId = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        EventName = GetType().Name;
        AggregateId = aggregateId;
        CorrelationId = EventId;
        CausationId = EventId;
    }

    public DomainEvent WithCausation(DomainEvent de)
    {
        CorrelationId = de.EventId;
        CausationId = de.CorrelationId;
        return this;
    }
}

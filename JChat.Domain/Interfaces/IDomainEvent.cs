namespace JChat.Domain.Interfaces;

public interface IDomainEvent
{
    Guid EventId { get; }
    Guid AggregateId { get; }
    string EventName { get; }
    DateTime CreatedAt { get; }
    bool IsPublished { get; set; }

    // https://blog.arkency.com/correlation-id-and-causation-id-in-evented-systems/
    Guid CorrelationId { get; } // primeiro
    Guid CausationId { get; } // anterior
}

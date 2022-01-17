using JChat.Domain.Interfaces;

public interface IDomainEventService
{
    Task Publish(IDomainEvent domainEvent);
}

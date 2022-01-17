using JChat.Application.Shared.Models;
using JChat.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JChat.Infrastructure.Persistence.Services;

public class DomainEventService : IDomainEventService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    private static INotification GetNotificationCorrespondingToDomainEvent(IDomainEvent domainEvent)
        => (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;

    public async Task Publish(IDomainEvent domainEvent)
    {
        _logger.LogInformation("[DOMAIN_EVENT]: publishing event {event}", domainEvent.GetType().Name);
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }
}

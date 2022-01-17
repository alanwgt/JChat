using JChat.Application.Shared.Models;
using JChat.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JChat.Application;

public class TestEventHandler : INotificationHandler<DomainEventNotification<TestEvent>>
{
    private readonly ILogger<TestEventHandler> _logger;

    public TestEventHandler(ILogger<TestEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<TestEvent> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing event {Event} in {Handler}", nameof(TestEvent), nameof(TestEventHandler));
        return Task.CompletedTask;
    }
}
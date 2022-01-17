using JChat.Application.Shared.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace JChat.Application.Shared.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ICurrentUserService currentUserService,
        ILogger<TRequest> logger)
    {
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var user = _currentUserService.User;
        var userId = user?.Id;
        var username = user?.Username;

        _logger.LogInformation("[REQUEST]: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, username, request);

        return Task.CompletedTask;
    }
}
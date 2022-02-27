using System.Diagnostics;
using JChat.Application.Shared.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JChat.Application.Shared.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMs = _timer.ElapsedMilliseconds;

        if (elapsedMs <= 500)
        {
            return response;
        }

        var requestName = typeof(TRequest).Name;
        var user = _currentUserService.User;
        var userId = user?.Id;
        var username = user?.Username;

        _logger.LogWarning("[PERFORMANCE]: Long running request {Name} ({ElapsedMd} ms) {@UserId} {@Username}",
            requestName, elapsedMs, userId, username);

        return response;
    }
}
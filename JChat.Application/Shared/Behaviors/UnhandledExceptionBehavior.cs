using MediatR;
using Microsoft.Extensions.Logging;

namespace JChat.Application.Shared.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(e, "[REQUEST:UNHANDLED_EXCEPTION]: Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}

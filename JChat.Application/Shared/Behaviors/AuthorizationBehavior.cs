using System.Reflection;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JChat.Application.Shared.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;

    public AuthorizationBehavior(ICurrentUserService currentUserService, IIdentityService identityService,
        ILogger<AuthorizationBehavior<TRequest, TResponse>> logger)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (request is IHasUserSetter br)
        {
            if (_currentUserService.User == null)
            {
                _logger.LogError("A command with required credentials was fired by an unauthenticated user");
                throw new UnauthorizedAccessException();
            }

            br.User = _currentUserService.User;
        }

        var authorizationAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (!authorizationAttributes.Any())
            return await next();

        // TODO: handle authorization!!
        return await next();
        var authorized = false;

        if (!authorized)
            throw new ForbiddenAccessException();

        return await next();
    }
}
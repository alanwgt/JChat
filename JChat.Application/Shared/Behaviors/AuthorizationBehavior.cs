using System.Reflection;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using JChat.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;

namespace JChat.Application.Shared.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentWorkspaceService _currentWorkspaceService;
    private readonly IAuthorizationService _authorization;

    public AuthorizationBehavior(ICurrentUserService currentUserService,
        ILogger<AuthorizationBehavior<TRequest, TResponse>> logger, ICurrentWorkspaceService currentWorkspaceService,
        IAuthorizationService authorization)
    {
        _currentUserService = currentUserService;
        _logger = logger;
        _currentWorkspaceService = currentWorkspaceService;
        _authorization = authorization;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        IUser? user = null;

        if (request is IHasUserSetter br)
        {
            if (_currentUserService.User == null)
            {
                _logger.LogError("A command with required credentials was fired by an unauthenticated user");
                throw new UnauthorizedAccessException();
            }

            br.User = _currentUserService.User;
            user = br.User;
        }

        if (request is IHasWorkspaceIdSetter idSetter)
        {
            if (!_currentWorkspaceService.WorkspaceId.HasValue)
            {
                _logger.LogError("A command with required workspace id was fired without a workspace id");
                throw new UnauthorizedAccessException();
            }

            idSetter.WorkspaceId = _currentWorkspaceService.WorkspaceId.Value;
        }

        var authorizationAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (!authorizationAttributes.Any())
            return await next();

        if (user == null)
        {
            _logger.LogError("A command with required authz was fired by an unauthenticated user");
            throw new UnauthorizedAccessException();
        }

        foreach (var authorizeAttribute in authorizationAttributes)
        {
            var objectId = request.GetType().GetProperty(authorizeAttribute.Object).GetValue(request);

            if (objectId == null)
            {
                _logger.LogError("couldn't find the object id in authorization behavior");
                throw new ApplicationException("object id not found in request");
            }

            var authzRequest = _authorization.Can(
                authorizeAttribute.Namespace,
                objectId.ToString(),
                authorizeAttribute.Relation,
                authorizeAttribute.Subject ?? user.Id.ToString(),
                cancellationToken
            );

            if (!await authzRequest)
            {
                throw new ForbiddenAccessException($"unauthorized access in command {request.GetType().Name}");
            }
        }

        return await next();
    }
}
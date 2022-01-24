using AutoMapper;
using FluentValidation;
using JChat.Application.Enums;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Workspaces.Queries;
using JChat.Domain.Entities.Workspace;
using MediatR;

namespace JChat.Application.Workspaces.Commands;

public record CreateWorkspaceCommand(string Name) : IRequest<WorkspaceBriefDto>;

public class CreateWorkspaceCommandHandlerValidator : AbstractValidator<CreateWorkspaceCommand>
{
    public CreateWorkspaceCommandHandlerValidator()
    {
        RuleFor(w => w.Name)
            .NotNull()
            .MaximumLength(50)
            .MinimumLength(3);
    }
}

internal class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, WorkspaceBriefDto>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationDbContext _applicationContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateWorkspaceCommandHandler(IApplicationDbContext applicationContext,
        ICurrentUserService currentUserService, IMapper mapper, IAuthorizationService authorizationService)
    {
        _applicationContext = applicationContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _authorizationService = authorizationService;
    }

    public async Task<WorkspaceBriefDto> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = new Workspace(request.Name);
        await using var transaction = await _applicationContext.Database.BeginTransactionAsync(cancellationToken);
        var userId = _currentUserService.User.Id;

        try
        {
            await _applicationContext.Workspaces.AddAsync(workspace, cancellationToken);
            workspace.AddMember(userId, true);

            await _applicationContext.SaveChangesAsync(cancellationToken);

            var ns = AuthzNamespace.Workspaces.Str();
            var workspaceId = workspace.Id.ToString();

            await _authorizationService.Authorize(
                ns,
                workspaceId,
                AuthzRelation.Ownership.Str(),
                userId.ToString(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                workspaceId,
                AuthzRelation.Manage.Str(),
                null,
                ns,
                workspaceId,
                AuthzRelation.Ownership.Str(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                workspaceId,
                AuthzRelation.Member.Str(),
                null,
                ns,
                workspaceId,
                AuthzRelation.Manage.Str(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                workspaceId,
                AuthzRelation.Write.Str(),
                null,
                ns,
                workspaceId,
                AuthzRelation.Member.Str(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                workspaceId,
                AuthzRelation.Read.Str(),
                null,
                ns,
                workspaceId,
                AuthzRelation.Member.Str(),
                cancellationToken
            );

            await transaction.CommitAsync(cancellationToken);

            return _mapper.Map<WorkspaceBriefDto>(workspace);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
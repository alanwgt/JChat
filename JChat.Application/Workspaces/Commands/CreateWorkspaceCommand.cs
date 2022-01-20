using AutoMapper;
using FluentValidation;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Workspaces.Queries;
using JChat.Domain.Entities.Workspace;
using MediatR;

namespace JChat.Application.Workspaces.Commands;

public record CreateWorkspaceCommand(string Name) : IRequest<WorkspaceBriefDto>;

internal class CreateWorkspaceCommandHandlerValidator : AbstractValidator<CreateWorkspaceCommand>
{
    private CreateWorkspaceCommandHandlerValidator()
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

        await _applicationContext.Workspaces.AddAsync(workspace, cancellationToken);
        workspace.AddMember(_currentUserService.User.Id, true);

        await _applicationContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WorkspaceBriefDto>(workspace);
    }
}
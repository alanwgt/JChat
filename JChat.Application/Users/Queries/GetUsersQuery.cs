using AutoMapper;
using FluentValidation;
using JChat.Application.Extensions;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Mappings;
using JChat.Application.Shared.Models;
using JChat.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Users.Queries;

public class GetUsersQuery : PaginatedQuery<UserBriefDto>, IHasUserSetter
{
    public string Q { get; set; }
    public bool WorkspaceScoped { get; set; }
    public IUser User { get; set; }
}

public class GetUserQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(e => e.Q)
            .NotEmpty()
            .MinimumLength(3);
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IAuthorizationService _authorizationService;

    public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentWorkspaceService currentWorkspace,
        IAuthorizationService authorizationService)
    {
        _context = context;
        _mapper = mapper;
        _currentWorkspace = currentWorkspace;
        _authorizationService = authorizationService;
    }

    public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        if (request.WorkspaceScoped)
        {
            if (!_currentWorkspace.WorkspaceId.HasValue)
                throw new BadRequestException { Details = "workspace id was not provided" };

            if (!await _authorizationService.CanReadWorkspace(request.User, _currentWorkspace.WorkspaceId.Value,
                    cancellationToken))
                throw new ForbiddenAccessException("unauthorized access to workspace");

            query = query.Join(
                _context.UserWorkspaces.Where(uw => uw.WorkspaceId == _currentWorkspace.WorkspaceId),
                u => u.Id,
                uw => uw.UserId,
                (user, workspace) => user
            );
        }

        return await
            query.Where(u => EF.Functions.ILike(u.Username, $"{request.Q}%"))
                .Where(u => u.Id != request.User.Id)
                .OrderBy(u => u.Username)
                .AsNoTracking()
                .PaginatedListAsync(request, _mapper.ConfigurationProvider);
    }
}
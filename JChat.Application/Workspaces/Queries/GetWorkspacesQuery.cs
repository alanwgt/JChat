using AutoMapper;
using AutoMapper.QueryableExtensions;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Workspaces.Queries;

public class GetWorkspacesQuery : PaginatedQuery<WorkspaceBriefDto>
{
}

public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, PaginatedList<WorkspaceBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public GetWorkspacesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper)
    {
        _context = context;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<PaginatedList<WorkspaceBriefDto>> Handle(GetWorkspacesQuery request,
        CancellationToken cancellationToken)
    {
        var query = from w in _context.Workspaces
            join uw in _context.UserWorkspaces on w.Id equals uw.WorkspaceId
            where uw.UserId == _currentUser.User.Id
            select w;

        return await PaginatedList<WorkspaceBriefDto>.CreateAsync(
            query.AsNoTracking().ProjectTo<WorkspaceBriefDto>(_mapper.ConfigurationProvider),
            request
        );
    }
}
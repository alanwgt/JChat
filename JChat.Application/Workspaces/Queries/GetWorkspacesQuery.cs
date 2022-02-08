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

    public GetWorkspacesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<PaginatedList<WorkspaceBriefDto>> Handle(GetWorkspacesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Workspaces
            .Join(
                _context.UserWorkspaces
                    .Where(uw => uw.UserId == _currentUser.User.Id)
                    .Where(uw => uw.Admin || uw.AcceptedAt != null),
                w => w.Id,
                uw => uw.WorkspaceId,
                (w, uw) => new
                {
                    Dto = new WorkspaceBriefDto
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Joined = uw.AcceptedAt != null,
                    },
                    uw.UserId
                }
            ).Select(g => g.Dto);

        return await PaginatedList<WorkspaceBriefDto>.CreateAsync(
            query.AsNoTracking(),
            request
        );
    }
}
using AutoMapper;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Mappings;
using JChat.Application.Shared.Models;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Users.Queries;

[Authorize(Namespace = "workspaces", ObjectIdFromProperty = "WorkspaceId", Relation = "read")]
public class GetUsersQuery : WorkspaceScopedPaginatedRequest<UserBriefDto>
{
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.UserWorkspaces
            .Where(uw => uw.WorkspaceId == request.WorkspaceId)
            .Include(uw => uw.User)
            .Select(uw => uw.User);

        return await query.PaginatedListAsync(request, _mapper.ConfigurationProvider);
    }
}
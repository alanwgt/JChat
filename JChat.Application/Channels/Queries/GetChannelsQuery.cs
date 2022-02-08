using AutoMapper;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Mappings;
using JChat.Application.Shared.Models;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Channels.Queries;

[Authorize(Namespace = "workspaces", ObjectIdFromProperty = "WorkspaceId", Relation = "read")]
public class GetChannelsQuery : WorkspaceScopedPaginatedRequest<ChannelBriefDto>
{
}

public class GetChannelsQueryHandler : IRequestHandler<GetChannelsQuery, PaginatedList<ChannelBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<ChannelBriefDto>> Handle(GetChannelsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Channels
            .Where(c => c.WorkspaceId == request.WorkspaceId)
            .Where(c => c.IsPrivate == false)
            .Join(
                _context.ChannelUsers.Where(cu => cu.UserId == request.User.Id),
                c => c.Id,
                cu => cu.ChannelId,
                (c, cu) => c
            ).OrderBy(c => c.Name)
            .AsNoTracking();

        return query.PaginatedListAsync(request, _mapper.ConfigurationProvider);
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JChat.Application.Channels.Queries;
using JChat.Application.Enums;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Boot.Queries;

[Authorize(Namespace = "workspaces", ObjectIdFromProperty = "WorkspaceId", Relation = "read")]
public class GetBootQuery : WorkspaceScopedRequest<BootDto>
{
}

public class GetBootQueryHandler : IRequestHandler<GetBootQuery, BootDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorization;

    public GetBootQueryHandler(IApplicationDbContext context, IMapper mapper, IAuthorizationService authorization)
    {
        _context = context;
        _mapper = mapper;
        _authorization = authorization;
    }

    public async Task<BootDto> Handle(GetBootQuery request, CancellationToken cancellationToken)
    {
        var priorities = await _context.MessagePriorities
            .ProjectTo<IdNameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var reactions = await _context.Reactions
            .ProjectTo<IdNameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var types = await _context.MessageBodyTypes
            .ProjectTo<IdNameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var workspaceUsers = await _context.Users
            .Join(
                _context.UserWorkspaces
                    .Where(uw => uw.WorkspaceId == request.WorkspaceId)
                    .Where(uw => uw.UserId != request.User.Id),
                u => u.Id,
                uw => uw.UserId,
                (u, uw) => u
            ).ProjectTo<UserBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var me = await _context.Users
            .Where(u => u.Id == request.User.Id)
            .ProjectTo<UserBriefDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
        var channels = await _context.Channels
            .Where(c => c.WorkspaceId == request.WorkspaceId)
            .Join(
                _context.ChannelUsers
                    .Where(cu => cu.UserId == request.User.Id),
                c => c.Id,
                cu => cu.ChannelId,
                (c, cu) => c
            ).ProjectTo<ChannelBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var channelsWithAccess = await _authorization.ListObjectsWithAccess(AuthzNamespace.Channels.Str(), null, null,
            null, null, request.User.Id.ToString(), null, null, null, cancellationToken);

        var workspacesWithAccess = await _authorization.ListObjectsWithAccess(AuthzNamespace.Workspaces.Str(), null,
            null,
            null, null, request.User.Id.ToString(), null, null, null, cancellationToken);

        var permissions = channelsWithAccess.RelationTuples;
        permissions.AddRange(workspacesWithAccess.RelationTuples);

        return new BootDto
        {
            MessagePriorities = priorities,
            MessageReactions = reactions,
            MessageTypes = types,
            Channels = channels,
            Users = workspaceUsers,
            Me = me,
            Permissions = permissions.Select(rt => new { rt.Namespace, rt.Object, rt.Relation }),
        };
    }
}
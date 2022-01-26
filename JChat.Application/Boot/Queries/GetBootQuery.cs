using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        var types = await _context.MessageTypes
            .ProjectTo<IdNameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var channelsWithAccess = await _authorization.ListObjectsWithAccess(AuthzNamespace.Channels.Str(), null, null,
            null, null, null, null, null, null, cancellationToken);

        return new BootDto
        {
            MessagePriorities = priorities,
            MessageReactions = reactions,
            MessageTypes = types,
            Permissions = channelsWithAccess.RelationTuples
        };
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JChat.Application.Messages.Queries;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Channels.Queries;

[Authorize(Namespace = "channels", ObjectIdFromProperty = "ChannelId", Relation = "read")]
public class GetChannelQuery : ChannelScopedRequest<ChannelDetailedDto>
{
}

public class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, ChannelDetailedDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ChannelDetailedDto> Handle(GetChannelQuery request, CancellationToken cancellationToken)
    {
        var channel =
            await _context.Channels
                .Where(c => c.Id == request.ChannelId)
                .ProjectTo<ChannelBriefDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken) ??
            throw new NotFoundException("channel", request.ChannelId);

        var members = await _context.ChannelUsers
            .Where(cu => cu.UserId != request.User.Id)
            .Where(cu => cu.ChannelId == channel.Id)
            .Join(
                _context.Users,
                cu => cu.UserId,
                u => u.Id,
                (cu, u) => u
            )
            .ProjectTo<UserBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var messages = await _context.MessageProjections
            .OrderByDescending(m => m.CreatedAt)
            .Where(mp => mp.ChannelId == request.ChannelId)
            .Where(mp =>
                mp.RecipientId == null || mp.RecipientId == request.User.Id ||
                mp.IsInbound == false && mp.SenderId == request.User.Id)
            .ProjectTo<MessageProjectionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        messages.Reverse();

        return new ChannelDetailedDto
        {
            Channel = channel,
            Members = members,
            Messages = messages,
            // MessageRecipients = messages.Select(m => m.MessageRecipient)
        };
    }
}
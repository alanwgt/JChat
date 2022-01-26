using AutoMapper;
using AutoMapper.QueryableExtensions;
using JChat.Application.Shared.Dtos;
using JChat.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Boot.Queries;

public record GetBootQuery : IRequest<BootDto>;

public class GetBootQueryHandler : IRequestHandler<GetBootQuery, BootDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBootQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

        return new BootDto
        {
            MessagePriorities = priorities,
            MessageReactions = reactions,
            MessageTypes = types
        };
    }
}
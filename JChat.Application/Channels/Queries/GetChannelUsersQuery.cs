using AutoMapper;
using FluentValidation;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Mappings;
using JChat.Application.Shared.Models;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Channels.Queries;

[Authorize(Namespace = "channels", Object = "ChannelId", Relation = "read")]
public class GetChannelUsersQuery : WorkspaceScopedPaginatedRequest<ChannelUserBriefDto>
{
    public Guid ChannelId { get; set; }
}

public class GetChannelUsersQueryValidator : AbstractValidator<GetChannelUsersQuery>
{
    public GetChannelUsersQueryValidator()
    {
        RuleFor(q => q.ChannelId)
            .NotEmpty();
    }
}

public class GetChannelUsersQueryHandler : IRequestHandler<GetChannelUsersQuery, PaginatedList<ChannelUserBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<ChannelUserBriefDto>> Handle(GetChannelUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ChannelUsers
            .Where(cu => cu.ChannelId == request.ChannelId)
            .AsNoTracking();

        return query.PaginatedListAsync(request, _mapper.ConfigurationProvider);
    }
}
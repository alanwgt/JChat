using AutoMapper;
using FluentValidation;
using JChat.Application.Channels.Queries;
using JChat.Application.Enums;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using MediatR;

namespace JChat.Application.Channels.Commands;

[Authorize(Namespace = "channels", ObjectIdFromProperty = "ChannelId", Relation = "manage")]
public class AddUserToChannelCommand : WorkspaceScopedRequest<ChannelUserBriefDto>
{
    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }
}

public class AddUserToChannelCommandValidator : AbstractValidator<AddUserToChannelCommand>
{
    public AddUserToChannelCommandValidator()
    {
        RuleFor(a => a.ChannelId)
            .NotEmpty();

        RuleFor(a => a.UserId)
            .NotEmpty();
    }
}

public class AddUserToChannelCommandHandler : IRequestHandler<AddUserToChannelCommand, ChannelUserBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthorizationService _authorization;
    private readonly IMapper _mapper;

    public AddUserToChannelCommandHandler(IApplicationDbContext context, IMapper mapper,
        IAuthorizationService authorization)
    {
        _context = context;
        _mapper = mapper;
        _authorization = authorization;
    }

    public async Task<ChannelUserBriefDto> Handle(AddUserToChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = await _context.Channels.FindAsync(new object?[] { request.ChannelId }, cancellationToken) ??
                      throw new NotFoundException("channel", request.ChannelId);

        var channelUser = channel.AddUser(request.UserId);

        await _authorization.Authorize(
            AuthzNamespace.Channels.Str(),
            channel.Id.ToString(),
            AuthzRelation.Member.Str(),
            request.UserId.ToString(),
            cancellationToken
        );

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ChannelUserBriefDto>(channelUser);
    }
}
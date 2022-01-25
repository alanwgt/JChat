using AutoMapper;
using FluentValidation;
using JChat.Application.Channels.Queries;
using JChat.Application.Enums;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Channels.Commands;

[Authorize(Relation = "ownership", Namespace = "channels", ObjectIdFromProperty = "ChannelId")]
public class ChangeUserChannelAdmCommand : WorkspaceScopedRequest<ChannelUserBriefDto>
{
    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }
    public bool Admin { get; set; }
}

public class ChangeUserChannelAdmCommandValidator : AbstractValidator<ChangeUserChannelAdmCommand>
{
    public ChangeUserChannelAdmCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.ChannelId)
            .NotEmpty();
    }
}

public class ChangeUserChannelAdmCommandHandler : IRequestHandler<ChangeUserChannelAdmCommand, ChannelUserBriefDto>
{
    private readonly IAuthorizationService _authorization;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ChangeUserChannelAdmCommandHandler(IAuthorizationService authorization, IApplicationDbContext context,
        IMapper mapper)
    {
        _authorization = authorization;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ChannelUserBriefDto> Handle(ChangeUserChannelAdmCommand request,
        CancellationToken cancellationToken)
    {
        var channelUser = await _context.ChannelUsers.SingleOrDefaultAsync(
            cu => cu.ChannelId == request.ChannelId && cu.UserId == request.UserId,
            cancellationToken
        );

        if (channelUser == null)
            throw new NotFoundException("channel_user", $"{request.ChannelId}_${request.UserId}");

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            channelUser.SetIsAdmin(request.Admin);

            if (request.Admin)
            {
                await _authorization.Authorize(
                    AuthzNamespace.Channels.Str(),
                    request.ChannelId.ToString(),
                    AuthzRelation.Manage.Str(),
                    request.UserId.ToString(),
                    cancellationToken
                );
            }
            else
            {
                await _authorization.RemoveAuthorization(
                    AuthzNamespace.Channels.Str(),
                    request.ChannelId.ToString(),
                    AuthzRelation.Manage.Str(),
                    request.UserId.ToString(),
                    cancellationToken
                );
            }

            await transaction.CommitAsync(cancellationToken);
            return _mapper.Map<ChannelUserBriefDto>(channelUser);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
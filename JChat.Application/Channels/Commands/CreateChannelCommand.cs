using AutoMapper;
using FluentValidation;
using JChat.Application.Channels.Queries;
using JChat.Application.Enums;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Entities.Channel;
using MediatR;

namespace JChat.Application.Channels.Commands;

public class CreateChannelCommand : WorkspaceScopedRequest<ChannelBriefDto>
{
    public string Name { get; set; }
    public bool IsPrivate { get; set; }
}

public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
{
    private CreateChannelCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .MaximumLength(50)
            .MinimumLength(3);

        RuleFor(c => c.IsPrivate)
            .NotNull();
    }
}

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, ChannelBriefDto>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationDbContext _applicationContext;
    private readonly IMapper _mapper;

    public CreateChannelCommandHandler(IAuthorizationService authorizationService,
        IApplicationDbContext applicationContext, IMapper mapper)
    {
        _authorizationService = authorizationService;
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<ChannelBriefDto> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = new Channel(request.WorkspaceId, request.Name, request.IsPrivate);
        await using var transaction = await _applicationContext.Database.BeginTransactionAsync(cancellationToken);
        var userId = request.User.Id;
        var cUser = channel.AddUser(userId, true);
        var ns = AuthzNamespace.Channels.Str();
        var channelId = channel.Id.ToString();

        try
        {
            await _applicationContext.Channels.AddAsync(channel, cancellationToken);
            await _applicationContext.ChannelUsers.AddAsync(cUser, cancellationToken);

            await _authorizationService.Authorize(
                ns,
                channelId,
                AuthzRelation.Ownership.Str(),
                userId.ToString(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                channelId,
                AuthzRelation.Manage.Str(),
                null,
                ns,
                channelId,
                AuthzRelation.Ownership.Str(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                channelId,
                AuthzRelation.Member.Str(),
                null,
                ns,
                channelId,
                AuthzRelation.Manage.Str(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                channelId,
                AuthzRelation.Write.Str(),
                null,
                ns,
                channelId,
                AuthzRelation.Member.Str(),
                cancellationToken
            );

            await _authorizationService.Authorize(
                ns,
                channelId,
                AuthzRelation.Read.Str(),
                null,
                ns,
                channelId,
                AuthzRelation.Member.Str(),
                cancellationToken
            );

            await transaction.CommitAsync(cancellationToken);

            return _mapper.Map<ChannelBriefDto>(channel);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
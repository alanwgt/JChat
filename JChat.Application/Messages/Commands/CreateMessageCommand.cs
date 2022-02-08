using AutoMapper;
using FluentValidation;
using JChat.Application.Channels.Queries;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using JChat.Domain.Entities.Message;
using JChat.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;

namespace JChat.Application.Messages.Commands;

[Authorize(Namespace = "channels", ObjectIdFromProperty = "ChannelId", Relation = "write")]
public class CreateMessageCommand : ChannelScopedRequest<MessageBriefDto>
{
    public string Body { get; set; }
    public Guid BodyType { get; set; }
    public Guid Priority { get; set; }
    public DateTime? ExpirationDate { get; set; } = null;
}

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(c => c.Body)
            .NotEmpty();
        RuleFor(c => c.BodyType)
            .NotEmpty();
        RuleFor(c => c.Priority)
            .NotEmpty();
    }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDomainEventService _eventService;

    public CreateMessageCommandHandler(IApplicationDbContext context, IMapper mapper, IDomainEventService eventService)
    {
        _context = context;
        _mapper = mapper;
        _eventService = eventService;
    }

    public async Task<MessageBriefDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var recipients = await _context.ChannelUsers
                .Where(cu => cu.ChannelId == request.ChannelId)
                .ToListAsync(cancellationToken);

            var messageBodyType = await _context.MessageBodyTypes
                                      .FindAsync(new object?[] { request.BodyType }, cancellationToken)
                                  ?? throw new NotFoundException("message body type", request.BodyType);

            if (!recipients.Any())
            {
                throw new ApplicationException { Details = "there is no user in the given channel" };
            }

            var message = new Message(
                messageBodyType.Id,
                request.Priority,
                request.Body,
                "",
                null,
                request.ExpirationDate
            );
            _context.Messages.Add(message);

            foreach (var recipient in recipients)
            {
                message.AddRecipient(new MessageRecipient(recipient.UserId, message.Id, request.ChannelId));
                await _context.MessageProjections.AddAsync(new MessageProjection
                {
                    ChannelId = request.ChannelId,
                    MessageId = message.Id,
                    Body = message.Body,
                    Meta = message.Meta,
                    PriorityId = message.PriorityId,
                    Reactions = "{}",
                    BodyTypeId = messageBodyType.Id,
                    RecipientId = recipient.UserId,
                    IsInbound = recipient.UserId != request.User.Id,
                    SenderId = request.User.Id,
                    SenderName = request.User.Username,
                }, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await _eventService.Publish(new MessageCreatedEvent(request.ChannelId, message.Id,
                recipients.Select(cu => cu.UserId)));

            // TODO: queue expiration date
            await transaction.CommitAsync(cancellationToken);
            return _mapper.Map<MessageBriefDto>(message);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
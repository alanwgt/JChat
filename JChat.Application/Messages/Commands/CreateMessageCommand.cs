using AutoMapper;
using FluentValidation;
using JChat.Application.Messages.Queries;
using JChat.Application.Shared.Constants;
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
public class CreateMessageCommand : ChannelScopedRequest<MessageProjectionDto>
{
    public string Body { get; set; }
    public Guid BodyType { get; set; }
    public Guid Priority { get; set; }
    public string Meta { get; set; } = "";
    public DateTime? ExpirationDate { get; set; } = null;
}

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(c => c.Meta)
            .NotEmpty()
            .When(c => c.BodyType == MessageBodyTypeId.Gif);
        RuleFor(c => c.Body)
            .NotEmpty()
            .When(c => c.BodyType == MessageBodyTypeId.Text);
        RuleFor(c => c.BodyType)
            .NotEmpty();
        RuleFor(c => c.Priority)
            .NotEmpty();
    }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageProjectionDto>
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

    public async Task<MessageProjectionDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
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
                request.Meta,
                null,
                request.ExpirationDate
            );
            _context.Messages.Add(message);
            MessageProjectionDto senderMessageProjection = null;

            foreach (var recipient in recipients)
            {
                message.AddRecipient(new MessageRecipient(recipient.UserId, message.Id, request.ChannelId));
                var mp = MessageProjection.From(message, request.ChannelId, request.User);
                mp.RecipientId = recipient.UserId;
                mp.IsInbound = recipient.UserId != request.User.Id;

                await _context.MessageProjections.AddAsync(mp, cancellationToken);

                if (!mp.IsInbound)
                {
                    senderMessageProjection = _mapper.Map<MessageProjectionDto>(mp);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            await _eventService.Publish(new MessageCreatedEvent(request.ChannelId, message.Id,
                recipients.Select(cu => cu.UserId)));

            // TODO: queue expiration date
            await transaction.CommitAsync(cancellationToken);
            return senderMessageProjection;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
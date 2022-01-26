using AutoMapper;
using FluentValidation;
using JChat.Application.Channels.Queries;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using JChat.Domain.Entities.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;

namespace JChat.Application.Messages.Commands;

[Authorize(Namespace = "channels", ObjectIdFromProperty = "ChannelId", Relation = "write")]
public class CreateMessageCommand : ChannelScopedRequest<MessageBriefDto>
{
    public string Body { get; set; }
    public Guid Type { get; set; }
    public Guid Priority { get; set; }
    public DateTime? ExpirationDate { get; set; }
}

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(c => c.Body)
            .NotEmpty();
        RuleFor(c => c.Type)
            .NotEmpty();
        RuleFor(c => c.Priority)
            .NotEmpty();
    }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateMessageCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MessageBriefDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var recipients = await _context.ChannelUsers
                .Where(cu => cu.ChannelId == request.ChannelId)
                .ToListAsync(cancellationToken);

            if (!recipients.Any())
            {
                throw new ApplicationException{ Details = "there is no user in the given channel" };
            }


            var message = new Message(
                request.Type,
                request.Priority,
                request.Body,
                "",
                null,
                request.ExpirationDate
            );
            _context.Messages.Add(message);

            var messageRecipients = recipients
                .Select(recipient => new MessageRecipient(recipient.UserId, message.Id, request.ChannelId));

            await _context.MessageRecipients.AddRangeAsync(messageRecipients, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: notification
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
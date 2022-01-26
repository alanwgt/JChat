using AutoMapper;
using JChat.Application.Channels.Queries;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Exceptions;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Security;
using JChat.Domain.Entities.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationException = JChat.Application.Shared.Exceptions.ApplicationException;

namespace JChat.Application.Messages.Commands;

[Authorize(Namespace = "channels", ObjectIdFromProperty = "ChannelId", Relation = "write")]
public class ForwardMessageCommand : ChannelScopedRequest<MessageBriefDto>
{
    public Guid MessageId { get; set; }
    public string Body { get; set; }
}

public class ForwardMessageCommandHandler : IRequestHandler<ForwardMessageCommand, MessageBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ForwardMessageCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MessageBriefDto> Handle(ForwardMessageCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var message = await _context.Messages.FindAsync(new object?[] { request.MessageId }, cancellationToken) ??
                          throw new NotFoundException("message", request.MessageId);

            var originalMessageRecipient = await _context.MessageRecipients
                .Where(mr => mr.MessageId == message.Id)
                .Where(mr => mr.RecipientId == request.User.Id)
                .SingleAsync(cancellationToken);

            var recipients = await _context.ChannelUsers
                .Where(cu => cu.ChannelId == request.ChannelId)
                .ToListAsync(cancellationToken);

            if (!recipients.Any())
            {
                throw new ApplicationException { Details = "there is no user in the given channel" };
            }

            var messageRecipients = recipients
                .Select(recipient => new MessageRecipient(recipient.UserId, message.Id, request.ChannelId,
                    originalMessageRecipient.Id));

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
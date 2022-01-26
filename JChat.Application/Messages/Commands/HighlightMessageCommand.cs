using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Entities.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JChat.Application.Messages.Commands;

public class HighlightMessageCommand : BaseRequest<Unit>
{
    public Guid MessageId { get; set; }
    public bool IsHighlighted { get; set; }
}

public class HighlightMessageCommandHandler : IRequestHandler<HighlightMessageCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public HighlightMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(HighlightMessageCommand request, CancellationToken cancellationToken)
    {
        if (request.IsHighlighted)
        {
            var messageHighlight = new MessageHighlight(request.MessageId);
            await _context.MessageHighlights.AddAsync(messageHighlight, cancellationToken);
        }
        else
        {
            var messageHighlight = await _context.MessageHighlights
                .Where(mh => mh.MessageId == request.MessageId)
                .Where(mh => mh.CreatedById == request.User.Id)
                .SingleAsync(cancellationToken);
            _context.MessageHighlights.Remove(messageHighlight);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
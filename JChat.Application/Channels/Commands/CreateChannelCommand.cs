using FluentValidation;
using JChat.Application.Shared.Interfaces;
using JChat.Domain.Interfaces;
using MediatR;

namespace JChat.Application.Channels.Commands;

public class CreateChannelCommand : IRequest<int>
{

}

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateChannelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        // await _aggregateRepository.SaveAsync();
        return await _context.SaveChangesAsync(cancellationToken);
        // TODO: permission to user manage channel
    }
}

public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
{
    public CreateChannelCommandValidator()
    {
    }
}

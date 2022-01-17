using FluentValidation;
using JChat.Application.Shared.CQRS;
using JChat.Application.Shared.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JChat.Application;

[Authorize]
public record TestCommand(string Arg) : BaseRequest, IRequest;

public class TestCommandValidator : AbstractValidator<TestCommand>
{
    public TestCommandValidator()
    {
        RuleFor(t => t.Arg)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(5);
    }
}

internal class TestCommandHandler : IRequestHandler<TestCommand>
{
    private readonly ILogger<TestCommandHandler> _logger;

    public TestCommandHandler(ILogger<TestCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("handling test command!");
        return Task.FromResult(Unit.Value);
    }
}


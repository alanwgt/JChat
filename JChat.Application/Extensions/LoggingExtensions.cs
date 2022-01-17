using JChat.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace JChat.Application.Extensions;

public static class LoggingExtensions
{
    public static void LogDomainMessage<T>(this ILogger<T> logger, string message,
        LogLevel level = LogLevel.Information, params object[]? args)
        => logger.Log(level, message, args);

    public static void LogEvent<T>(this ILogger<T> logger, IDomainEvent @event, string message, params object[]? args)
        => logger.LogInformation("[{Name}]: {Message}", @event.GetType().Name, message, args);
}
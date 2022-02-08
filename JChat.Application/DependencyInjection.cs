using System.Reflection;
using JChat.Application.Shared.Behaviors;
using FluentValidation;
using JChat.Application.Notifications;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Services;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace JChat.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<ICurrentWorkspaceService, CurrentWorkspaceService>();
        services.AddTransient<INotificationCenter, NotificationCenter>();
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehavior<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

        return services;
    }
}

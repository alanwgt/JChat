using JChat.Application.Shared.Interfaces;
using JChat.Infrastructure.Identity;
using JChat.Infrastructure.Interfaces;
using JChat.Infrastructure.Models;
using JChat.Infrastructure.Persistence;
using JChat.Infrastructure.Persistence.Services;
using JChat.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JChat.Infrastructure;

public static class DependencyInjection
{
    public static Task<IServiceCollection> AddInfrastructure(this IServiceCollection services,
        InfrastructureConfig infrastructureConfig)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(infrastructureConfig.DatabaseConnectionString)
                .UseSnakeCaseNamingConvention();

#if DEBUG
            options.EnableSensitiveDataLogging()
                .EnableDetailedErrors();
#endif
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddSingleton(infrastructureConfig);
        services.AddSingleton<IInfrastructureConfig>(provider => provider.GetRequiredService<InfrastructureConfig>());

        return Task.FromResult(services);
    }
}
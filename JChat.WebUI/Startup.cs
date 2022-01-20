using FluentValidation.AspNetCore;
using JChat.Application;
using JChat.Application.Notifications;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Services;
using JChat.Infrastructure;
using JChat.Infrastructure.Files;
using JChat.Infrastructure.Models;
using JChat.WebUI.Filters;
using JChat.WebUI.Security;
using Microsoft.AspNetCore.Mvc;

namespace JChat.WebUI;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), "..", ".env"));
        var presentationUrl = Environment.GetEnvironmentVariable("PRESENTATION_URL");

        services.AddHttpContextAccessor();
        services.AddApplication();
        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>()
        ).AddFluentValidation(x => x.AutomaticValidationEnabled = false);
        services.AddInfrastructure(new InfrastructureConfig(
            Environment.GetEnvironmentVariable("KRATOS_PUBLIC_URL"),
            Environment.GetEnvironmentVariable("KETO_WRITE_URL"),
            Environment.GetEnvironmentVariable("KETO_READ_URL"),
            Environment.GetEnvironmentVariable("APP_DATABASE_CONNECTION_STRING")
        ));
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHealthChecks();
        services.AddAuthentication("Kratos")
            .AddScheme<KratosAuthenticationOptions, KratosAuthenticationHandler>("Kratos", null);

        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument(x => { x.Title = "JChat"; });

        services.AddSignalR();

        services.AddCors(options =>
        {
            options.AddPolicy("dev", policy =>
            {
                policy
                    .WithOrigins(presentationUrl)
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("X-Workspace-Id")
                    .AllowAnyHeader();
            });

            options.AddPolicy("prod", policy =>
            {
                policy
                    .WithOrigins(presentationUrl)
                    .WithMethods("GET", "POST")
                    .WithExposedHeaders("X-Workspace-Id")
                    .AllowCredentials();
            });
        });

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            // Adds the OpenAPI/Swagger document generator middleware
            // serve documents (same as app.UseSwagger())
            // app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseHealthChecks("/health");
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(env.IsDevelopment() ? "dev" : "prod");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatHub>("/hub");
        });
    }
}
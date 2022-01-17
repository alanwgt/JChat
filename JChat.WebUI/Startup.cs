using FluentValidation.AspNetCore;
using JChat.Application;
using JChat.Application.Notifications;
using JChat.Application.Shared.Interfaces;
using JChat.Application.Shared.Services;
using JChat.Infrastructure;
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
        services.AddHttpContextAccessor();
        services.AddApplication();
        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>()
        ).AddFluentValidation(x => x.AutomaticValidationEnabled = false);
        services.AddInfrastructure(Configuration);
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
                    .WithOrigins("http://jchat.alanwgt.com")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
            });

            options.AddPolicy("prod", policy =>
            {
                // TODO
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

        // app.MapControllers();
    }
}
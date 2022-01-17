using JChat.Infrastructure.Files;

namespace JChat.WebUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), "..", ".env"));

        // using (var scope = host.Services.CreateScope())
        // {
        //     var services = scope.ServiceProvider;
        //
        //     try
        //     {
        //         var context = services.GetRequiredService<ApplicationDbContext>();
        //
        //         if (context.Database.IsSqlServer())
        //         {
        //             context.Database.Migrate();
        //         }
        //
        //         var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        //         var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //
        //         await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
        //         await ApplicationDbContextSeed.SeedSampleDataAsync(context);
        //     }
        //     catch (Exception ex)
        //     {
        //         var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        //
        //         logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        //
        //         throw;
        //     }
        // }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TestingTracking.Infrastructure.InfrastructureDbContext;
using TestingTracking.Infrastructure.DataConfigurations;
using TestingTracking.Console.Commands;

class Program
{
    static void Main(string[] args)

    {
        var serviceProvider = BuildServiceProvider();
        InitializeDatabase(serviceProvider);

        var menuItems = serviceProvider.GetRequiredService<MenuItems>();
        menuItems.RunProgram();
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();


        // Get the base directory of the application
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Navigate to the desired directory relative to the base directory
        string targetDirectory = Path.Combine(baseDirectory, @"..\..\..\");

        // Normalize the path
        targetDirectory = Path.GetFullPath(targetDirectory);

        // Verify the change
        Console.WriteLine("Target Directory: " + targetDirectory);

        // Build configuration
        var configuration = new ConfigurationBuilder()
                        .SetBasePath(targetDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Register DbContext with the connection string from configuration
        services.AddDbContext<ItemsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register other services
        services.AddTransient<MenuItems>();
        services.AddTransient<DatabaseSeeder>();
        services.AddLogging(configure => configure.AddConsole());

        // Register IConfiguration instance
        services.AddSingleton<IConfiguration>(configuration);

        return services.BuildServiceProvider();
    }

    private static void InitializeDatabase(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ItemsDbContext>();
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

           
            context.Database.Migrate();

            // Seed the database
            seeder.Seed();
        }
    }
}

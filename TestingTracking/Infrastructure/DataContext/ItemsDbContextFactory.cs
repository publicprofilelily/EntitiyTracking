using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TestingTracking.Infrastructure.InfrastructureDbContext;

namespace TestingTracking.Infrastructure.DataContext
{
    public class ItemsDbContextFactory : IDesignTimeDbContextFactory<ItemsDbContext>
    {
        public ItemsDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ItemsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new ItemsDbContext(optionsBuilder.Options);
        }
    }
}

using TestingTracking.Infrastructure.InfrastructureDbContext;
using TestingTracking.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace TestingTracking.Infrastructure.DataConfigurations
{
    public class DatabaseSeeder
    {
        private readonly ItemsDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(ItemsDbContext context, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Seed()
        {
            try
            {
                if (!_context.Assets.Any())
                {
                    _context.Assets.AddRange(
                        new Asset { Type = "Phone", Brand = "iPhone", Model = "8", Office = "Spain", PurchaseDate = new DateTime(2021, 08, 05), PriceInUSD = 970m, Currency = "EUR", LocalPriceToday=1000m },
                        new Asset { Type = "Computer", Brand = "HP", Model = "Elitebook", Office = "Spain", PurchaseDate = new DateTime(2021, 10, 11), PriceInUSD = 1423m, Currency = "EUR", LocalPriceToday = 1500m },
                        new Asset { Type = "Phone", Brand = "iPhone", Model = "11", Office = "Spain", PurchaseDate = new DateTime(2021, 11, 25), PriceInUSD = 990m, Currency = "EUR", LocalPriceToday = 1100m },
                        new Asset { Type = "Phone", Brand = "iPhone", Model = "X", Office = "Sweden", PurchaseDate = new DateTime(2021, 12, 15), PriceInUSD = 1245m, Currency = "SEK", LocalPriceToday = 10000m },
                        new Asset { Type = "Phone", Brand = "Motorola", Model = "Razr", Office = "Sweden", PurchaseDate = new DateTime(2020, 3, 16), PriceInUSD = 970m, Currency = "SEK" , LocalPriceToday = 9700m },
                        new Asset { Type = "Computer", Brand = "HP", Model = "Elitebook", Office = "Sweden", PurchaseDate = new DateTime(2020, 10, 10), PriceInUSD = 588m, Currency = "SEK", LocalPriceToday = 5800m }
                    );

                    _context.SaveChanges();
                    _logger.LogInformation("Database seeded successfully.");
                }
                else
                {
                    _logger.LogInformation("Seeding skipped: Data already exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding the database.");
                throw;
            }
        }
    }
}

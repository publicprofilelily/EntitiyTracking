// File: /Infrastructure/DbContext/ItemsDbContext.cs

using TestingTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestingTracking.Infrastructure.InfrastructureDbContext
{
    public class ItemsDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Office).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PurchaseDate).IsRequired();
                entity.Property(e => e.PriceInUSD).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Currency).IsRequired().HasMaxLength(10);
            });
        }
    }
}

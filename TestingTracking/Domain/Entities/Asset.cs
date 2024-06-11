// File: /Domain/Entities/Asset.cs

namespace TestingTracking.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Office { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public decimal PriceInUSD { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal LocalPriceToday { get; set; }
    }

    
}

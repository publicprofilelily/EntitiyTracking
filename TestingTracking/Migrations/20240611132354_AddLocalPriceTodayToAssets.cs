using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalPriceTodayToAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LocalPriceToday",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPriceToday",
                table: "Assets");
        }
    }
}

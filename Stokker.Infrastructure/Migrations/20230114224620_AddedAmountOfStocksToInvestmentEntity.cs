using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stokker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAmountOfStocksToInvestmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfStocks",
                table: "Investments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfStocks",
                table: "Investments");
        }
    }
}

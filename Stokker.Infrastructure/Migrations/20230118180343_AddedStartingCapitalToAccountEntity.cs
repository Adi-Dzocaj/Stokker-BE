using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stokker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedStartingCapitalToAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StartingCapital",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingCapital",
                table: "Accounts");
        }
    }
}

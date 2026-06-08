using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenEconomia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFilePricePerUnityToPrductEnterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PricePerUnity",
                table: "ProductEnters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnity",
                table: "ProductEnters");
        }
    }
}

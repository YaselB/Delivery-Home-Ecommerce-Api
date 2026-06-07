using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenEconomia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductEnterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductEnters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    PriceCUP = table.Column<double>(type: "double precision", nullable: false),
                    PriceUSD = table.Column<double>(type: "double precision", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEnters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEnters_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEnters_ProductId",
                table: "ProductEnters",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEnters");
        }
    }
}

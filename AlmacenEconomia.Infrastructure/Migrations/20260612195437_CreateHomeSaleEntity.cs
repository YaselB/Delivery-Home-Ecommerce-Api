using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenEconomia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateHomeSaleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeSales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeSaleDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    HomeSaleId = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeSaleDetails_HomeSales_HomeSaleId",
                        column: x => x.HomeSaleId,
                        principalTable: "HomeSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSaleDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeSaleDetails_HomeSaleId",
                table: "HomeSaleDetails",
                column: "HomeSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSaleDetails_ProductId",
                table: "HomeSaleDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeSaleDetails");

            migrationBuilder.DropTable(
                name: "HomeSales");
        }
    }
}

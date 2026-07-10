using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenEconomia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsBetweenAdminSaleDetailsAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminSaleDetails_Products_AdminSaleId",
                table: "AdminSaleDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AdminSaleDetails_ProductId",
                table: "AdminSaleDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminSaleDetails_Products_ProductId",
                table: "AdminSaleDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminSaleDetails_Products_ProductId",
                table: "AdminSaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_AdminSaleDetails_ProductId",
                table: "AdminSaleDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminSaleDetails_Products_AdminSaleId",
                table: "AdminSaleDetails",
                column: "AdminSaleId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

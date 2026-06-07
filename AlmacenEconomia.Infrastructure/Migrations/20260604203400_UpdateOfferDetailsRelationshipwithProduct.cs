using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenEconomia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOfferDetailsRelationshipwithProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferDetails_Products_OfferId",
                table: "OfferDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OfferDetails_ProductId",
                table: "OfferDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDetails_Products_ProductId",
                table: "OfferDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferDetails_Products_ProductId",
                table: "OfferDetails");

            migrationBuilder.DropIndex(
                name: "IX_OfferDetails_ProductId",
                table: "OfferDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDetails_Products_OfferId",
                table: "OfferDetails",
                column: "OfferId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class returnIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_MinProductItemId",
                table: "Products",
                column: "MinProductItemId",
                unique: true,
                filter: "[MinProductItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductItems_MinProductItemId",
                table: "Products",
                column: "MinProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductItems_MinProductItemId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MinProductItemId",
                table: "Products");
        }
    }
}

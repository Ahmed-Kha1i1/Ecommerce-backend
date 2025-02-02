using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHandlingPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "Products",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinProductItemId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MinPrice",
                table: "Products",
                column: "MinPrice");

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
                name: "IX_Products_MinPrice",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MinProductItemId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinProductItemId",
                table: "Products");
        }
    }
}

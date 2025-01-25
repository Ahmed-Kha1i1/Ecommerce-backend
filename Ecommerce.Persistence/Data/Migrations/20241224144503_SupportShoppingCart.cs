using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupportShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Orders",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)1,
                comment: "1-Placed,2-Confirmed,3-Processing,4-Shipped,5-InTransit,6-OutForDelivery,7-Delivered,8-Canceled,9-AttemptedDelivery,10-Lost",
                oldClrType: typeof(byte),
                oldType: "TINYINT",
                oldDefaultValue: (byte)0,
                oldComment: "0-Opened 1-Placed,2-Confirmed,3-Processing,4-Shipped,5-InTransit,6-OutForDelivery,7-Delivered,8-Canceled,9-AttemptedDelivery,10-Lost");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ProductItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductItemId",
                table: "ShoppingCartItems",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerId",
                table: "ShoppingCarts",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Orders",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)0,
                comment: "0-Opened 1-Placed,2-Confirmed,3-Processing,4-Shipped,5-InTransit,6-OutForDelivery,7-Delivered,8-Canceled,9-AttemptedDelivery,10-Lost",
                oldClrType: typeof(byte),
                oldType: "TINYINT",
                oldDefaultValue: (byte)1,
                oldComment: "1-Placed,2-Confirmed,3-Processing,4-Shipped,5-InTransit,6-OutForDelivery,7-Delivered,8-Canceled,9-AttemptedDelivery,10-Lost");
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_Stars",
                table: "Products",
                column: "Stars")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Condition",
                table: "Products",
                column: "Condition")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_Price",
                table: "ProductItems",
                column: "Price")
                .Annotation("SqlServer:Clustered", false);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Condition",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Stars",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_Price",
                table: "ProductItems");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateBaseAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "OrderAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "OrderAddresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "OrderAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "OrderAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderAddresses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "OrderAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_CountryId",
                table: "OrderAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_CustomerId",
                table: "OrderAddresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddresses_Countries_CountryId",
                table: "OrderAddresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAddresses_Customers_CustomerId",
                table: "OrderAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddresses_Countries_CountryId",
                table: "OrderAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderAddresses_Customers_CustomerId",
                table: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderAddresses_CountryId",
                table: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderAddresses_CustomerId",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderAddresses");

            migrationBuilder.DropColumn(
                name: "State",
                table: "OrderAddresses");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

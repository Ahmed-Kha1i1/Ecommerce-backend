using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenderRequered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: true,
                comment: "0-Male,1-Female",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "0-Male,1-Female");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "0-Male,1-Female",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "0-Male,1-Female");
        }
    }
}

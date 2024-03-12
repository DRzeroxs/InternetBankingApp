using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBankingApp.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartAmount",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "StartAmount",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

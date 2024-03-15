using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBankingApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IdentifiersAddToCreditCardAndPrestamo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Identifier",
                table: "TarjetasDeCredito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Identifier",
                table: "Prestamos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "TarjetasDeCredito");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Prestamos");
        }
    }
}

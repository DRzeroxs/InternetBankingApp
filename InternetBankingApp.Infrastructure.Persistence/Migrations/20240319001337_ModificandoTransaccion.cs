using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBankingApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoTransaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_CuentasDeAhorro_CuentaDestinoId",
                table: "Transacciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_CuentasDeAhorro_CuentaOrigenId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_CuentaDestinoId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_CuentaOrigenId",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "CuentaOrigenId",
                table: "Transacciones",
                newName: "ProductOrigenIde");

            migrationBuilder.RenameColumn(
                name: "CuentaDestinoId",
                table: "Transacciones",
                newName: "ProductDestinoIde");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductOrigenIde",
                table: "Transacciones",
                newName: "CuentaOrigenId");

            migrationBuilder.RenameColumn(
                name: "ProductDestinoIde",
                table: "Transacciones",
                newName: "CuentaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaDestinoId",
                table: "Transacciones",
                column: "CuentaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaOrigenId",
                table: "Transacciones",
                column: "CuentaOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_CuentasDeAhorro_CuentaDestinoId",
                table: "Transacciones",
                column: "CuentaDestinoId",
                principalTable: "CuentasDeAhorro",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_CuentasDeAhorro_CuentaOrigenId",
                table: "Transacciones",
                column: "CuentaOrigenId",
                principalTable: "CuentasDeAhorro",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBankingApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDeTransaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones");

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
                name: "ClienteId",
                table: "Transacciones",
                newName: "clienteId");

            migrationBuilder.RenameColumn(
                name: "CuentaOrigenId",
                table: "Transacciones",
                newName: "ProductOrigenIde");

            migrationBuilder.RenameColumn(
                name: "CuentaDestinoId",
                table: "Transacciones",
                newName: "ProductDestinoIde");

            migrationBuilder.RenameIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones",
                newName: "IX_Transacciones_clienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_clienteId",
                table: "Transacciones",
                column: "clienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_clienteId",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "clienteId",
                table: "Transacciones",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "ProductOrigenIde",
                table: "Transacciones",
                newName: "CuentaOrigenId");

            migrationBuilder.RenameColumn(
                name: "ProductDestinoIde",
                table: "Transacciones",
                newName: "CuentaDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacciones_clienteId",
                table: "Transacciones",
                newName: "IX_Transacciones_ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaDestinoId",
                table: "Transacciones",
                column: "CuentaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CuentaOrigenId",
                table: "Transacciones",
                column: "CuentaOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

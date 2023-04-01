using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_tareas.Migrations
{
    public partial class addTwoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "cuentas",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroCuenta = table.Column<long>(type: "bigint", nullable: false),
                    moneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    situacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    operacion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuentas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transacciones",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuentaId = table.Column<long>(type: "bigint", nullable: false),
                    registerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    oficina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    debito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    abono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false),
                    transaccionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacciones", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cuentas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "transacciones",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.id);
                });
        }
    }
}

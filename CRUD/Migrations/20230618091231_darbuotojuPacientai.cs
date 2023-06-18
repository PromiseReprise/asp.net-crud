using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Migrations
{
    /// <inheritdoc />
    public partial class darbuotojuPacientai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vardas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pavarde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GimimoData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DarbuotojuPacientai",
                columns: table => new
                {
                    DarbuotojaiId = table.Column<int>(type: "int", nullable: false),
                    PacientaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DarbuotojuPacientai", x => new { x.DarbuotojaiId, x.PacientaiId });
                    table.ForeignKey(
                        name: "FK_DarbuotojuPacientai_Darbuotojai_DarbuotojaiId",
                        column: x => x.DarbuotojaiId,
                        principalTable: "Darbuotojai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DarbuotojuPacientai_Pacientai_PacientaiId",
                        column: x => x.PacientaiId,
                        principalTable: "Pacientai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DarbuotojuPacientai_PacientaiId",
                table: "DarbuotojuPacientai",
                column: "PacientaiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DarbuotojuPacientai");

            migrationBuilder.DropTable(
                name: "Pacientai");
        }
    }
}

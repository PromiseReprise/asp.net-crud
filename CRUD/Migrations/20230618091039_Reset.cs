using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Migrations
{
    /// <inheritdoc />
    public partial class Reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "Darbuotojai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vardas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pavarde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GimimoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statusas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Darbuotojai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pareigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pareigos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pareigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DarbuotojuPareigos",
                columns: table => new
                {
                    DarbuotojaiId = table.Column<int>(type: "int", nullable: false),
                    PareigosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DarbuotojuPareigos", x => new { x.DarbuotojaiId, x.PareigosId });
                    table.ForeignKey(
                        name: "FK_DarbuotojuPareigos_Darbuotojai_DarbuotojaiId",
                        column: x => x.DarbuotojaiId,
                        principalTable: "Darbuotojai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DarbuotojuPareigos_Pareigos_PareigosId",
                        column: x => x.PareigosId,
                        principalTable: "Pareigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DarbuotojuPareigos_PareigosId",
                table: "DarbuotojuPareigos",
                column: "PareigosId");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DarbuotojuPareigos");

            migrationBuilder.DropTable(
                name: "Darbuotojai");

            migrationBuilder.DropTable(
                name: "Pareigos");
        }
    }
}

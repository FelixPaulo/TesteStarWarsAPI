using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planeta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planeta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Diretor = table.Column<string>(maxLength: 300, nullable: false),
                    DataLancamento = table.Column<DateTime>(nullable: false),
                    PlanetaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filme_Planeta_PlanetaId",
                        column: x => x.PlanetaId,
                        principalTable: "Planeta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filme_PlanetaId",
                table: "Filme",
                column: "PlanetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Planeta");
        }
    }
}

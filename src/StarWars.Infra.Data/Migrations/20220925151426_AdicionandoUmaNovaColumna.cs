using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.Infra.Data.Migrations
{
    public partial class AdicionandoUmaNovaColumna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Clima",
                table: "Planeta",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clima",
                table: "Planeta");
        }
    }
}

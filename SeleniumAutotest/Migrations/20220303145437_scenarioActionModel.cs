using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeleniumAutotest.Migrations
{
    public partial class scenarioActionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContinueOnError",
                table: "ScenarioActions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContinueOnError",
                table: "ScenarioActions");
        }
    }
}

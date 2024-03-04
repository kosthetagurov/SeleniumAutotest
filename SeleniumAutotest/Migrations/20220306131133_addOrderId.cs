using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeleniumAutotest.Migrations
{
    public partial class addOrderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ScenarioActions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ScenarioActions");
        }
    }
}

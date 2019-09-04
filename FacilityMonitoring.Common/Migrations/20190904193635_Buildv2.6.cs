using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Units",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Units",
                table: "Categories");
        }
    }
}

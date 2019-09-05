using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Build29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxValue",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ZeroValue",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "ZeroValue",
                table: "Channels");
        }
    }
}

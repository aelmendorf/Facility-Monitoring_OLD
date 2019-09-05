using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Build31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCalibration",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ZeroValue",
                table: "Categories",
                newName: "ZeroPoint");

            migrationBuilder.RenameColumn(
                name: "ZeroCalibration",
                table: "Categories",
                newName: "MaxPoint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZeroPoint",
                table: "Categories",
                newName: "ZeroValue");

            migrationBuilder.RenameColumn(
                name: "MaxPoint",
                table: "Categories",
                newName: "ZeroCalibration");

            migrationBuilder.AddColumn<double>(
                name: "MaxCalibration",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaxValue",
                table: "Categories",
                nullable: true);
        }
    }
}

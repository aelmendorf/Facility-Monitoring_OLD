using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ReadInterval",
                table: "ModbusDevices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaveInterval",
                table: "ModbusDevices",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadInterval",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "SaveInterval",
                table: "ModbusDevices");
        }
    }
}

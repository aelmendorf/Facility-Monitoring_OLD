using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateAddr",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BypassAll",
                table: "ModbusDevices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateAddr",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "BypassAll",
                table: "ModbusDevices");
        }
    }
}

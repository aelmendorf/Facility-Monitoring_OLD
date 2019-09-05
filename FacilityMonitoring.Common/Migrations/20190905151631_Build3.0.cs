using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Build30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlarmAddr",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModbusComAddr",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoftwareMaintAddr",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarningAddr",
                table: "ModbusDevices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmAddr",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "ModbusComAddr",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "SoftwareMaintAddr",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "WarningAddr",
                table: "ModbusDevices");
        }
    }
}

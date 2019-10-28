using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class AppendAlertEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlarmAction",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarningAction",
                table: "ModbusDevices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmAction",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "WarningAction",
                table: "ModbusDevices");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Redesignefcore3v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmmoniaControllerAlert_AmmoniaControllerReadings_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlert");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmmoniaControllerAlert",
                table: "AmmoniaControllerAlert");

            migrationBuilder.RenameTable(
                name: "AmmoniaControllerAlert",
                newName: "AmmoniaControllerAlerts");

            migrationBuilder.RenameIndex(
                name: "IX_AmmoniaControllerAlert_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlerts",
                newName: "IX_AmmoniaControllerAlerts_AmmoniaControllerReadingId");

            migrationBuilder.AddColumn<int>(
                name: "AlarmSetPoint",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarningSetPoint",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmmoniaControllerAlerts",
                table: "AmmoniaControllerAlerts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmmoniaControllerAlerts_AmmoniaControllerReadings_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlerts",
                column: "AmmoniaControllerReadingId",
                principalTable: "AmmoniaControllerReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmmoniaControllerAlerts_AmmoniaControllerReadings_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmmoniaControllerAlerts",
                table: "AmmoniaControllerAlerts");

            migrationBuilder.DropColumn(
                name: "AlarmSetPoint",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "WarningSetPoint",
                table: "ModbusDevices");

            migrationBuilder.RenameTable(
                name: "AmmoniaControllerAlerts",
                newName: "AmmoniaControllerAlert");

            migrationBuilder.RenameIndex(
                name: "IX_AmmoniaControllerAlerts_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlert",
                newName: "IX_AmmoniaControllerAlert_AmmoniaControllerReadingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmmoniaControllerAlert",
                table: "AmmoniaControllerAlert",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmmoniaControllerAlert_AmmoniaControllerReadings_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlert",
                column: "AmmoniaControllerReadingId",
                principalTable: "AmmoniaControllerReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

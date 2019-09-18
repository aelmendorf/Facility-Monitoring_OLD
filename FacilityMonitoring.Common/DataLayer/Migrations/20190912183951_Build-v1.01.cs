using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmmoniaBoxReadings_ModbusDevices_AmmoniaControllerId",
                table: "AmmoniaBoxReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmmoniaBoxReadings",
                table: "AmmoniaBoxReadings");

            migrationBuilder.RenameTable(
                name: "AmmoniaBoxReadings",
                newName: "AmmoniaControllerReadings");

            migrationBuilder.RenameIndex(
                name: "IX_AmmoniaBoxReadings_AmmoniaControllerId",
                table: "AmmoniaControllerReadings",
                newName: "IX_AmmoniaControllerReadings_AmmoniaControllerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmmoniaControllerReadings",
                table: "AmmoniaControllerReadings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmmoniaControllerReadings_ModbusDevices_AmmoniaControllerId",
                table: "AmmoniaControllerReadings",
                column: "AmmoniaControllerId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmmoniaControllerReadings_ModbusDevices_AmmoniaControllerId",
                table: "AmmoniaControllerReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AmmoniaControllerReadings",
                table: "AmmoniaControllerReadings");

            migrationBuilder.RenameTable(
                name: "AmmoniaControllerReadings",
                newName: "AmmoniaBoxReadings");

            migrationBuilder.RenameIndex(
                name: "IX_AmmoniaControllerReadings_AmmoniaControllerId",
                table: "AmmoniaBoxReadings",
                newName: "IX_AmmoniaBoxReadings_AmmoniaControllerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AmmoniaBoxReadings",
                table: "AmmoniaBoxReadings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AmmoniaBoxReadings_ModbusDevices_AmmoniaControllerId",
                table: "AmmoniaBoxReadings",
                column: "AmmoniaControllerId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

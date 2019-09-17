using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_AmmoniaControllerId",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_GenericMonitorBoxId",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_H2GeneratorId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_AmmoniaControllerId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_H2GeneratorId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "AmmoniaControllerId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "H2GeneratorId",
                table: "Registers");

            migrationBuilder.RenameColumn(
                name: "GenericMonitorBoxId",
                table: "Registers",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_GenericMonitorBoxId",
                table: "Registers",
                newName: "IX_Registers_DeviceId");

            migrationBuilder.RenameColumn(
                name: "PSV_A1A2",
                table: "H2GenReadings",
                newName: "PSV_A2");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_DeviceId",
                table: "Registers",
                column: "DeviceId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_DeviceId",
                table: "Registers");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Registers",
                newName: "GenericMonitorBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_DeviceId",
                table: "Registers",
                newName: "IX_Registers_GenericMonitorBoxId");

            migrationBuilder.RenameColumn(
                name: "PSV_A2",
                table: "H2GenReadings",
                newName: "PSV_A1A2");

            migrationBuilder.AddColumn<int>(
                name: "AmmoniaControllerId",
                table: "Registers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "H2GeneratorId",
                table: "Registers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registers_AmmoniaControllerId",
                table: "Registers",
                column: "AmmoniaControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_H2GeneratorId",
                table: "Registers",
                column: "H2GeneratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_AmmoniaControllerId",
                table: "Registers",
                column: "AmmoniaControllerId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_GenericMonitorBoxId",
                table: "Registers",
                column: "GenericMonitorBoxId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_H2GeneratorId",
                table: "Registers",
                column: "H2GeneratorId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

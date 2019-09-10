using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Categories_SensorTypeId",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Categories_SensorTypeId1",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Categories_SensorTypeId2",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_ModbusDevices_GenericMonitorBoxId",
                table: "Channels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Channels",
                table: "Channels");

            migrationBuilder.RenameTable(
                name: "Channels",
                newName: "Registers");

            migrationBuilder.RenameIndex(
                name: "IX_Channels_GenericMonitorBoxId",
                table: "Registers",
                newName: "IX_Registers_GenericMonitorBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Channels_SensorTypeId2",
                table: "Registers",
                newName: "IX_Registers_SensorTypeId2");

            migrationBuilder.RenameIndex(
                name: "IX_Channels_SensorTypeId1",
                table: "Registers",
                newName: "IX_Registers_SensorTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Channels_SensorTypeId",
                table: "Registers",
                newName: "IX_Registers_SensorTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registers",
                table: "Registers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Categories_SensorTypeId",
                table: "Registers",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Categories_SensorTypeId1",
                table: "Registers",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Categories_SensorTypeId2",
                table: "Registers",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_GenericMonitorBoxId",
                table: "Registers",
                column: "GenericMonitorBoxId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId1",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId2",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_GenericMonitorBoxId",
                table: "Registers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registers",
                table: "Registers");

            migrationBuilder.RenameTable(
                name: "Registers",
                newName: "Channels");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_GenericMonitorBoxId",
                table: "Channels",
                newName: "IX_Channels_GenericMonitorBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_SensorTypeId2",
                table: "Channels",
                newName: "IX_Channels_SensorTypeId2");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_SensorTypeId1",
                table: "Channels",
                newName: "IX_Channels_SensorTypeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Registers_SensorTypeId",
                table: "Channels",
                newName: "IX_Channels_SensorTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Channels",
                table: "Channels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Categories_SensorTypeId",
                table: "Channels",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Categories_SensorTypeId1",
                table: "Channels",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Categories_SensorTypeId2",
                table: "Channels",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_ModbusDevices_GenericMonitorBoxId",
                table: "Channels",
                column: "GenericMonitorBoxId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

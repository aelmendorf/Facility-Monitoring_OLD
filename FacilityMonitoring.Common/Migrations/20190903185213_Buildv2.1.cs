using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channel_Categories_SensorTypeId",
                table: "Channel");

            migrationBuilder.DropForeignKey(
                name: "FK_Channel_ModbusDevices_GenericMonitorBoxId",
                table: "Channel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Channel",
                table: "Channel");

            migrationBuilder.RenameTable(
                name: "Channel",
                newName: "Channels");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_GenericMonitorBoxId",
                table: "Channels",
                newName: "IX_Channels_GenericMonitorBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_SensorTypeId",
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
                name: "FK_Channels_ModbusDevices_GenericMonitorBoxId",
                table: "Channels",
                column: "GenericMonitorBoxId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Categories_SensorTypeId",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_ModbusDevices_GenericMonitorBoxId",
                table: "Channels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Channels",
                table: "Channels");

            migrationBuilder.RenameTable(
                name: "Channels",
                newName: "Channel");

            migrationBuilder.RenameIndex(
                name: "IX_Channels_GenericMonitorBoxId",
                table: "Channel",
                newName: "IX_Channel_GenericMonitorBoxId");

            migrationBuilder.RenameIndex(
                name: "IX_Channels_SensorTypeId",
                table: "Channel",
                newName: "IX_Channel_SensorTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Channel",
                table: "Channel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_Categories_SensorTypeId",
                table: "Channel",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_ModbusDevices_GenericMonitorBoxId",
                table: "Channel",
                column: "GenericMonitorBoxId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

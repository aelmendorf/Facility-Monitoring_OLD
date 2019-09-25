using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildef3newv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId1",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId2",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId3",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_SensorTypeId1",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_SensorTypeId2",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_SensorTypeId3",
                table: "Registers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId1",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId2",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId3",
                table: "Registers",
                column: "SensorTypeId");

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
                name: "FK_Registers_Categories_SensorTypeId3",
                table: "Registers",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

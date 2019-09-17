using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "Registers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FunctionCode",
                table: "Registers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId3",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Categories_SensorTypeId3",
                table: "Registers",
                column: "SensorTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Categories_SensorTypeId3",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_SensorTypeId3",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "FunctionCode",
                table: "Registers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Channels_SensorTypeId1",
                table: "Channels",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_SensorTypeId2",
                table: "Channels",
                column: "SensorTypeId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Categories_SensorTypeId1",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Categories_SensorTypeId2",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_SensorTypeId1",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_SensorTypeId2",
                table: "Channels");
        }
    }
}

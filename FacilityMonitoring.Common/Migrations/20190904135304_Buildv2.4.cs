using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Channels");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ModbusDevices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DigitalOutputChannel_Logic",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "DigitalOutputChannel_Logic",
                table: "Channels");

            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "Channels",
                nullable: true);
        }
    }
}

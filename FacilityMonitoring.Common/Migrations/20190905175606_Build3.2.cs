using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Build32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Alarm1",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alarm2",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alarm3",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Alarm1Action",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alarm1Enabled",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Alarm2Action",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alarm2Enabled",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Alarm3Action",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alarm3Enabled",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlarmAction",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alarm1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "Alarm2",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "Alarm3",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "Alarm1Action",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Alarm1Enabled",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Alarm2Action",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Alarm2Enabled",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Alarm3Action",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Alarm3Enabled",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "AlarmAction",
                table: "Channels");
        }
    }
}

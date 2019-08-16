using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class GenericMonitoringBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AnalogCh1",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh10",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh11",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh12",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh13",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh14",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh15",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh16",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh2",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh3",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh4",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh5",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh6",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh7",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh8",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnalogCh9",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh1",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh10",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh11",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh12",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh13",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh14",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh15",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh16",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh17",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh18",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh19",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh2",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh20",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh21",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh22",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh23",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh24",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh25",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh26",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh27",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh28",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh29",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh3",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh30",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh31",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh32",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh33",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh34",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh35",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh36",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh37",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh38",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh4",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh5",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh6",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh7",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh8",
                table: "Readings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DigitalCh9",
                table: "Readings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalogCh1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh10",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh11",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh12",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh13",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh14",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh15",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh16",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh2",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh3",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh4",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh5",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh6",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh7",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh8",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "AnalogCh9",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh1",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh10",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh11",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh12",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh13",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh14",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh15",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh16",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh17",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh18",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh19",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh2",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh20",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh21",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh22",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh23",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh24",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh25",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh26",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh27",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh28",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh29",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh3",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh30",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh31",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh32",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh33",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh34",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh35",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh36",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh37",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh38",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh4",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh5",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh6",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh7",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh8",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DigitalCh9",
                table: "Readings");
        }
    }
}

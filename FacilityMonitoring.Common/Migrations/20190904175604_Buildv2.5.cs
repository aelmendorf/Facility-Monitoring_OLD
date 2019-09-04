using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitalOutputChannel_Logic",
                table: "Channels");

            migrationBuilder.AlterColumn<int>(
                name: "Logic",
                table: "Channels",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Logic",
                table: "Channels",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DigitalOutputChannel_Logic",
                table: "Channels",
                nullable: true);
        }
    }
}

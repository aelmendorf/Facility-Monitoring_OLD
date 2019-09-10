using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChannelNumber",
                table: "Channels",
                newName: "RegisterLength");

            migrationBuilder.AddColumn<int>(
                name: "RegisterIndex",
                table: "Channels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterIndex",
                table: "Channels");

            migrationBuilder.RenameColumn(
                name: "RegisterLength",
                table: "Channels",
                newName: "ChannelNumber");
        }
    }
}

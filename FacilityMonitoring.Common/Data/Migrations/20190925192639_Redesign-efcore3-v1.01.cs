using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Redesignefcore3v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Tank1AlertEnabled",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tank2AlertEnabled",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tank3AlertEnabled",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tank4AlertEnabled",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AmmoniaControllerAlert",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmmoniaControllerReadingId = table.Column<int>(nullable: false),
                    Tank1Alert = table.Column<int>(nullable: false),
                    Tank2Alert = table.Column<int>(nullable: false),
                    Tank3Alert = table.Column<int>(nullable: false),
                    Tank4Alert = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmmoniaControllerAlert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmmoniaControllerAlert_AmmoniaControllerReadings_AmmoniaControllerReadingId",
                        column: x => x.AmmoniaControllerReadingId,
                        principalTable: "AmmoniaControllerReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmmoniaControllerAlert_AmmoniaControllerReadingId",
                table: "AmmoniaControllerAlert",
                column: "AmmoniaControllerReadingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmmoniaControllerAlert");

            migrationBuilder.DropColumn(
                name: "Tank1AlertEnabled",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "Tank2AlertEnabled",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "Tank3AlertEnabled",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "Tank4AlertEnabled",
                table: "ModbusDevices");
        }
    }
}

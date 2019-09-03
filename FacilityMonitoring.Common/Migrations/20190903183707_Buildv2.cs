using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DigitalChannels_ModbusDevices_GenericMonitorBoxId",
                table: "DigitalChannels");

            migrationBuilder.DropTable(
                name: "AnalogChannels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DigitalChannels",
                table: "DigitalChannels");

            migrationBuilder.RenameTable(
                name: "DigitalChannels",
                newName: "Channel");

            migrationBuilder.RenameIndex(
                name: "IX_DigitalChannels_GenericMonitorBoxId",
                table: "Channel",
                newName: "IX_Channel_GenericMonitorBoxId");

            migrationBuilder.AlterColumn<int>(
                name: "Logic",
                table: "Channel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Direction",
                table: "Channel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<double>(
                name: "Alarm1SetPoint",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Alarm2SetPoint",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Alarm3SetPoint",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Offset",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Resistance",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SensorTypeId",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Slope",
                table: "Channel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Channel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Channel",
                table: "Channel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_SensorTypeId",
                table: "Channel",
                column: "SensorTypeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Channel_SensorTypeId",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Alarm1SetPoint",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Alarm2SetPoint",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Alarm3SetPoint",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Offset",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Resistance",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "SensorTypeId",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Slope",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Channel");

            migrationBuilder.RenameTable(
                name: "Channel",
                newName: "DigitalChannels");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_GenericMonitorBoxId",
                table: "DigitalChannels",
                newName: "IX_DigitalChannels_GenericMonitorBoxId");

            migrationBuilder.AlterColumn<int>(
                name: "Logic",
                table: "DigitalChannels",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Direction",
                table: "DigitalChannels",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DigitalChannels",
                table: "DigitalChannels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AnalogChannels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Alarm1SetPoint = table.Column<double>(nullable: false),
                    Alarm2SetPoint = table.Column<double>(nullable: false),
                    Alarm3SetPoint = table.Column<double>(nullable: false),
                    Bypass = table.Column<bool>(nullable: false),
                    ChannelNumber = table.Column<int>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    GenericMonitorBoxId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Offset = table.Column<double>(nullable: false),
                    Resistance = table.Column<double>(nullable: false),
                    SensorTypeId = table.Column<int>(nullable: true),
                    Slope = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalogChannels_ModbusDevices_GenericMonitorBoxId",
                        column: x => x.GenericMonitorBoxId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalogChannels_Categories_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalogChannels_GenericMonitorBoxId",
                table: "AnalogChannels",
                column: "GenericMonitorBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalogChannels_SensorTypeId",
                table: "AnalogChannels",
                column: "SensorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DigitalChannels_ModbusDevices_GenericMonitorBoxId",
                table: "DigitalChannels",
                column: "GenericMonitorBoxId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv1101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenericBoxAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenericBoxReadingId = table.Column<int>(nullable: false),
                    AnalogCh1 = table.Column<int>(nullable: false),
                    AnalogCh2 = table.Column<int>(nullable: false),
                    AnalogCh3 = table.Column<int>(nullable: false),
                    AnalogCh4 = table.Column<int>(nullable: false),
                    AnalogCh5 = table.Column<int>(nullable: false),
                    AnalogCh6 = table.Column<int>(nullable: false),
                    AnalogCh7 = table.Column<int>(nullable: false),
                    AnalogCh8 = table.Column<int>(nullable: false),
                    AnalogCh9 = table.Column<int>(nullable: false),
                    AnalogCh10 = table.Column<int>(nullable: false),
                    AnalogCh11 = table.Column<int>(nullable: false),
                    AnalogCh12 = table.Column<int>(nullable: false),
                    AnalogCh13 = table.Column<int>(nullable: false),
                    AnalogCh14 = table.Column<int>(nullable: false),
                    AnalogCh15 = table.Column<int>(nullable: false),
                    AnalogCh16 = table.Column<int>(nullable: false),
                    DigitalCh1 = table.Column<bool>(nullable: false),
                    DigitalCh2 = table.Column<bool>(nullable: false),
                    DigitalCh3 = table.Column<bool>(nullable: false),
                    DigitalCh4 = table.Column<bool>(nullable: false),
                    DigitalCh5 = table.Column<bool>(nullable: false),
                    DigitalCh6 = table.Column<bool>(nullable: false),
                    DigitalCh7 = table.Column<bool>(nullable: false),
                    DigitalCh8 = table.Column<bool>(nullable: false),
                    DigitalCh9 = table.Column<bool>(nullable: false),
                    DigitalCh10 = table.Column<bool>(nullable: false),
                    DigitalCh11 = table.Column<bool>(nullable: false),
                    DigitalCh12 = table.Column<bool>(nullable: false),
                    DigitalCh13 = table.Column<bool>(nullable: false),
                    DigitalCh14 = table.Column<bool>(nullable: false),
                    DigitalCh15 = table.Column<bool>(nullable: false),
                    DigitalCh16 = table.Column<bool>(nullable: false),
                    DigitalCh17 = table.Column<bool>(nullable: false),
                    DigitalCh18 = table.Column<bool>(nullable: false),
                    DigitalCh19 = table.Column<bool>(nullable: false),
                    DigitalCh20 = table.Column<bool>(nullable: false),
                    DigitalCh21 = table.Column<bool>(nullable: false),
                    DigitalCh22 = table.Column<bool>(nullable: false),
                    DigitalCh23 = table.Column<bool>(nullable: false),
                    DigitalCh24 = table.Column<bool>(nullable: false),
                    DigitalCh25 = table.Column<bool>(nullable: false),
                    DigitalCh26 = table.Column<bool>(nullable: false),
                    DigitalCh27 = table.Column<bool>(nullable: false),
                    DigitalCh28 = table.Column<bool>(nullable: false),
                    DigitalCh29 = table.Column<bool>(nullable: false),
                    DigitalCh30 = table.Column<bool>(nullable: false),
                    DigitalCh31 = table.Column<bool>(nullable: false),
                    DigitalCh32 = table.Column<bool>(nullable: false),
                    DigitalCh33 = table.Column<bool>(nullable: false),
                    DigitalCh34 = table.Column<bool>(nullable: false),
                    DigitalCh35 = table.Column<bool>(nullable: false),
                    DigitalCh36 = table.Column<bool>(nullable: false),
                    DigitalCh37 = table.Column<bool>(nullable: false),
                    DigitalCh38 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericBoxAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericBoxAlerts_GenericBoxReadings_GenericBoxReadingId",
                        column: x => x.GenericBoxReadingId,
                        principalTable: "GenericBoxReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenericBoxAlerts_GenericBoxReadingId",
                table: "GenericBoxAlerts",
                column: "GenericBoxReadingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenericBoxAlerts");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneratorSystemError_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemError");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneratorSystemWarning_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemWarning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneratorSystemWarning",
                table: "GeneratorSystemWarning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneratorSystemError",
                table: "GeneratorSystemError");

            migrationBuilder.RenameTable(
                name: "GeneratorSystemWarning",
                newName: "GeneratorSystemWarnings");

            migrationBuilder.RenameTable(
                name: "GeneratorSystemError",
                newName: "GeneratorSystemErrors");

            migrationBuilder.RenameIndex(
                name: "IX_GeneratorSystemWarning_H2GenReadingId",
                table: "GeneratorSystemWarnings",
                newName: "IX_GeneratorSystemWarnings_H2GenReadingId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneratorSystemError_H2GenReadingId",
                table: "GeneratorSystemErrors",
                newName: "IX_GeneratorSystemErrors_H2GenReadingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneratorSystemWarnings",
                table: "GeneratorSystemWarnings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneratorSystemErrors",
                table: "GeneratorSystemErrors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratorSystemErrors_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemErrors",
                column: "H2GenReadingId",
                principalTable: "H2GenReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratorSystemWarnings_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemWarnings",
                column: "H2GenReadingId",
                principalTable: "H2GenReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneratorSystemErrors_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemErrors");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneratorSystemWarnings_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemWarnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneratorSystemWarnings",
                table: "GeneratorSystemWarnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneratorSystemErrors",
                table: "GeneratorSystemErrors");

            migrationBuilder.RenameTable(
                name: "GeneratorSystemWarnings",
                newName: "GeneratorSystemWarning");

            migrationBuilder.RenameTable(
                name: "GeneratorSystemErrors",
                newName: "GeneratorSystemError");

            migrationBuilder.RenameIndex(
                name: "IX_GeneratorSystemWarnings_H2GenReadingId",
                table: "GeneratorSystemWarning",
                newName: "IX_GeneratorSystemWarning_H2GenReadingId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneratorSystemErrors_H2GenReadingId",
                table: "GeneratorSystemError",
                newName: "IX_GeneratorSystemError_H2GenReadingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneratorSystemWarning",
                table: "GeneratorSystemWarning",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneratorSystemError",
                table: "GeneratorSystemError",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratorSystemError_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemError",
                column: "H2GenReadingId",
                principalTable: "H2GenReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratorSystemWarning_H2GenReadings_H2GenReadingId",
                table: "GeneratorSystemWarning",
                column: "H2GenReadingId",
                principalTable: "H2GenReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

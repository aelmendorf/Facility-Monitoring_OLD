using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildv103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A200_Drain_Valve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200_Inlet_Valve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200_Level_Empty",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200_Level_Flooded",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200_Level_High",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200_Level_Low",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300_Drain_Valve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300_Level_Empty",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300_Level_Flooded",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300_Level_High",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300_Level_Low",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "All_System_Errors",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "All_System_Warnings",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Dryer_Valve1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Dryer_Valve2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Dryer_Valve3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Dryer_Valve4",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Operation_Mode",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSV_A1_A2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_A1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_A2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_A3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_B1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_B2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_B3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_C1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_C2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PS_Fault_C3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_A1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_A2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_A3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_B1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_B2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_B3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_C1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_C2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Ps_card_enable_C3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Pump_control",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_A_Water_Flow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_A_monitor_Current",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_A_valve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_B_Water_Flow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_B_monitor_Current",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_B_valve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_C_Water_Flow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Stack_C_valve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "Vent_Valve",
                table: "H2GenReadings");

            migrationBuilder.RenameColumn(
                name: "ps_unit_status",
                table: "H2GenReadings",
                newName: "StackCMonitorCurrent");

            migrationBuilder.RenameColumn(
                name: "Water_Temperature",
                table: "H2GenReadings",
                newName: "WaterTemperature");

            migrationBuilder.RenameColumn(
                name: "Water_Quality",
                table: "H2GenReadings",
                newName: "WaterQuality");

            migrationBuilder.RenameColumn(
                name: "Thermal_Control_Valve",
                table: "H2GenReadings",
                newName: "ThermalControlValve");

            migrationBuilder.RenameColumn(
                name: "System_mode",
                table: "H2GenReadings",
                newName: "StackBMonitorCurrent");

            migrationBuilder.RenameColumn(
                name: "System_State",
                table: "H2GenReadings",
                newName: "StackAMonitorCurrent");

            migrationBuilder.RenameColumn(
                name: "System_Pressure",
                table: "H2GenReadings",
                newName: "SystemPressure");

            migrationBuilder.RenameColumn(
                name: "System_5V_power",
                table: "H2GenReadings",
                newName: "System5VPower");

            migrationBuilder.RenameColumn(
                name: "System_3V_power",
                table: "H2GenReadings",
                newName: "System3VPower");

            migrationBuilder.RenameColumn(
                name: "System_24V_power",
                table: "H2GenReadings",
                newName: "System24VPower");

            migrationBuilder.RenameColumn(
                name: "Stack_C_monitor_Current",
                table: "H2GenReadings",
                newName: "PSV_A1A2");

            migrationBuilder.RenameColumn(
                name: "Product_pressure",
                table: "H2GenReadings",
                newName: "ProductPressure");

            migrationBuilder.RenameColumn(
                name: "Hydrogen_flow",
                table: "H2GenReadings",
                newName: "HydrogenFlow");

            migrationBuilder.RenameColumn(
                name: "Heat_sink_Temperature",
                table: "H2GenReadings",
                newName: "HeatSinkTemperature");

            migrationBuilder.RenameColumn(
                name: "Heat_Exchanger_Water_Temp",
                table: "H2GenReadings",
                newName: "HeatExchangerWaterTemp");

            migrationBuilder.RenameColumn(
                name: "Ext_Water_Quality",
                table: "H2GenReadings",
                newName: "ExtWaterQuality");

            migrationBuilder.RenameColumn(
                name: "DI_water_quality",
                table: "H2GenReadings",
                newName: "DIWaterQuality");

            migrationBuilder.RenameColumn(
                name: "CG220_level",
                table: "H2GenReadings",
                newName: "CG220Level");

            migrationBuilder.RenameColumn(
                name: "Ambient_Temperature",
                table: "H2GenReadings",
                newName: "AmbientTemperature");

            migrationBuilder.AddColumn<int>(
                name: "AmmoniaControllerId",
                table: "Registers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "H2GeneratorId",
                table: "Registers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OperationMode",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemState",
                table: "ModbusDevices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "A200DrainValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A200InletValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A200LevelEmpty",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A200LevelFlooded",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A200LevelHigh",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A200LevelLow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A300DrainValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A300LevelEmpty",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A300LevelFlooded",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A300LevelHigh",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A300LevelLow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DryerValve1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DryerValve2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DryerValve3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DryerValve4",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperationMode",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFaultA1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFaultA2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFaultB1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFaultB2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFaultC1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFaultC2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFault_A3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFault_B3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSFault_C3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSUnitStatus",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnableA1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnableA2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnableB1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnableB2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnableC1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnableC2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnable_A3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnable_B3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PsCardEnable_C3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PumpControl",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StackAValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StackAWaterFlow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StackBValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StackBWaterFlow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StackCValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StackCWaterFlow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemMode",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemState",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VentValve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GeneratorSystemError",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    H2GenReadingId = table.Column<int>(nullable: false),
                    E01_A1 = table.Column<int>(nullable: false),
                    E01_A2 = table.Column<int>(nullable: false),
                    E01_A3 = table.Column<int>(nullable: false),
                    E01_B1 = table.Column<int>(nullable: false),
                    E01_B2 = table.Column<int>(nullable: false),
                    E01_B3 = table.Column<int>(nullable: false),
                    E01_C1 = table.Column<int>(nullable: false),
                    E01_C2 = table.Column<int>(nullable: false),
                    E01_C3 = table.Column<int>(nullable: false),
                    E02_A1 = table.Column<int>(nullable: false),
                    E02_A2 = table.Column<int>(nullable: false),
                    E02_A3 = table.Column<int>(nullable: false),
                    E02_B1 = table.Column<int>(nullable: false),
                    E02_B2 = table.Column<int>(nullable: false),
                    E02_B3 = table.Column<int>(nullable: false),
                    E02_C1 = table.Column<int>(nullable: false),
                    E02_C2 = table.Column<int>(nullable: false),
                    E02_C3 = table.Column<int>(nullable: false),
                    E03_A = table.Column<int>(nullable: false),
                    E03_B = table.Column<int>(nullable: false),
                    E03_C = table.Column<int>(nullable: false),
                    E04_A = table.Column<int>(nullable: false),
                    E04_B = table.Column<int>(nullable: false),
                    E04_C = table.Column<int>(nullable: false),
                    E05_A1 = table.Column<int>(nullable: false),
                    E05_A2 = table.Column<int>(nullable: false),
                    E05_A3 = table.Column<int>(nullable: false),
                    E05_B1 = table.Column<int>(nullable: false),
                    E05_B2 = table.Column<int>(nullable: false),
                    E05_B3 = table.Column<int>(nullable: false),
                    E05_C1 = table.Column<int>(nullable: false),
                    E05_C2 = table.Column<int>(nullable: false),
                    E05_C3 = table.Column<int>(nullable: false),
                    E06 = table.Column<int>(nullable: false),
                    E07 = table.Column<int>(nullable: false),
                    E08 = table.Column<int>(nullable: false),
                    E09 = table.Column<int>(nullable: false),
                    E10 = table.Column<int>(nullable: false),
                    E11 = table.Column<int>(nullable: false),
                    E12 = table.Column<int>(nullable: false),
                    E13 = table.Column<int>(nullable: false),
                    E14 = table.Column<int>(nullable: false),
                    E15 = table.Column<int>(nullable: false),
                    E16_A = table.Column<int>(nullable: false),
                    E16_B = table.Column<int>(nullable: false),
                    E17 = table.Column<int>(nullable: false),
                    E18 = table.Column<int>(nullable: false),
                    E19 = table.Column<int>(nullable: false),
                    E20_A = table.Column<int>(nullable: false),
                    E20_B = table.Column<int>(nullable: false),
                    E21 = table.Column<int>(nullable: false),
                    E22 = table.Column<int>(nullable: false),
                    E23 = table.Column<int>(nullable: false),
                    E24 = table.Column<int>(nullable: false),
                    E25 = table.Column<int>(nullable: false),
                    E26 = table.Column<int>(nullable: false),
                    E27 = table.Column<int>(nullable: false),
                    E28 = table.Column<int>(nullable: false),
                    E29 = table.Column<int>(nullable: false),
                    E30 = table.Column<int>(nullable: false),
                    E31 = table.Column<int>(nullable: false),
                    E32 = table.Column<int>(nullable: false),
                    E33 = table.Column<int>(nullable: false),
                    E34 = table.Column<int>(nullable: false),
                    E35 = table.Column<int>(nullable: false),
                    E36_A = table.Column<int>(nullable: false),
                    E36_B = table.Column<int>(nullable: false),
                    E36_C = table.Column<int>(nullable: false),
                    E37 = table.Column<int>(nullable: false),
                    E38 = table.Column<int>(nullable: false),
                    E39 = table.Column<int>(nullable: false),
                    E40 = table.Column<int>(nullable: false),
                    E41 = table.Column<int>(nullable: false),
                    E42 = table.Column<int>(nullable: false),
                    E43 = table.Column<int>(nullable: false),
                    E44 = table.Column<int>(nullable: false),
                    E45 = table.Column<int>(nullable: false),
                    E46 = table.Column<int>(nullable: false),
                    E47 = table.Column<int>(nullable: false),
                    E48 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratorSystemError", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratorSystemError_H2GenReadings_H2GenReadingId",
                        column: x => x.H2GenReadingId,
                        principalTable: "H2GenReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratorSystemWarning",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    H2GenReadingId = table.Column<int>(nullable: false),
                    W01 = table.Column<int>(nullable: false),
                    W02 = table.Column<int>(nullable: false),
                    W03 = table.Column<int>(nullable: false),
                    W04 = table.Column<int>(nullable: false),
                    W05 = table.Column<int>(nullable: false),
                    W06 = table.Column<int>(nullable: false),
                    W07 = table.Column<int>(nullable: false),
                    W08 = table.Column<int>(nullable: false),
                    W09 = table.Column<int>(nullable: false),
                    W10 = table.Column<int>(nullable: false),
                    W11 = table.Column<int>(nullable: false),
                    W12 = table.Column<int>(nullable: false),
                    W13 = table.Column<int>(nullable: false),
                    W14 = table.Column<int>(nullable: false),
                    W15 = table.Column<int>(nullable: false),
                    W16 = table.Column<int>(nullable: false),
                    W17 = table.Column<int>(nullable: false),
                    W18 = table.Column<int>(nullable: false),
                    W19 = table.Column<int>(nullable: false),
                    W20 = table.Column<int>(nullable: false),
                    W21 = table.Column<int>(nullable: false),
                    W22 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratorSystemWarning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratorSystemWarning_H2GenReadings_H2GenReadingId",
                        column: x => x.H2GenReadingId,
                        principalTable: "H2GenReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registers_AmmoniaControllerId",
                table: "Registers",
                column: "AmmoniaControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_H2GeneratorId",
                table: "Registers",
                column: "H2GeneratorId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorSystemError_H2GenReadingId",
                table: "GeneratorSystemError",
                column: "H2GenReadingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorSystemWarning_H2GenReadingId",
                table: "GeneratorSystemWarning",
                column: "H2GenReadingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_AmmoniaControllerId",
                table: "Registers",
                column: "AmmoniaControllerId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_ModbusDevices_H2GeneratorId",
                table: "Registers",
                column: "H2GeneratorId",
                principalTable: "ModbusDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_AmmoniaControllerId",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_ModbusDevices_H2GeneratorId",
                table: "Registers");

            migrationBuilder.DropTable(
                name: "GeneratorSystemError");

            migrationBuilder.DropTable(
                name: "GeneratorSystemWarning");

            migrationBuilder.DropIndex(
                name: "IX_Registers_AmmoniaControllerId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_H2GeneratorId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "AmmoniaControllerId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "H2GeneratorId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "OperationMode",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "SystemState",
                table: "ModbusDevices");

            migrationBuilder.DropColumn(
                name: "A200DrainValve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200InletValve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200LevelEmpty",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200LevelFlooded",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200LevelHigh",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A200LevelLow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300DrainValve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300LevelEmpty",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300LevelFlooded",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300LevelHigh",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "A300LevelLow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "DryerValve1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "DryerValve2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "DryerValve3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "DryerValve4",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "OperationMode",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFaultA1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFaultA2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFaultB1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFaultB2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFaultC1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFaultC2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFault_A3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFault_B3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSFault_C3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PSUnitStatus",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnableA1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnableA2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnableB1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnableB2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnableC1",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnableC2",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnable_A3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnable_B3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PsCardEnable_C3",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "PumpControl",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "StackAValve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "StackAWaterFlow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "StackBValve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "StackBWaterFlow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "StackCValve",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "StackCWaterFlow",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "SystemMode",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "SystemState",
                table: "H2GenReadings");

            migrationBuilder.DropColumn(
                name: "VentValve",
                table: "H2GenReadings");

            migrationBuilder.RenameColumn(
                name: "WaterTemperature",
                table: "H2GenReadings",
                newName: "Water_Temperature");

            migrationBuilder.RenameColumn(
                name: "WaterQuality",
                table: "H2GenReadings",
                newName: "Water_Quality");

            migrationBuilder.RenameColumn(
                name: "ThermalControlValve",
                table: "H2GenReadings",
                newName: "Thermal_Control_Valve");

            migrationBuilder.RenameColumn(
                name: "SystemPressure",
                table: "H2GenReadings",
                newName: "System_Pressure");

            migrationBuilder.RenameColumn(
                name: "System5VPower",
                table: "H2GenReadings",
                newName: "System_5V_power");

            migrationBuilder.RenameColumn(
                name: "System3VPower",
                table: "H2GenReadings",
                newName: "System_3V_power");

            migrationBuilder.RenameColumn(
                name: "System24VPower",
                table: "H2GenReadings",
                newName: "System_24V_power");

            migrationBuilder.RenameColumn(
                name: "StackCMonitorCurrent",
                table: "H2GenReadings",
                newName: "ps_unit_status");

            migrationBuilder.RenameColumn(
                name: "StackBMonitorCurrent",
                table: "H2GenReadings",
                newName: "System_mode");

            migrationBuilder.RenameColumn(
                name: "StackAMonitorCurrent",
                table: "H2GenReadings",
                newName: "System_State");

            migrationBuilder.RenameColumn(
                name: "ProductPressure",
                table: "H2GenReadings",
                newName: "Product_pressure");

            migrationBuilder.RenameColumn(
                name: "PSV_A1A2",
                table: "H2GenReadings",
                newName: "Stack_C_monitor_Current");

            migrationBuilder.RenameColumn(
                name: "HydrogenFlow",
                table: "H2GenReadings",
                newName: "Hydrogen_flow");

            migrationBuilder.RenameColumn(
                name: "HeatSinkTemperature",
                table: "H2GenReadings",
                newName: "Heat_sink_Temperature");

            migrationBuilder.RenameColumn(
                name: "HeatExchangerWaterTemp",
                table: "H2GenReadings",
                newName: "Heat_Exchanger_Water_Temp");

            migrationBuilder.RenameColumn(
                name: "ExtWaterQuality",
                table: "H2GenReadings",
                newName: "Ext_Water_Quality");

            migrationBuilder.RenameColumn(
                name: "DIWaterQuality",
                table: "H2GenReadings",
                newName: "DI_water_quality");

            migrationBuilder.RenameColumn(
                name: "CG220Level",
                table: "H2GenReadings",
                newName: "CG220_level");

            migrationBuilder.RenameColumn(
                name: "AmbientTemperature",
                table: "H2GenReadings",
                newName: "Ambient_Temperature");

            migrationBuilder.AddColumn<bool>(
                name: "A200_Drain_Valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A200_Inlet_Valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A200_Level_Empty",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A200_Level_Flooded",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A200_Level_High",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A200_Level_Low",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A300_Drain_Valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A300_Level_Empty",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A300_Level_Flooded",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A300_Level_High",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A300_Level_Low",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "All_System_Errors",
                table: "H2GenReadings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "All_System_Warnings",
                table: "H2GenReadings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Dryer_Valve1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dryer_Valve2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dryer_Valve3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dryer_Valve4",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Operation_Mode",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSV_A1_A2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_A1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_A2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_A3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_B1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_B2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_B3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_C1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_C2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PS_Fault_C3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_A1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_A2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_A3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_B1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_B2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_B3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_C1",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_C2",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ps_card_enable_C3",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Pump_control",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stack_A_Water_Flow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Stack_A_monitor_Current",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Stack_A_valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stack_B_Water_Flow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Stack_B_monitor_Current",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Stack_B_valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stack_C_Water_Flow",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stack_C_valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vent_Valve",
                table: "H2GenReadings",
                nullable: false,
                defaultValue: false);
        }
    }
}

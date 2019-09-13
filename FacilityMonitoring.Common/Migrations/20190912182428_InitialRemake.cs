using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class InitialRemake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ZeroPoint = table.Column<double>(nullable: true),
                    MaxPoint = table.Column<double>(nullable: true),
                    Units = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModbusDevices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Identifier = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    SlaveAddress = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    BypassAll = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ActiveTank = table.Column<int>(nullable: true),
                    RegisterBaseAddress = table.Column<int>(nullable: true),
                    ReadRegisterLength = table.Column<int>(nullable: true),
                    CoilBaseAddress = table.Column<int>(nullable: true),
                    ReadCoilLength = table.Column<int>(nullable: true),
                    CalInputBaseAddr = table.Column<int>(nullable: true),
                    CalInputLength = table.Column<int>(nullable: true),
                    DataForInputAddr = table.Column<int>(nullable: true),
                    CalModeAddr = table.Column<int>(nullable: true),
                    AnalogChannelCount = table.Column<int>(nullable: true),
                    DigitalInputChannelCount = table.Column<int>(nullable: true),
                    DigitalOutputChannelCount = table.Column<int>(nullable: true),
                    ModbusComAddr = table.Column<int>(nullable: true),
                    SoftwareMaintAddr = table.Column<int>(nullable: true),
                    WarningAddr = table.Column<int>(nullable: true),
                    AlarmAddr = table.Column<int>(nullable: true),
                    StateAddr = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmmoniaBoxReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    AmmoniaControllerId = table.Column<int>(nullable: false),
                    Tank1Weight = table.Column<int>(nullable: false),
                    Tank2Weight = table.Column<int>(nullable: false),
                    Tank3Weight = table.Column<int>(nullable: false),
                    Tank4Weight = table.Column<int>(nullable: false),
                    Tank1ZeroCal = table.Column<int>(nullable: false),
                    Tank1NonZeroCal = table.Column<int>(nullable: false),
                    Tank1Zero = table.Column<int>(nullable: false),
                    Tank1NonZero = table.Column<int>(nullable: false),
                    Tank1Total = table.Column<int>(nullable: false),
                    Tank1Gas = table.Column<int>(nullable: false),
                    Tank2ZeroCal = table.Column<int>(nullable: false),
                    Tank2NonZeroCal = table.Column<int>(nullable: false),
                    Tank2Zero = table.Column<int>(nullable: false),
                    Tank2NonZero = table.Column<int>(nullable: false),
                    Tank2Total = table.Column<int>(nullable: false),
                    Tank2Gas = table.Column<int>(nullable: false),
                    Tank3ZeroCal = table.Column<int>(nullable: false),
                    Tank3NonZeroCal = table.Column<int>(nullable: false),
                    Tank3Zero = table.Column<int>(nullable: false),
                    Tank3NonZero = table.Column<int>(nullable: false),
                    Tank3Total = table.Column<int>(nullable: false),
                    Tank3Gas = table.Column<int>(nullable: false),
                    Tank4ZeroCal = table.Column<int>(nullable: false),
                    Tank4NonZeroCal = table.Column<int>(nullable: false),
                    Tank4Zero = table.Column<int>(nullable: false),
                    Tank4NonZero = table.Column<int>(nullable: false),
                    Tank4Total = table.Column<int>(nullable: false),
                    Tank4Gas = table.Column<int>(nullable: false),
                    Tank1Tare = table.Column<int>(nullable: false),
                    Tank2Tare = table.Column<int>(nullable: false),
                    Tank3Tare = table.Column<int>(nullable: false),
                    Tank4Tare = table.Column<int>(nullable: false),
                    Tank1Temperature = table.Column<double>(nullable: false),
                    Tank2Temperature = table.Column<double>(nullable: false),
                    Tank3Temperature = table.Column<double>(nullable: false),
                    Tank4Temperature = table.Column<double>(nullable: false),
                    Heater1DutyCycle = table.Column<int>(nullable: false),
                    Heater2DutyCycle = table.Column<int>(nullable: false),
                    Heater3DutyCycle = table.Column<int>(nullable: false),
                    Heater4DutyCycle = table.Column<int>(nullable: false),
                    Tank1Warning = table.Column<bool>(nullable: false),
                    Tank2Warning = table.Column<bool>(nullable: false),
                    Tank3Warning = table.Column<bool>(nullable: false),
                    Tank4Warning = table.Column<bool>(nullable: false),
                    Tank1Alarm = table.Column<bool>(nullable: false),
                    Tank2Alarm = table.Column<bool>(nullable: false),
                    Tank3Alarm = table.Column<bool>(nullable: false),
                    Tank4Alarm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmmoniaBoxReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmmoniaBoxReadings_ModbusDevices_AmmoniaControllerId",
                        column: x => x.AmmoniaControllerId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenericBoxReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    GenericMonitorBoxId = table.Column<int>(nullable: false),
                    AnalogCh1 = table.Column<double>(nullable: false),
                    AnalogCh2 = table.Column<double>(nullable: false),
                    AnalogCh3 = table.Column<double>(nullable: false),
                    AnalogCh4 = table.Column<double>(nullable: false),
                    AnalogCh5 = table.Column<double>(nullable: false),
                    AnalogCh6 = table.Column<double>(nullable: false),
                    AnalogCh7 = table.Column<double>(nullable: false),
                    AnalogCh8 = table.Column<double>(nullable: false),
                    AnalogCh9 = table.Column<double>(nullable: false),
                    AnalogCh10 = table.Column<double>(nullable: false),
                    AnalogCh11 = table.Column<double>(nullable: false),
                    AnalogCh12 = table.Column<double>(nullable: false),
                    AnalogCh13 = table.Column<double>(nullable: false),
                    AnalogCh14 = table.Column<double>(nullable: false),
                    AnalogCh15 = table.Column<double>(nullable: false),
                    AnalogCh16 = table.Column<double>(nullable: false),
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
                    DigitalCh38 = table.Column<bool>(nullable: false),
                    OutputCh1 = table.Column<bool>(nullable: false),
                    OutputCh2 = table.Column<bool>(nullable: false),
                    OutputCh3 = table.Column<bool>(nullable: false),
                    OutputCh4 = table.Column<bool>(nullable: false),
                    OutputCh5 = table.Column<bool>(nullable: false),
                    OutputCh6 = table.Column<bool>(nullable: false),
                    OutputCh7 = table.Column<bool>(nullable: false),
                    OutputCh8 = table.Column<bool>(nullable: false),
                    OutputCh9 = table.Column<bool>(nullable: false),
                    OutputCh10 = table.Column<bool>(nullable: false),
                    Alarm1 = table.Column<bool>(nullable: false),
                    Alarm2 = table.Column<bool>(nullable: false),
                    Alarm3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericBoxReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericBoxReadings_ModbusDevices_GenericMonitorBoxId",
                        column: x => x.GenericMonitorBoxId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "H2GenReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    GeneratorId = table.Column<int>(nullable: false),
                    A200_Level_Flooded = table.Column<bool>(nullable: false),
                    A200_Level_High = table.Column<bool>(nullable: false),
                    A200_Level_Low = table.Column<bool>(nullable: false),
                    A300_Level_Flooded = table.Column<bool>(nullable: false),
                    A300_Level_High = table.Column<bool>(nullable: false),
                    A300_Level_Low = table.Column<bool>(nullable: false),
                    A200_Level_Empty = table.Column<bool>(nullable: false),
                    A300_Level_Empty = table.Column<bool>(nullable: false),
                    Stack_A_Water_Flow = table.Column<bool>(nullable: false),
                    Stack_B_Water_Flow = table.Column<bool>(nullable: false),
                    Stack_C_Water_Flow = table.Column<bool>(nullable: false),
                    Vent_Valve = table.Column<bool>(nullable: false),
                    A200_Drain_Valve = table.Column<bool>(nullable: false),
                    A200_Inlet_Valve = table.Column<bool>(nullable: false),
                    Dryer_Valve1 = table.Column<bool>(nullable: false),
                    Dryer_Valve2 = table.Column<bool>(nullable: false),
                    Dryer_Valve3 = table.Column<bool>(nullable: false),
                    Dryer_Valve4 = table.Column<bool>(nullable: false),
                    A300_Drain_Valve = table.Column<bool>(nullable: false),
                    Stack_A_valve = table.Column<bool>(nullable: false),
                    Stack_B_valve = table.Column<bool>(nullable: false),
                    Stack_C_valve = table.Column<bool>(nullable: false),
                    Pump_control = table.Column<bool>(nullable: false),
                    PSV_A1 = table.Column<int>(nullable: false),
                    PSV_A1_A2 = table.Column<int>(nullable: false),
                    PSV_B1 = table.Column<int>(nullable: false),
                    PSV_B2 = table.Column<int>(nullable: false),
                    PSV_C1 = table.Column<int>(nullable: false),
                    PSV_C2 = table.Column<int>(nullable: false),
                    Stack_A_monitor_Current = table.Column<int>(nullable: false),
                    Stack_B_monitor_Current = table.Column<int>(nullable: false),
                    Stack_C_monitor_Current = table.Column<int>(nullable: false),
                    PS_Fault_A1 = table.Column<int>(nullable: false),
                    PS_Fault_A2 = table.Column<int>(nullable: false),
                    PS_Fault_B1 = table.Column<int>(nullable: false),
                    PS_Fault_B2 = table.Column<int>(nullable: false),
                    PS_Fault_C1 = table.Column<int>(nullable: false),
                    PS_Fault_C2 = table.Column<int>(nullable: false),
                    ps_unit_status = table.Column<int>(nullable: false),
                    Ps_card_enable_A1 = table.Column<int>(nullable: false),
                    Ps_card_enable_A2 = table.Column<int>(nullable: false),
                    Ps_card_enable_B1 = table.Column<int>(nullable: false),
                    Ps_card_enable_B2 = table.Column<int>(nullable: false),
                    Ps_card_enable_C1 = table.Column<int>(nullable: false),
                    Ps_card_enable_C2 = table.Column<int>(nullable: false),
                    PSV_A3 = table.Column<int>(nullable: false),
                    PSV_B3 = table.Column<int>(nullable: false),
                    PSV_C3 = table.Column<int>(nullable: false),
                    PS_Fault_A3 = table.Column<int>(nullable: false),
                    PS_Fault_B3 = table.Column<int>(nullable: false),
                    PS_Fault_C3 = table.Column<int>(nullable: false),
                    Ps_card_enable_A3 = table.Column<int>(nullable: false),
                    Ps_card_enable_B3 = table.Column<int>(nullable: false),
                    Ps_card_enable_C3 = table.Column<int>(nullable: false),
                    System_mode = table.Column<int>(nullable: false),
                    Operation_Mode = table.Column<int>(nullable: false),
                    System_State = table.Column<int>(nullable: false),
                    Water_Quality = table.Column<double>(nullable: false),
                    Water_Temperature = table.Column<double>(nullable: false),
                    System_Pressure = table.Column<double>(nullable: false),
                    Product_pressure = table.Column<double>(nullable: false),
                    CG220_level = table.Column<double>(nullable: false),
                    Heat_Exchanger_Water_Temp = table.Column<double>(nullable: false),
                    System_24V_power = table.Column<double>(nullable: false),
                    System_5V_power = table.Column<double>(nullable: false),
                    System_3V_power = table.Column<double>(nullable: false),
                    Heat_sink_Temperature = table.Column<double>(nullable: false),
                    Ambient_Temperature = table.Column<double>(nullable: false),
                    DI_water_quality = table.Column<double>(nullable: false),
                    Hydrogen_flow = table.Column<double>(nullable: false),
                    Ext_Water_Quality = table.Column<double>(nullable: false),
                    All_System_Warnings = table.Column<string>(nullable: true),
                    All_System_Errors = table.Column<string>(nullable: true),
                    Thermal_Control_Valve = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_H2GenReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_H2GenReadings_ModbusDevices_GeneratorId",
                        column: x => x.GeneratorId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RegisterIndex = table.Column<int>(nullable: false),
                    RegisterLength = table.Column<int>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    Bypass = table.Column<bool>(nullable: false),
                    PropertyMap = table.Column<string>(nullable: true),
                    Logic = table.Column<int>(nullable: false),
                    GenericMonitorBoxId = table.Column<int>(nullable: false),
                    SensorTypeId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Slope = table.Column<double>(nullable: true),
                    Offset = table.Column<double>(nullable: true),
                    Resistance = table.Column<double>(nullable: true),
                    ZeroValue = table.Column<double>(nullable: true),
                    MaxValue = table.Column<double>(nullable: true),
                    Alarm1SetPoint = table.Column<double>(nullable: true),
                    Alarm1Action = table.Column<int>(nullable: true),
                    Alarm1Enabled = table.Column<bool>(nullable: true),
                    Alarm2SetPoint = table.Column<double>(nullable: true),
                    Alarm2Action = table.Column<int>(nullable: true),
                    Alarm2Enabled = table.Column<bool>(nullable: true),
                    Alarm3SetPoint = table.Column<double>(nullable: true),
                    Alarm3Action = table.Column<int>(nullable: true),
                    Alarm3Enabled = table.Column<bool>(nullable: true),
                    ValueDivisor = table.Column<double>(nullable: true),
                    AlarmAction = table.Column<int>(nullable: true),
                    OutputControl = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registers_Categories_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registers_Categories_SensorTypeId1",
                        column: x => x.SensorTypeId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registers_Categories_SensorTypeId2",
                        column: x => x.SensorTypeId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registers_ModbusDevices_GenericMonitorBoxId",
                        column: x => x.GenericMonitorBoxId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmmoniaBoxReadings_AmmoniaControllerId",
                table: "AmmoniaBoxReadings",
                column: "AmmoniaControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericBoxReadings_GenericMonitorBoxId",
                table: "GenericBoxReadings",
                column: "GenericMonitorBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_H2GenReadings_GeneratorId",
                table: "H2GenReadings",
                column: "GeneratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId1",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId2",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_GenericMonitorBoxId",
                table: "Registers",
                column: "GenericMonitorBoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmmoniaBoxReadings");

            migrationBuilder.DropTable(
                name: "GenericBoxReadings");

            migrationBuilder.DropTable(
                name: "H2GenReadings");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ModbusDevices");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class Buildef3new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    SlaveAddress = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    BypassAll = table.Column<bool>(nullable: false),
                    ReadInterval = table.Column<double>(nullable: false),
                    SaveInterval = table.Column<double>(nullable: false),
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
                    StateAddr = table.Column<int>(nullable: true),
                    OperationMode = table.Column<int>(nullable: true),
                    SystemState = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmmoniaControllerReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_AmmoniaControllerReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmmoniaControllerReadings_ModbusDevices_AmmoniaControllerId",
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
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    GeneratorId = table.Column<int>(nullable: false),
                    A200LevelFlooded = table.Column<int>(nullable: false),
                    A200LevelHigh = table.Column<int>(nullable: false),
                    A200LevelLow = table.Column<int>(nullable: false),
                    A300LevelFlooded = table.Column<int>(nullable: false),
                    A300LevelHigh = table.Column<int>(nullable: false),
                    A300LevelLow = table.Column<int>(nullable: false),
                    A200LevelEmpty = table.Column<int>(nullable: false),
                    A300LevelEmpty = table.Column<int>(nullable: false),
                    StackAWaterFlow = table.Column<int>(nullable: false),
                    StackBWaterFlow = table.Column<int>(nullable: false),
                    StackCWaterFlow = table.Column<int>(nullable: false),
                    VentValve = table.Column<int>(nullable: false),
                    A200DrainValve = table.Column<int>(nullable: false),
                    A200InletValve = table.Column<int>(nullable: false),
                    DryerValve1 = table.Column<int>(nullable: false),
                    DryerValve2 = table.Column<int>(nullable: false),
                    DryerValve3 = table.Column<int>(nullable: false),
                    DryerValve4 = table.Column<int>(nullable: false),
                    A300DrainValve = table.Column<int>(nullable: false),
                    StackAValve = table.Column<int>(nullable: false),
                    StackBValve = table.Column<int>(nullable: false),
                    StackCValve = table.Column<int>(nullable: false),
                    PumpControl = table.Column<int>(nullable: false),
                    PSV_A1 = table.Column<int>(nullable: false),
                    PSV_A2 = table.Column<int>(nullable: false),
                    PSV_B1 = table.Column<int>(nullable: false),
                    PSV_B2 = table.Column<int>(nullable: false),
                    PSV_C1 = table.Column<int>(nullable: false),
                    PSV_C2 = table.Column<int>(nullable: false),
                    StackAMonitorCurrent = table.Column<int>(nullable: false),
                    StackBMonitorCurrent = table.Column<int>(nullable: false),
                    StackCMonitorCurrent = table.Column<int>(nullable: false),
                    PSFaultA1 = table.Column<int>(nullable: false),
                    PSFaultA2 = table.Column<int>(nullable: false),
                    PSFaultB1 = table.Column<int>(nullable: false),
                    PSFaultB2 = table.Column<int>(nullable: false),
                    PSFaultC1 = table.Column<int>(nullable: false),
                    PSFaultC2 = table.Column<int>(nullable: false),
                    PSUnitStatus = table.Column<int>(nullable: false),
                    PsCardEnableA1 = table.Column<int>(nullable: false),
                    PsCardEnableA2 = table.Column<int>(nullable: false),
                    PsCardEnableB1 = table.Column<int>(nullable: false),
                    PsCardEnableB2 = table.Column<int>(nullable: false),
                    PsCardEnableC1 = table.Column<int>(nullable: false),
                    PsCardEnableC2 = table.Column<int>(nullable: false),
                    PSV_A3 = table.Column<int>(nullable: false),
                    PSV_B3 = table.Column<int>(nullable: false),
                    PSV_C3 = table.Column<int>(nullable: false),
                    PSFault_A3 = table.Column<int>(nullable: false),
                    PSFault_B3 = table.Column<int>(nullable: false),
                    PSFault_C3 = table.Column<int>(nullable: false),
                    PsCardEnable_A3 = table.Column<int>(nullable: false),
                    PsCardEnable_B3 = table.Column<int>(nullable: false),
                    PsCardEnable_C3 = table.Column<int>(nullable: false),
                    SystemMode = table.Column<int>(nullable: false),
                    OperationMode = table.Column<int>(nullable: false),
                    SystemState = table.Column<int>(nullable: false),
                    WaterQuality = table.Column<double>(nullable: false),
                    WaterTemperature = table.Column<double>(nullable: false),
                    SystemPressure = table.Column<double>(nullable: false),
                    ProductPressure = table.Column<double>(nullable: false),
                    CG220Level = table.Column<double>(nullable: false),
                    HeatExchangerWaterTemp = table.Column<double>(nullable: false),
                    System24VPower = table.Column<double>(nullable: false),
                    System5VPower = table.Column<double>(nullable: false),
                    System3VPower = table.Column<double>(nullable: false),
                    HeatSinkTemperature = table.Column<double>(nullable: false),
                    AmbientTemperature = table.Column<double>(nullable: false),
                    DIWaterQuality = table.Column<double>(nullable: false),
                    HydrogenFlow = table.Column<double>(nullable: false),
                    ExtWaterQuality = table.Column<double>(nullable: false),
                    ThermalControlValve = table.Column<double>(nullable: false)
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RegisterIndex = table.Column<int>(nullable: false),
                    RegisterLength = table.Column<int>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    Bypass = table.Column<bool>(nullable: false),
                    PropertyMap = table.Column<string>(nullable: true),
                    Logic = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false),
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
                    OutputControl = table.Column<int>(nullable: true),
                    FunctionCode = table.Column<int>(nullable: true),
                    DataType = table.Column<int>(nullable: true)
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
                        name: "FK_Registers_Categories_SensorTypeId3",
                        column: x => x.SensorTypeId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registers_ModbusDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenericBoxAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateTable(
                name: "GeneratorSystemErrors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_GeneratorSystemErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratorSystemErrors_H2GenReadings_H2GenReadingId",
                        column: x => x.H2GenReadingId,
                        principalTable: "H2GenReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratorSystemWarnings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_GeneratorSystemWarnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratorSystemWarnings_H2GenReadings_H2GenReadingId",
                        column: x => x.H2GenReadingId,
                        principalTable: "H2GenReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmmoniaControllerReadings_AmmoniaControllerId",
                table: "AmmoniaControllerReadings",
                column: "AmmoniaControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorSystemErrors_H2GenReadingId",
                table: "GeneratorSystemErrors",
                column: "H2GenReadingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorSystemWarnings_H2GenReadingId",
                table: "GeneratorSystemWarnings",
                column: "H2GenReadingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenericBoxAlerts_GenericBoxReadingId",
                table: "GenericBoxAlerts",
                column: "GenericBoxReadingId",
                unique: true);

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
                name: "IX_Registers_SensorTypeId3",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_DeviceId",
                table: "Registers",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmmoniaControllerReadings");

            migrationBuilder.DropTable(
                name: "GeneratorSystemErrors");

            migrationBuilder.DropTable(
                name: "GeneratorSystemWarnings");

            migrationBuilder.DropTable(
                name: "GenericBoxAlerts");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "H2GenReadings");

            migrationBuilder.DropTable(
                name: "GenericBoxReadings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ModbusDevices");
        }
    }
}

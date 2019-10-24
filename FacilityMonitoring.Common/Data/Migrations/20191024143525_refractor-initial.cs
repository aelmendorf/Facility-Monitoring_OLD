using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class refractorinitial : Migration
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
                    OperationMode = table.Column<int>(nullable: true),
                    SystemState = table.Column<int>(nullable: true),
                    AnalogChannelCount = table.Column<int>(nullable: true),
                    DigitalInputChannelCount = table.Column<int>(nullable: true),
                    DigitalOutputChannelCount = table.Column<int>(nullable: true),
                    ModbusComAddr = table.Column<int>(nullable: true),
                    SoftwareMaintAddr = table.Column<int>(nullable: true),
                    WarningAddr = table.Column<int>(nullable: true),
                    AlarmAddr = table.Column<int>(nullable: true),
                    StateAddr = table.Column<int>(nullable: true),
                    ActiveTank = table.Column<int>(nullable: true),
                    WarningSetPoint = table.Column<int>(nullable: true),
                    AlarmSetPoint = table.Column<int>(nullable: true),
                    Tank1AlertEnabled = table.Column<bool>(nullable: true),
                    Tank2AlertEnabled = table.Column<bool>(nullable: true),
                    Tank3AlertEnabled = table.Column<bool>(nullable: true),
                    Tank4AlertEnabled = table.Column<bool>(nullable: true),
                    RegisterBaseAddress = table.Column<int>(nullable: true),
                    ReadRegisterLength = table.Column<int>(nullable: true),
                    CoilBaseAddress = table.Column<int>(nullable: true),
                    ReadCoilLength = table.Column<int>(nullable: true),
                    CalInputBaseAddr = table.Column<int>(nullable: true),
                    CalInputLength = table.Column<int>(nullable: true),
                    DataForInputAddr = table.Column<int>(nullable: true),
                    CalModeAddr = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusDevices", x => x.Id);
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
                name: "MonitorBoxReadings",
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
                    table.PrimaryKey("PK_MonitorBoxReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitorBoxReadings_ModbusDevices_GenericMonitorBoxId",
                        column: x => x.GenericMonitorBoxId,
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
                    Display = table.Column<bool>(nullable: false),
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
                        name: "FK_Registers_ModbusDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registers_Categories_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TankScaleReadings",
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
                    table.PrimaryKey("PK_TankScaleReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TankScaleReadings_ModbusDevices_AmmoniaControllerId",
                        column: x => x.AmmoniaControllerId,
                        principalTable: "ModbusDevices",
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
                    PS_CARD_A1_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_A2_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_A3_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_B1_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_B2_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_B3_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_C1_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_C2_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_C3_V_LOW = table.Column<int>(nullable: false),
                    PS_CARD_A1_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_A2_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_A3_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_B1_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_B2_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_B3_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_C1_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_C2_V_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_C3_V_HIGH = table.Column<int>(nullable: false),
                    STACK_A_CUR_LOW = table.Column<int>(nullable: false),
                    STACK_B_CUR_LOW = table.Column<int>(nullable: false),
                    STACK_C_CUR_LOW = table.Column<int>(nullable: false),
                    STACK_A_CUR_HIGH = table.Column<int>(nullable: false),
                    STACK_B_CUR_HIGH = table.Column<int>(nullable: false),
                    STACK_C_CUR_HIGH = table.Column<int>(nullable: false),
                    PS_CARD_A1_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_A2_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_A3_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_B1_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_B2_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_B3_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_C1_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_C2_FAULT = table.Column<int>(nullable: false),
                    PS_CARD_C3_FAULT = table.Column<int>(nullable: false),
                    PSU_ERROR = table.Column<int>(nullable: false),
                    PSU_COMM_ERROR = table.Column<int>(nullable: false),
                    RESERVED_E08 = table.Column<int>(nullable: false),
                    SYS_PRESSURE_HIGH_ERR = table.Column<int>(nullable: false),
                    SYS_PRESSURE_LOW_ERR = table.Column<int>(nullable: false),
                    PRESS_TIMEOUT_ERR = table.Column<int>(nullable: false),
                    SYSTEM_TEMP_SHUTDOWN_ERR = table.Column<int>(nullable: false),
                    WATER_FROZEN_ERR = table.Column<int>(nullable: false),
                    A200_EMPTY_ERR = table.Column<int>(nullable: false),
                    A200_FLOODED_ERR = table.Column<int>(nullable: false),
                    RS209_SYS_WATER_QUALITY = table.Column<int>(nullable: false),
                    RESERVED_E16_B = table.Column<int>(nullable: false),
                    PRE_START_TIMEOUT_A200 = table.Column<int>(nullable: false),
                    EMPTY_A300 = table.Column<int>(nullable: false),
                    FLOODED_A300 = table.Column<int>(nullable: false),
                    RS209_OUT_OF_RANGE = table.Column<int>(nullable: false),
                    RESERVED_E20_B = table.Column<int>(nullable: false),
                    RESERVED_E21 = table.Column<int>(nullable: false),
                    RESERVED_E22 = table.Column<int>(nullable: false),
                    RESERVED_E23 = table.Column<int>(nullable: false),
                    HIGH_CONCENTRATION_CG220 = table.Column<int>(nullable: false),
                    RESERVED_E25 = table.Column<int>(nullable: false),
                    OUT_OF_RANGE_CG220 = table.Column<int>(nullable: false),
                    RESERVED_E27 = table.Column<int>(nullable: false),
                    INVALID_STATE_FSW250_FLOW_SWITCHES = table.Column<int>(nullable: false),
                    CHECKSUM_ERROR_CONTROLLER = table.Column<int>(nullable: false),
                    OUT_OF_RANGE_24V_5V_3V_SUPPLY = table.Column<int>(nullable: false),
                    FAULT_DIGITAL_FUSES = table.Column<int>(nullable: false),
                    RESERVED_E32 = table.Column<int>(nullable: false),
                    INVALID_STATE_A200_SWITCH = table.Column<int>(nullable: false),
                    INVALID_STATE_S300_SWITCH = table.Column<int>(nullable: false),
                    RESERVED_E35 = table.Column<int>(nullable: false),
                    LOW_WATER_FLOW_STACK_A = table.Column<int>(nullable: false),
                    LOW_WATER_FLOW_STACK_B = table.Column<int>(nullable: false),
                    LOW_WATER_FLOW_STACK_C = table.Column<int>(nullable: false),
                    PREVIOUS_ERROR_RESET = table.Column<int>(nullable: false),
                    NO_STACKS_PRESENT = table.Column<int>(nullable: false),
                    RESERVED_E39 = table.Column<int>(nullable: false),
                    CAL_DUE_CG220 = table.Column<int>(nullable: false),
                    LOW_TEMP_TE159_PS_ENCLOSURE = table.Column<int>(nullable: false),
                    HIGH_TEMP_TE159_PS_ENCLOSURE = table.Column<int>(nullable: false),
                    RESERVED_E43 = table.Column<int>(nullable: false),
                    RESERVED_E44 = table.Column<int>(nullable: false),
                    RESERVED_E45 = table.Column<int>(nullable: false),
                    OUT_OF_RANGE_TE218_SYS_TEMP = table.Column<int>(nullable: false),
                    OUT_OF_RANGE_PT307_SYS_PRESSURE = table.Column<int>(nullable: false),
                    RESERVED_E48 = table.Column<int>(nullable: false)
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
                    DI_WQ_SENSOR_FAIL_WARN = table.Column<int>(nullable: false),
                    DI_WQ_WARN = table.Column<int>(nullable: false),
                    WATER_QUAL_WARN = table.Column<int>(nullable: false),
                    A200_LOW_FLOW_WARNING = table.Column<int>(nullable: false),
                    BAD_GURAD_WARN = table.Column<int>(nullable: false),
                    COOL_TEMP_LOW_WARN = table.Column<int>(nullable: false),
                    COOL_TEMP_HIGH_WARN = table.Column<int>(nullable: false),
                    SYSTEM_TEMP_WARN = table.Column<int>(nullable: false),
                    CG220_LEVEL_HIGH = table.Column<int>(nullable: false),
                    CG120_LEVEL_HIGH = table.Column<int>(nullable: false),
                    CAL_GAS_PRESSURE_LOW = table.Column<int>(nullable: false),
                    PRODUCT_PRESS_HIGH_WARN = table.Column<int>(nullable: false),
                    HEAT_SINK_TEMP_HIGH = table.Column<int>(nullable: false),
                    COOL_FLOW_LOW_WARN = table.Column<int>(nullable: false),
                    CG120_LOW = table.Column<int>(nullable: false),
                    STACK_WATER_FLOW_WARN = table.Column<int>(nullable: false),
                    SYSTEM_PRESSURE_LOW = table.Column<int>(nullable: false),
                    CABINET_TEMP_HIGH = table.Column<int>(nullable: false),
                    PURGE_PRESSURE_WARN = table.Column<int>(nullable: false),
                    CG_CAL_CANCEL_WARN = table.Column<int>(nullable: false),
                    CG_CAL_DUE_WARN = table.Column<int>(nullable: false),
                    CG_LEVEL_LOW = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MonitorBoxAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonitorBoxReadingId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_MonitorBoxAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitorBoxAlerts_MonitorBoxReadings_MonitorBoxReadingId",
                        column: x => x.MonitorBoxReadingId,
                        principalTable: "MonitorBoxReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TankScaleAlerts",
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
                    table.PrimaryKey("PK_TankScaleAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TankScaleAlerts_TankScaleReadings_AmmoniaControllerReadingId",
                        column: x => x.AmmoniaControllerReadingId,
                        principalTable: "TankScaleReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_H2GenReadings_GeneratorId",
                table: "H2GenReadings",
                column: "GeneratorId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitorBoxAlerts_MonitorBoxReadingId",
                table: "MonitorBoxAlerts",
                column: "MonitorBoxReadingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonitorBoxReadings_GenericMonitorBoxId",
                table: "MonitorBoxReadings",
                column: "GenericMonitorBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_DeviceId",
                table: "Registers",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_SensorTypeId",
                table: "Registers",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TankScaleAlerts_AmmoniaControllerReadingId",
                table: "TankScaleAlerts",
                column: "AmmoniaControllerReadingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TankScaleReadings_AmmoniaControllerId",
                table: "TankScaleReadings",
                column: "AmmoniaControllerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratorSystemErrors");

            migrationBuilder.DropTable(
                name: "GeneratorSystemWarnings");

            migrationBuilder.DropTable(
                name: "MonitorBoxAlerts");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "TankScaleAlerts");

            migrationBuilder.DropTable(
                name: "H2GenReadings");

            migrationBuilder.DropTable(
                name: "MonitorBoxReadings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TankScaleReadings");

            migrationBuilder.DropTable(
                name: "ModbusDevices");
        }
    }
}

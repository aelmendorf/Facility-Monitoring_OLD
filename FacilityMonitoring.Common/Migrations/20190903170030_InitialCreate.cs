﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMonitoring.Common.Migrations
{
    public partial class InitialCreate : Migration
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
                    ZeroValue = table.Column<float>(nullable: true),
                    ZeroCalibration = table.Column<float>(nullable: true),
                    MaxValue = table.Column<float>(nullable: true),
                    MaxCalibration = table.Column<float>(nullable: true)
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
                    Status = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    AnalogChannelCount = table.Column<int>(nullable: true),
                    DigitalInputChannelCount = table.Column<int>(nullable: true),
                    DigitalOutputChannelCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalogChannels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ChannelNumber = table.Column<int>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    Bypass = table.Column<bool>(nullable: false),
                    GenericMonitorBoxId = table.Column<int>(nullable: false),
                    SensorTypeId = table.Column<int>(nullable: true),
                    Slope = table.Column<double>(nullable: false),
                    Offset = table.Column<double>(nullable: false),
                    Resistance = table.Column<double>(nullable: false),
                    Alarm1SetPoint = table.Column<double>(nullable: false),
                    Alarm2SetPoint = table.Column<double>(nullable: false),
                    Alarm3SetPoint = table.Column<double>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "DigitalChannels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ChannelNumber = table.Column<int>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    Bypass = table.Column<bool>(nullable: false),
                    GenericMonitorBoxId = table.Column<int>(nullable: false),
                    Logic = table.Column<int>(nullable: false),
                    Direction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalChannels_ModbusDevices_GenericMonitorBoxId",
                        column: x => x.GenericMonitorBoxId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    ModbusDeviceId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Scale1Status = table.Column<string>(nullable: true),
                    Scale2Status = table.Column<string>(nullable: true),
                    Scale3Status = table.Column<string>(nullable: true),
                    Scale4Status = table.Column<string>(nullable: true),
                    ActiveTank = table.Column<string>(nullable: true),
                    Tank1Weight = table.Column<int>(nullable: true),
                    Tank2Weight = table.Column<int>(nullable: true),
                    Tank3Weight = table.Column<int>(nullable: true),
                    Tank4Weight = table.Column<int>(nullable: true),
                    Tank1Tare = table.Column<int>(nullable: true),
                    Tank2Tare = table.Column<int>(nullable: true),
                    Tank3Tare = table.Column<int>(nullable: true),
                    Tank4Tare = table.Column<int>(nullable: true),
                    Tank1Temperature = table.Column<double>(nullable: true),
                    Tank2Temperature = table.Column<double>(nullable: true),
                    Tank3Temperature = table.Column<double>(nullable: true),
                    Tank4Temperature = table.Column<double>(nullable: true),
                    Heater1DutyCycle = table.Column<int>(nullable: true),
                    Heater2DutyCycle = table.Column<int>(nullable: true),
                    Heater3DutyCycle = table.Column<int>(nullable: true),
                    Heater4DutyCycle = table.Column<int>(nullable: true),
                    Tank1Warning = table.Column<bool>(nullable: true),
                    Tank2Warning = table.Column<bool>(nullable: true),
                    Tank3Warning = table.Column<bool>(nullable: true),
                    Tank4Warning = table.Column<bool>(nullable: true),
                    Tank1Alarm = table.Column<bool>(nullable: true),
                    Tank2Alarm = table.Column<bool>(nullable: true),
                    Tank3Alarm = table.Column<bool>(nullable: true),
                    Tank4Alarm = table.Column<bool>(nullable: true),
                    AnalogCh1 = table.Column<float>(nullable: true),
                    AnalogCh2 = table.Column<float>(nullable: true),
                    AnalogCh3 = table.Column<float>(nullable: true),
                    AnalogCh4 = table.Column<float>(nullable: true),
                    AnalogCh5 = table.Column<float>(nullable: true),
                    AnalogCh6 = table.Column<float>(nullable: true),
                    AnalogCh7 = table.Column<float>(nullable: true),
                    AnalogCh8 = table.Column<float>(nullable: true),
                    AnalogCh9 = table.Column<float>(nullable: true),
                    AnalogCh10 = table.Column<float>(nullable: true),
                    AnalogCh11 = table.Column<float>(nullable: true),
                    AnalogCh12 = table.Column<float>(nullable: true),
                    AnalogCh13 = table.Column<float>(nullable: true),
                    AnalogCh14 = table.Column<float>(nullable: true),
                    AnalogCh15 = table.Column<float>(nullable: true),
                    AnalogCh16 = table.Column<float>(nullable: true),
                    DigitalCh1 = table.Column<bool>(nullable: true),
                    DigitalCh2 = table.Column<bool>(nullable: true),
                    DigitalCh3 = table.Column<bool>(nullable: true),
                    DigitalCh4 = table.Column<bool>(nullable: true),
                    DigitalCh5 = table.Column<bool>(nullable: true),
                    DigitalCh6 = table.Column<bool>(nullable: true),
                    DigitalCh7 = table.Column<bool>(nullable: true),
                    DigitalCh8 = table.Column<bool>(nullable: true),
                    DigitalCh9 = table.Column<bool>(nullable: true),
                    DigitalCh10 = table.Column<bool>(nullable: true),
                    DigitalCh11 = table.Column<bool>(nullable: true),
                    DigitalCh12 = table.Column<bool>(nullable: true),
                    DigitalCh13 = table.Column<bool>(nullable: true),
                    DigitalCh14 = table.Column<bool>(nullable: true),
                    DigitalCh15 = table.Column<bool>(nullable: true),
                    DigitalCh16 = table.Column<bool>(nullable: true),
                    DigitalCh17 = table.Column<bool>(nullable: true),
                    DigitalCh18 = table.Column<bool>(nullable: true),
                    DigitalCh19 = table.Column<bool>(nullable: true),
                    DigitalCh20 = table.Column<bool>(nullable: true),
                    DigitalCh21 = table.Column<bool>(nullable: true),
                    DigitalCh22 = table.Column<bool>(nullable: true),
                    DigitalCh23 = table.Column<bool>(nullable: true),
                    DigitalCh24 = table.Column<bool>(nullable: true),
                    DigitalCh25 = table.Column<bool>(nullable: true),
                    DigitalCh26 = table.Column<bool>(nullable: true),
                    DigitalCh27 = table.Column<bool>(nullable: true),
                    DigitalCh28 = table.Column<bool>(nullable: true),
                    DigitalCh29 = table.Column<bool>(nullable: true),
                    DigitalCh30 = table.Column<bool>(nullable: true),
                    DigitalCh31 = table.Column<bool>(nullable: true),
                    DigitalCh32 = table.Column<bool>(nullable: true),
                    DigitalCh33 = table.Column<bool>(nullable: true),
                    DigitalCh34 = table.Column<bool>(nullable: true),
                    DigitalCh35 = table.Column<bool>(nullable: true),
                    DigitalCh36 = table.Column<bool>(nullable: true),
                    DigitalCh37 = table.Column<bool>(nullable: true),
                    DigitalCh38 = table.Column<bool>(nullable: true),
                    OutputCh1 = table.Column<bool>(nullable: true),
                    OutputCh2 = table.Column<bool>(nullable: true),
                    OutputCh3 = table.Column<bool>(nullable: true),
                    OutputCh4 = table.Column<bool>(nullable: true),
                    OutputCh5 = table.Column<bool>(nullable: true),
                    OutputCh6 = table.Column<bool>(nullable: true),
                    OutputCh7 = table.Column<bool>(nullable: true),
                    OutputCh8 = table.Column<bool>(nullable: true),
                    OutputCh9 = table.Column<bool>(nullable: true),
                    OutputCh10 = table.Column<bool>(nullable: true),
                    A200_Level_Flooded = table.Column<bool>(nullable: true),
                    A200_Level_High = table.Column<bool>(nullable: true),
                    A200_Level_Low = table.Column<bool>(nullable: true),
                    A300_Level_Flooded = table.Column<bool>(nullable: true),
                    A300_Level_High = table.Column<bool>(nullable: true),
                    A300_Level_Low = table.Column<bool>(nullable: true),
                    A200_Level_Empty = table.Column<bool>(nullable: true),
                    A300_Level_Empty = table.Column<bool>(nullable: true),
                    Stack_A_Water_Flow = table.Column<bool>(nullable: true),
                    Stack_B_Water_Flow = table.Column<bool>(nullable: true),
                    Stack_C_Water_Flow = table.Column<bool>(nullable: true),
                    Vent_Valve = table.Column<bool>(nullable: true),
                    A200_Drain_Valve = table.Column<bool>(nullable: true),
                    A200_Inlet_Valve = table.Column<bool>(nullable: true),
                    Dryer_Valve1 = table.Column<bool>(nullable: true),
                    Dryer_Valve2 = table.Column<bool>(nullable: true),
                    Dryer_Valve3 = table.Column<bool>(nullable: true),
                    Dryer_Valve4 = table.Column<bool>(nullable: true),
                    A300_Drain_Valve = table.Column<bool>(nullable: true),
                    Stack_A_valve = table.Column<bool>(nullable: true),
                    Stack_B_valve = table.Column<bool>(nullable: true),
                    Stack_C_valve = table.Column<bool>(nullable: true),
                    Pump_control = table.Column<bool>(nullable: true),
                    PSV_A1 = table.Column<int>(nullable: true),
                    PSV_A1_A2 = table.Column<int>(nullable: true),
                    PSV_B1 = table.Column<int>(nullable: true),
                    PSV_B2 = table.Column<int>(nullable: true),
                    PSV_C1 = table.Column<int>(nullable: true),
                    PSV_C2 = table.Column<int>(nullable: true),
                    Stack_A_monitor_Current = table.Column<int>(nullable: true),
                    Stack_B_monitor_Current = table.Column<int>(nullable: true),
                    Stack_C_monitor_Current = table.Column<int>(nullable: true),
                    PS_Fault_A1 = table.Column<int>(nullable: true),
                    PS_Fault_A2 = table.Column<int>(nullable: true),
                    PS_Fault_B1 = table.Column<int>(nullable: true),
                    PS_Fault_B2 = table.Column<int>(nullable: true),
                    PS_Fault_C1 = table.Column<int>(nullable: true),
                    PS_Fault_C2 = table.Column<int>(nullable: true),
                    ps_unit_status = table.Column<int>(nullable: true),
                    Ps_card_enable_A1 = table.Column<int>(nullable: true),
                    Ps_card_enable_A2 = table.Column<int>(nullable: true),
                    Ps_card_enable_B1 = table.Column<int>(nullable: true),
                    Ps_card_enable_B2 = table.Column<int>(nullable: true),
                    Ps_card_enable_C1 = table.Column<int>(nullable: true),
                    Ps_card_enable_C2 = table.Column<int>(nullable: true),
                    PSV_A3 = table.Column<int>(nullable: true),
                    PSV_B3 = table.Column<int>(nullable: true),
                    PSV_C3 = table.Column<int>(nullable: true),
                    PS_Fault_A3 = table.Column<int>(nullable: true),
                    PS_Fault_B3 = table.Column<int>(nullable: true),
                    PS_Fault_C3 = table.Column<int>(nullable: true),
                    Ps_card_enable_A3 = table.Column<int>(nullable: true),
                    Ps_card_enable_B3 = table.Column<int>(nullable: true),
                    Ps_card_enable_C3 = table.Column<int>(nullable: true),
                    System_mode = table.Column<int>(nullable: true),
                    Operation_Mode = table.Column<int>(nullable: true),
                    System_State = table.Column<int>(nullable: true),
                    Water_Quality = table.Column<double>(nullable: true),
                    Water_Temperature = table.Column<double>(nullable: true),
                    System_Pressure = table.Column<double>(nullable: true),
                    Product_pressure = table.Column<double>(nullable: true),
                    CG220_level = table.Column<double>(nullable: true),
                    Heat_Exchanger_Water_Temp = table.Column<double>(nullable: true),
                    System_24V_power = table.Column<double>(nullable: true),
                    System_5V_power = table.Column<double>(nullable: true),
                    System_3V_power = table.Column<double>(nullable: true),
                    Heat_sink_Temperature = table.Column<double>(nullable: true),
                    Ambient_Temperature = table.Column<double>(nullable: true),
                    DI_water_quality = table.Column<double>(nullable: true),
                    Hydrogen_flow = table.Column<double>(nullable: true),
                    Ext_Water_Quality = table.Column<double>(nullable: true),
                    All_System_Warnings = table.Column<string>(nullable: true),
                    All_System_Errors = table.Column<string>(nullable: true),
                    Thermal_Control_Valve = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readings_ModbusDevices_ModbusDeviceId",
                        column: x => x.ModbusDeviceId,
                        principalTable: "ModbusDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalogChannels_GenericMonitorBoxId",
                table: "AnalogChannels",
                column: "GenericMonitorBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalogChannels_SensorTypeId",
                table: "AnalogChannels",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalChannels_GenericMonitorBoxId",
                table: "DigitalChannels",
                column: "GenericMonitorBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_ModbusDeviceId",
                table: "Readings",
                column: "ModbusDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalogChannels");

            migrationBuilder.DropTable(
                name: "DigitalChannels");

            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ModbusDevices");
        }
    }
}
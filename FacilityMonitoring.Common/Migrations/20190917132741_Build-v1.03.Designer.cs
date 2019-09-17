﻿// <auto-generated />
using System;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FacilityMonitoring.Common.Migrations
{
    [DbContext(typeof(FacilityContext))]
    [Migration("20190917132741_Build-v1.03")]
    partial class Buildv103
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FacilityMonitoring.Common.Model.AmmoniaControllerReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmmoniaControllerId");

                    b.Property<int>("Heater1DutyCycle");

                    b.Property<int>("Heater2DutyCycle");

                    b.Property<int>("Heater3DutyCycle");

                    b.Property<int>("Heater4DutyCycle");

                    b.Property<bool>("Tank1Alarm");

                    b.Property<int>("Tank1Gas");

                    b.Property<int>("Tank1NonZero");

                    b.Property<int>("Tank1NonZeroCal");

                    b.Property<int>("Tank1Tare");

                    b.Property<double>("Tank1Temperature");

                    b.Property<int>("Tank1Total");

                    b.Property<bool>("Tank1Warning");

                    b.Property<int>("Tank1Weight");

                    b.Property<int>("Tank1Zero");

                    b.Property<int>("Tank1ZeroCal");

                    b.Property<bool>("Tank2Alarm");

                    b.Property<int>("Tank2Gas");

                    b.Property<int>("Tank2NonZero");

                    b.Property<int>("Tank2NonZeroCal");

                    b.Property<int>("Tank2Tare");

                    b.Property<double>("Tank2Temperature");

                    b.Property<int>("Tank2Total");

                    b.Property<bool>("Tank2Warning");

                    b.Property<int>("Tank2Weight");

                    b.Property<int>("Tank2Zero");

                    b.Property<int>("Tank2ZeroCal");

                    b.Property<bool>("Tank3Alarm");

                    b.Property<int>("Tank3Gas");

                    b.Property<int>("Tank3NonZero");

                    b.Property<int>("Tank3NonZeroCal");

                    b.Property<int>("Tank3Tare");

                    b.Property<double>("Tank3Temperature");

                    b.Property<int>("Tank3Total");

                    b.Property<bool>("Tank3Warning");

                    b.Property<int>("Tank3Weight");

                    b.Property<int>("Tank3Zero");

                    b.Property<int>("Tank3ZeroCal");

                    b.Property<bool>("Tank4Alarm");

                    b.Property<int>("Tank4Gas");

                    b.Property<int>("Tank4NonZero");

                    b.Property<int>("Tank4NonZeroCal");

                    b.Property<int>("Tank4Tare");

                    b.Property<double>("Tank4Temperature");

                    b.Property<int>("Tank4Total");

                    b.Property<bool>("Tank4Warning");

                    b.Property<int>("Tank4Weight");

                    b.Property<int>("Tank4Zero");

                    b.Property<int>("Tank4ZeroCal");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("AmmoniaControllerId");

                    b.ToTable("AmmoniaControllerReadings");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Category");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GeneratorSystemError", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("E01_A1");

                    b.Property<int>("E01_A2");

                    b.Property<int>("E01_A3");

                    b.Property<int>("E01_B1");

                    b.Property<int>("E01_B2");

                    b.Property<int>("E01_B3");

                    b.Property<int>("E01_C1");

                    b.Property<int>("E01_C2");

                    b.Property<int>("E01_C3");

                    b.Property<int>("E02_A1");

                    b.Property<int>("E02_A2");

                    b.Property<int>("E02_A3");

                    b.Property<int>("E02_B1");

                    b.Property<int>("E02_B2");

                    b.Property<int>("E02_B3");

                    b.Property<int>("E02_C1");

                    b.Property<int>("E02_C2");

                    b.Property<int>("E02_C3");

                    b.Property<int>("E03_A");

                    b.Property<int>("E03_B");

                    b.Property<int>("E03_C");

                    b.Property<int>("E04_A");

                    b.Property<int>("E04_B");

                    b.Property<int>("E04_C");

                    b.Property<int>("E05_A1");

                    b.Property<int>("E05_A2");

                    b.Property<int>("E05_A3");

                    b.Property<int>("E05_B1");

                    b.Property<int>("E05_B2");

                    b.Property<int>("E05_B3");

                    b.Property<int>("E05_C1");

                    b.Property<int>("E05_C2");

                    b.Property<int>("E05_C3");

                    b.Property<int>("E06");

                    b.Property<int>("E07");

                    b.Property<int>("E08");

                    b.Property<int>("E09");

                    b.Property<int>("E10");

                    b.Property<int>("E11");

                    b.Property<int>("E12");

                    b.Property<int>("E13");

                    b.Property<int>("E14");

                    b.Property<int>("E15");

                    b.Property<int>("E16_A");

                    b.Property<int>("E16_B");

                    b.Property<int>("E17");

                    b.Property<int>("E18");

                    b.Property<int>("E19");

                    b.Property<int>("E20_A");

                    b.Property<int>("E20_B");

                    b.Property<int>("E21");

                    b.Property<int>("E22");

                    b.Property<int>("E23");

                    b.Property<int>("E24");

                    b.Property<int>("E25");

                    b.Property<int>("E26");

                    b.Property<int>("E27");

                    b.Property<int>("E28");

                    b.Property<int>("E29");

                    b.Property<int>("E30");

                    b.Property<int>("E31");

                    b.Property<int>("E32");

                    b.Property<int>("E33");

                    b.Property<int>("E34");

                    b.Property<int>("E35");

                    b.Property<int>("E36_A");

                    b.Property<int>("E36_B");

                    b.Property<int>("E36_C");

                    b.Property<int>("E37");

                    b.Property<int>("E38");

                    b.Property<int>("E39");

                    b.Property<int>("E40");

                    b.Property<int>("E41");

                    b.Property<int>("E42");

                    b.Property<int>("E43");

                    b.Property<int>("E44");

                    b.Property<int>("E45");

                    b.Property<int>("E46");

                    b.Property<int>("E47");

                    b.Property<int>("E48");

                    b.Property<int>("H2GenReadingId");

                    b.HasKey("Id");

                    b.HasIndex("H2GenReadingId")
                        .IsUnique();

                    b.ToTable("GeneratorSystemError");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GeneratorSystemWarning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("H2GenReadingId");

                    b.Property<int>("W01");

                    b.Property<int>("W02");

                    b.Property<int>("W03");

                    b.Property<int>("W04");

                    b.Property<int>("W05");

                    b.Property<int>("W06");

                    b.Property<int>("W07");

                    b.Property<int>("W08");

                    b.Property<int>("W09");

                    b.Property<int>("W10");

                    b.Property<int>("W11");

                    b.Property<int>("W12");

                    b.Property<int>("W13");

                    b.Property<int>("W14");

                    b.Property<int>("W15");

                    b.Property<int>("W16");

                    b.Property<int>("W17");

                    b.Property<int>("W18");

                    b.Property<int>("W19");

                    b.Property<int>("W20");

                    b.Property<int>("W21");

                    b.Property<int>("W22");

                    b.HasKey("Id");

                    b.HasIndex("H2GenReadingId")
                        .IsUnique();

                    b.ToTable("GeneratorSystemWarning");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GenericBoxReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Alarm1");

                    b.Property<bool>("Alarm2");

                    b.Property<bool>("Alarm3");

                    b.Property<double>("AnalogCh1");

                    b.Property<double>("AnalogCh10");

                    b.Property<double>("AnalogCh11");

                    b.Property<double>("AnalogCh12");

                    b.Property<double>("AnalogCh13");

                    b.Property<double>("AnalogCh14");

                    b.Property<double>("AnalogCh15");

                    b.Property<double>("AnalogCh16");

                    b.Property<double>("AnalogCh2");

                    b.Property<double>("AnalogCh3");

                    b.Property<double>("AnalogCh4");

                    b.Property<double>("AnalogCh5");

                    b.Property<double>("AnalogCh6");

                    b.Property<double>("AnalogCh7");

                    b.Property<double>("AnalogCh8");

                    b.Property<double>("AnalogCh9");

                    b.Property<bool>("DigitalCh1");

                    b.Property<bool>("DigitalCh10");

                    b.Property<bool>("DigitalCh11");

                    b.Property<bool>("DigitalCh12");

                    b.Property<bool>("DigitalCh13");

                    b.Property<bool>("DigitalCh14");

                    b.Property<bool>("DigitalCh15");

                    b.Property<bool>("DigitalCh16");

                    b.Property<bool>("DigitalCh17");

                    b.Property<bool>("DigitalCh18");

                    b.Property<bool>("DigitalCh19");

                    b.Property<bool>("DigitalCh2");

                    b.Property<bool>("DigitalCh20");

                    b.Property<bool>("DigitalCh21");

                    b.Property<bool>("DigitalCh22");

                    b.Property<bool>("DigitalCh23");

                    b.Property<bool>("DigitalCh24");

                    b.Property<bool>("DigitalCh25");

                    b.Property<bool>("DigitalCh26");

                    b.Property<bool>("DigitalCh27");

                    b.Property<bool>("DigitalCh28");

                    b.Property<bool>("DigitalCh29");

                    b.Property<bool>("DigitalCh3");

                    b.Property<bool>("DigitalCh30");

                    b.Property<bool>("DigitalCh31");

                    b.Property<bool>("DigitalCh32");

                    b.Property<bool>("DigitalCh33");

                    b.Property<bool>("DigitalCh34");

                    b.Property<bool>("DigitalCh35");

                    b.Property<bool>("DigitalCh36");

                    b.Property<bool>("DigitalCh37");

                    b.Property<bool>("DigitalCh38");

                    b.Property<bool>("DigitalCh4");

                    b.Property<bool>("DigitalCh5");

                    b.Property<bool>("DigitalCh6");

                    b.Property<bool>("DigitalCh7");

                    b.Property<bool>("DigitalCh8");

                    b.Property<bool>("DigitalCh9");

                    b.Property<int>("GenericMonitorBoxId");

                    b.Property<string>("Identifier");

                    b.Property<bool>("OutputCh1");

                    b.Property<bool>("OutputCh10");

                    b.Property<bool>("OutputCh2");

                    b.Property<bool>("OutputCh3");

                    b.Property<bool>("OutputCh4");

                    b.Property<bool>("OutputCh5");

                    b.Property<bool>("OutputCh6");

                    b.Property<bool>("OutputCh7");

                    b.Property<bool>("OutputCh8");

                    b.Property<bool>("OutputCh9");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("GenericMonitorBoxId");

                    b.ToTable("GenericBoxReadings");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.H2GenReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("A200DrainValve");

                    b.Property<int>("A200InletValve");

                    b.Property<int>("A200LevelEmpty");

                    b.Property<int>("A200LevelFlooded");

                    b.Property<int>("A200LevelHigh");

                    b.Property<int>("A200LevelLow");

                    b.Property<int>("A300DrainValve");

                    b.Property<int>("A300LevelEmpty");

                    b.Property<int>("A300LevelFlooded");

                    b.Property<int>("A300LevelHigh");

                    b.Property<int>("A300LevelLow");

                    b.Property<double>("AmbientTemperature");

                    b.Property<double>("CG220Level");

                    b.Property<double>("DIWaterQuality");

                    b.Property<int>("DryerValve1");

                    b.Property<int>("DryerValve2");

                    b.Property<int>("DryerValve3");

                    b.Property<int>("DryerValve4");

                    b.Property<double>("ExtWaterQuality");

                    b.Property<int>("GeneratorId");

                    b.Property<double>("HeatExchangerWaterTemp");

                    b.Property<double>("HeatSinkTemperature");

                    b.Property<double>("HydrogenFlow");

                    b.Property<string>("Identifier");

                    b.Property<int>("OperationMode");

                    b.Property<int>("PSFaultA1");

                    b.Property<int>("PSFaultA2");

                    b.Property<int>("PSFaultB1");

                    b.Property<int>("PSFaultB2");

                    b.Property<int>("PSFaultC1");

                    b.Property<int>("PSFaultC2");

                    b.Property<int>("PSFault_A3");

                    b.Property<int>("PSFault_B3");

                    b.Property<int>("PSFault_C3");

                    b.Property<int>("PSUnitStatus");

                    b.Property<int>("PSV_A1");

                    b.Property<int>("PSV_A1A2");

                    b.Property<int>("PSV_A3");

                    b.Property<int>("PSV_B1");

                    b.Property<int>("PSV_B2");

                    b.Property<int>("PSV_B3");

                    b.Property<int>("PSV_C1");

                    b.Property<int>("PSV_C2");

                    b.Property<int>("PSV_C3");

                    b.Property<double>("ProductPressure");

                    b.Property<int>("PsCardEnableA1");

                    b.Property<int>("PsCardEnableA2");

                    b.Property<int>("PsCardEnableB1");

                    b.Property<int>("PsCardEnableB2");

                    b.Property<int>("PsCardEnableC1");

                    b.Property<int>("PsCardEnableC2");

                    b.Property<int>("PsCardEnable_A3");

                    b.Property<int>("PsCardEnable_B3");

                    b.Property<int>("PsCardEnable_C3");

                    b.Property<int>("PumpControl");

                    b.Property<int>("StackAMonitorCurrent");

                    b.Property<int>("StackAValve");

                    b.Property<int>("StackAWaterFlow");

                    b.Property<int>("StackBMonitorCurrent");

                    b.Property<int>("StackBValve");

                    b.Property<int>("StackBWaterFlow");

                    b.Property<int>("StackCMonitorCurrent");

                    b.Property<int>("StackCValve");

                    b.Property<int>("StackCWaterFlow");

                    b.Property<double>("System24VPower");

                    b.Property<double>("System3VPower");

                    b.Property<double>("System5VPower");

                    b.Property<int>("SystemMode");

                    b.Property<double>("SystemPressure");

                    b.Property<int>("SystemState");

                    b.Property<double>("ThermalControlValve");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<int>("VentValve");

                    b.Property<double>("WaterQuality");

                    b.Property<double>("WaterTemperature");

                    b.HasKey("Id");

                    b.HasIndex("GeneratorId");

                    b.ToTable("H2GenReadings");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.ModbusDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BypassAll");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Identifier");

                    b.Property<string>("IpAddress");

                    b.Property<int>("Port");

                    b.Property<int>("SlaveAddress");

                    b.Property<int>("State");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("ModbusDevices");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ModbusDevice");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AmmoniaControllerId");

                    b.Property<bool>("Bypass");

                    b.Property<bool>("Connected");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("GenericMonitorBoxId");

                    b.Property<int?>("H2GeneratorId");

                    b.Property<int>("Logic");

                    b.Property<string>("Name");

                    b.Property<string>("PropertyMap");

                    b.Property<int>("RegisterIndex");

                    b.Property<int>("RegisterLength");

                    b.Property<int?>("SensorTypeId");

                    b.HasKey("Id");

                    b.HasIndex("AmmoniaControllerId");

                    b.HasIndex("GenericMonitorBoxId");

                    b.HasIndex("H2GeneratorId");

                    b.ToTable("Registers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Register");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.SensorType", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.Category");

                    b.Property<double>("MaxPoint");

                    b.Property<string>("Units");

                    b.Property<double>("ZeroPoint");

                    b.HasDiscriminator().HasValue("SensorType");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.AmmoniaController", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.ModbusDevice");

                    b.Property<int>("ActiveTank");

                    b.Property<int>("CalInputBaseAddr");

                    b.Property<int>("CalInputLength");

                    b.Property<int>("CalModeAddr");

                    b.Property<int>("CoilBaseAddress");

                    b.Property<int>("DataForInputAddr");

                    b.Property<int>("ReadCoilLength");

                    b.Property<int>("ReadRegisterLength");

                    b.Property<int>("RegisterBaseAddress");

                    b.HasDiscriminator().HasValue("AmmoniaController");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GenericMonitorBox", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.ModbusDevice");

                    b.Property<int>("AlarmAddr");

                    b.Property<int>("AnalogChannelCount");

                    b.Property<int>("DigitalInputChannelCount");

                    b.Property<int>("DigitalOutputChannelCount");

                    b.Property<int>("ModbusComAddr");

                    b.Property<int>("SoftwareMaintAddr");

                    b.Property<int>("StateAddr");

                    b.Property<int>("WarningAddr");

                    b.HasDiscriminator().HasValue("GenericMonitorBox");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.H2Generator", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.ModbusDevice");

                    b.Property<int>("OperationMode");

                    b.Property<int>("SystemState");

                    b.HasDiscriminator().HasValue("H2Generator");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.AnalogChannel", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.Register");

                    b.Property<int>("Alarm1Action");

                    b.Property<bool>("Alarm1Enabled");

                    b.Property<double>("Alarm1SetPoint");

                    b.Property<int>("Alarm2Action");

                    b.Property<bool>("Alarm2Enabled");

                    b.Property<double>("Alarm2SetPoint");

                    b.Property<int>("Alarm3Action");

                    b.Property<bool>("Alarm3Enabled");

                    b.Property<double>("Alarm3SetPoint");

                    b.Property<double>("MaxValue");

                    b.Property<double>("Offset");

                    b.Property<double>("Resistance");

                    b.Property<double>("Slope");

                    b.Property<double>("ValueDivisor");

                    b.Property<double>("ZeroValue");

                    b.HasIndex("SensorTypeId");

                    b.HasDiscriminator().HasValue("AnalogChannel");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.DigitalInputChannel", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.Register");

                    b.Property<int>("AlarmAction");

                    b.HasIndex("SensorTypeId")
                        .HasName("IX_Registers_SensorTypeId1");

                    b.HasDiscriminator().HasValue("DigitalInputChannel");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.DigitalOutputChannel", b =>
                {
                    b.HasBaseType("FacilityMonitoring.Common.Model.Register");

                    b.Property<int>("OutputControl");

                    b.HasIndex("SensorTypeId")
                        .HasName("IX_Registers_SensorTypeId2");

                    b.HasDiscriminator().HasValue("DigitalOutputChannel");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.AmmoniaControllerReading", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.AmmoniaController", "AmmoniaController")
                        .WithMany("Readings")
                        .HasForeignKey("AmmoniaControllerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GeneratorSystemError", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.H2GenReading", "H2GenReading")
                        .WithOne("AllSystemErrors")
                        .HasForeignKey("FacilityMonitoring.Common.Model.GeneratorSystemError", "H2GenReadingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GeneratorSystemWarning", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.H2GenReading", "H2GenReading")
                        .WithOne("AllSystemWarnings")
                        .HasForeignKey("FacilityMonitoring.Common.Model.GeneratorSystemWarning", "H2GenReadingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.GenericBoxReading", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.GenericMonitorBox", "GenericMonitorBox")
                        .WithMany("BoxReadings")
                        .HasForeignKey("GenericMonitorBoxId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.H2GenReading", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.H2Generator", "H2Generator")
                        .WithMany("H2Readings")
                        .HasForeignKey("GeneratorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.Register", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.AmmoniaController")
                        .WithMany("Registers")
                        .HasForeignKey("AmmoniaControllerId");

                    b.HasOne("FacilityMonitoring.Common.Model.GenericMonitorBox", "GenericMonitorBox")
                        .WithMany("Registers")
                        .HasForeignKey("GenericMonitorBoxId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FacilityMonitoring.Common.Model.H2Generator")
                        .WithMany("Registers")
                        .HasForeignKey("H2GeneratorId");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.AnalogChannel", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.SensorType", "SensorType")
                        .WithMany("AnalogChannels")
                        .HasForeignKey("SensorTypeId");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.DigitalInputChannel", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.SensorType", "SensorType")
                        .WithMany()
                        .HasForeignKey("SensorTypeId")
                        .HasConstraintName("FK_Registers_Categories_SensorTypeId1");
                });

            modelBuilder.Entity("FacilityMonitoring.Common.Model.DigitalOutputChannel", b =>
                {
                    b.HasOne("FacilityMonitoring.Common.Model.SensorType", "SensorType")
                        .WithMany()
                        .HasForeignKey("SensorTypeId")
                        .HasConstraintName("FK_Registers_Categories_SensorTypeId2");
                });
#pragma warning restore 612, 618
        }
    }
}

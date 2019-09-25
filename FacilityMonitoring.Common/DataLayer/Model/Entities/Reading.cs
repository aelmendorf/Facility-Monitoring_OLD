using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Data;

namespace FacilityMonitoring.Common.Model {

    public partial class GenericBoxReading {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Identifier { get; set; }
        public int GenericMonitorBoxId { get; set; }
        public GenericMonitorBox GenericMonitorBox { get; set; }

        public GenericBoxReading() { }

        public GenericBoxReading(DateTime timestamp, string identifier, GenericMonitorBox device) {
            this.TimeStamp = timestamp;
            this.Identifier = identifier;
        }

        public double AnalogCh1 { get; set; }
        public double AnalogCh2 { get; set; }
        public double AnalogCh3 { get; set; }
        public double AnalogCh4 { get; set; }
        public double AnalogCh5 { get; set; }
        public double AnalogCh6 { get; set; }
        public double AnalogCh7 { get; set; }
        public double AnalogCh8 { get; set; }
        public double AnalogCh9 { get; set; }
        public double AnalogCh10 { get; set; }
        public double AnalogCh11 { get; set; }
        public double AnalogCh12 { get; set; }
        public double AnalogCh13 { get; set; }
        public double AnalogCh14 { get; set; }
        public double AnalogCh15 { get; set; }
        public double AnalogCh16 { get; set; }

        public bool DigitalCh1 { get; set; }
        public bool DigitalCh2 { get; set; }
        public bool DigitalCh3 { get; set; }
        public bool DigitalCh4 { get; set; }
        public bool DigitalCh5 { get; set; }
        public bool DigitalCh6 { get; set; }
        public bool DigitalCh7 { get; set; }
        public bool DigitalCh8 { get; set; }
        public bool DigitalCh9 { get; set; }
        public bool DigitalCh10 { get; set; }
        public bool DigitalCh11 { get; set; }
        public bool DigitalCh12 { get; set; }
        public bool DigitalCh13 { get; set; }
        public bool DigitalCh14 { get; set; }
        public bool DigitalCh15 { get; set; }
        public bool DigitalCh16 { get; set; }
        public bool DigitalCh17 { get; set; }
        public bool DigitalCh18 { get; set; }
        public bool DigitalCh19 { get; set; }
        public bool DigitalCh20 { get; set; }
        public bool DigitalCh21 { get; set; }
        public bool DigitalCh22 { get; set; }
        public bool DigitalCh23 { get; set; }
        public bool DigitalCh24 { get; set; }
        public bool DigitalCh25 { get; set; }
        public bool DigitalCh26 { get; set; }
        public bool DigitalCh27 { get; set; }
        public bool DigitalCh28 { get; set; }
        public bool DigitalCh29 { get; set; }
        public bool DigitalCh30 { get; set; }
        public bool DigitalCh31 { get; set; }
        public bool DigitalCh32 { get; set; }
        public bool DigitalCh33 { get; set; }
        public bool DigitalCh34 { get; set; }
        public bool DigitalCh35 { get; set; }
        public bool DigitalCh36 { get; set; }
        public bool DigitalCh37 { get; set; }
        public bool DigitalCh38 { get; set; }

        public bool OutputCh1 { get; set; }
        public bool OutputCh2 { get; set; }
        public bool OutputCh3 { get; set; }
        public bool OutputCh4 { get; set; }
        public bool OutputCh5 { get; set; }
        public bool OutputCh6 { get; set; }
        public bool OutputCh7 { get; set; }
        public bool OutputCh8 { get; set; }
        public bool OutputCh9 { get; set; }
        public bool OutputCh10 { get; set; }

        public GenericMonitorBoxAlert GenericMonitorBoxAlert { get; set; }

        public bool Alarm1 { get; set; }
        public bool Alarm2 { get; set; }
        public bool Alarm3 { get; set; }

        [NotMapped]
        public object this[string propertyName] {
            set {
                switch (propertyName) {
                    case "AnalogCh1":
                        AnalogCh1 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh2":
                        AnalogCh2 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh3":
                        AnalogCh3 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh4":
                        AnalogCh4 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh5":
                        AnalogCh5 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh6":
                        AnalogCh6 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh7":
                        AnalogCh7 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh8":
                        AnalogCh8 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh9":
                        AnalogCh9 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh10":
                        AnalogCh10 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh11":
                        AnalogCh11 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh12":
                        AnalogCh12 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh13":
                        AnalogCh13 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh14":
                        AnalogCh14 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh15":
                        AnalogCh15 = Convert.ToDouble(value);
                        break;
                    case "AnalogCh16":
                        AnalogCh16 = Convert.ToDouble(value);
                        break;
                    case "DigitalCh1":
                        this.DigitalCh1 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh2":
                        this.DigitalCh2 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh3":
                        this.DigitalCh1 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh4":
                        this.DigitalCh4 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh5":
                        this.DigitalCh5 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh6":
                        this.DigitalCh6 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh7":
                        this.DigitalCh7 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh8":
                        this.DigitalCh8 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh9":
                        this.DigitalCh9 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh10":
                        this.DigitalCh10 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh11":
                        this.DigitalCh11 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh12":
                        this.DigitalCh12 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh13":
                        this.DigitalCh13 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh14":
                        this.DigitalCh14 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh15":
                        this.DigitalCh15 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh16":
                        this.DigitalCh16 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh17":
                        this.DigitalCh17 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh18":
                        this.DigitalCh18 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh19":
                        this.DigitalCh19 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh20":
                        this.DigitalCh20 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh21":
                        this.DigitalCh21 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh22":
                        this.DigitalCh22 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh23":
                        this.DigitalCh23 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh24":
                        this.DigitalCh24 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh25":
                        this.DigitalCh25 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh26":
                        this.DigitalCh26 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh27":
                        this.DigitalCh27 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh28":
                        this.DigitalCh28 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh29":
                        this.DigitalCh29 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh30":
                        this.DigitalCh30 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh31":
                        this.DigitalCh31 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh32":
                        this.DigitalCh32 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh33":
                        this.DigitalCh33 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh34":
                        this.DigitalCh34 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh35":
                        this.DigitalCh35 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh36":
                        this.DigitalCh36 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh37":
                        this.DigitalCh37 = Convert.ToBoolean(value);
                        break;
                    case "DigitalCh38":
                        this.DigitalCh38 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh1":
                        this.OutputCh1 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh2":
                        this.OutputCh2 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh3":
                        this.OutputCh3 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh4":
                        this.OutputCh4 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh5":
                        this.OutputCh5 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh6":
                        this.OutputCh6 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh7":
                        this.OutputCh7 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh8":
                        this.OutputCh8 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh9":
                        this.OutputCh9 = Convert.ToBoolean(value);
                        break;
                    case "OutputCh10":
                        this.OutputCh10 = Convert.ToBoolean(value);
                        break;
                }
            }
            get {
                switch (propertyName) {
                    case "AnalogCh1":
                        return AnalogCh1;
                    case "AnalogCh2":
                        return AnalogCh2;
                    case "AnalogCh3":
                        return AnalogCh3;
                    case "AnalogCh4":
                        return AnalogCh4;
                    case "AnalogCh5":
                        return AnalogCh5;
                    case "AnalogCh6":
                        return AnalogCh6;
                    case "AnalogCh7":
                        return AnalogCh7;
                    case "AnalogCh8":
                        return AnalogCh8;
                    case "AnalogCh9":
                        return AnalogCh9;
                    case "AnalogCh10":
                        return AnalogCh10;
                    case "AnalogCh11":
                        return AnalogCh11;
                    case "AnalogCh12":
                        return AnalogCh12;
                    case "AnalogCh13":
                        return AnalogCh13;
                    case "AnalogCh14":
                        return AnalogCh14;
                    case "AnalogCh15":
                        return AnalogCh15;
                    case "AnalogCh16":
                        return AnalogCh16;
                    case "DigitalCh1":
                        return this.DigitalCh1;
                    case "DigitalCh2":
                        return this.DigitalCh2;
                    case "DigitalCh3":
                        return this.DigitalCh1;
                    case "DigitalCh4":
                        return this.DigitalCh4;
                    case "DigitalCh5":
                        return this.DigitalCh5;
                    case "DigitalCh6":
                        return this.DigitalCh6;
                    case "DigitalCh7":
                        return this.DigitalCh7;
                    case "DigitalCh8":
                        return this.DigitalCh8;
                    case "DigitalCh9":
                        return this.DigitalCh9;
                    case "DigitalCh10":
                        return this.DigitalCh10;
                    case "DigitalCh11":
                        return this.DigitalCh11;
                    case "DigitalCh12":
                        return this.DigitalCh12;
                    case "DigitalCh13":
                        return this.DigitalCh13;
                    case "DigitalCh14":
                        return this.DigitalCh14;
                    case "DigitalCh15":
                        return this.DigitalCh15;
                    case "DigitalCh16":
                        return this.DigitalCh16;
                    case "DigitalCh17":
                        return this.DigitalCh17;
                    case "DigitalCh18":
                        return this.DigitalCh18;
                    case "DigitalCh19":
                        return this.DigitalCh19;
                    case "DigitalCh20":
                        return this.DigitalCh20;
                    case "DigitalCh21":
                        return this.DigitalCh21;
                    case "DigitalCh22":
                        return this.DigitalCh22;
                    case "DigitalCh23":
                        return this.DigitalCh23;
                    case "DigitalCh24":
                        return this.DigitalCh24;
                    case "DigitalCh25":
                        return this.DigitalCh25;
                    case "DigitalCh26":
                        return this.DigitalCh26;
                    case "DigitalCh27":
                        return this.DigitalCh27;
                    case "DigitalCh28":
                        return this.DigitalCh28;
                    case "DigitalCh29":
                        return this.DigitalCh29;
                    case "DigitalCh30":
                        return this.DigitalCh30;
                    case "DigitalCh31":
                        return this.DigitalCh31;
                    case "DigitalCh32":
                        return this.DigitalCh32;
                    case "DigitalCh33":
                        return this.DigitalCh33;
                    case "DigitalCh34":
                        return this.DigitalCh34;
                    case "DigitalCh35":
                        return this.DigitalCh35;
                    case "DigitalCh36":
                        return this.DigitalCh36;
                    case "DigitalCh37":
                        return this.DigitalCh37;
                    case "DigitalCh38":
                        return this.DigitalCh38;
                    case "OutputCh1":
                        return this.OutputCh1;
                    case "OutputCh2":
                        return this.OutputCh2;
                    case "OutputCh3":
                        return this.OutputCh3;
                    case "OutputCh4":
                        return this.OutputCh4;
                    case "OutputCh5":
                        return this.OutputCh5;
                    case "OutputCh6":
                        return this.OutputCh6;
                    case "OutputCh7":
                        return this.OutputCh7;
                    case "OutputCh8":
                        return this.OutputCh8;
                    case "OutputCh9":
                        return this.OutputCh9;
                    case "OutputCh10":
                        return this.OutputCh10;
                    default: return null;

                }
            }
        }
    }

    public partial class H2GenReading {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Identifier { get; set; }

        public int GeneratorId { get; set; }
        public H2Generator H2Generator { get; set; }

        public H2GenReading() { }

        public H2GenReading(DateTime timestamp, string identifier, H2Generator generator) {
            this.TimeStamp = timestamp;
            this.Identifier = identifier;
            this.H2Generator = generator;
        }

        public object this[string param] {
            get {
                switch (param) {
                    case "A200LevelFlooded": { return this.A200LevelFlooded; }
                    case "A200LevelHigh": { return this.A200LevelHigh; }
                    case "A200LevelLow": { return this.A200LevelLow; }
                    case "A300LevelFlooded": { return this.A300LevelFlooded; }
                    case "A300LevelHigh": { return this.A300LevelHigh; }
                    case "A300LevelLow": { return this.A300LevelLow; }
                    case "A200LevelEmpty": { return this.A200LevelEmpty; }
                    case "A300LevelEmpty": { return this.A300LevelEmpty; }
                    case "StackAWaterFlow": { return this.StackAWaterFlow; }
                    case "StackBWaterFlow": { return this.StackBWaterFlow; }
                    case "StackCWaterFlow": { return this.StackCWaterFlow; }
                    case "VentValve": { return this.VentValve; }
                    case "A200DrainValve": { return this.A200DrainValve; }
                    case "A200InletValve": { return this.A200InletValve; }
                    case "DryerValve1": { return this.DryerValve1; }
                    case "DryerValve2": { return this.DryerValve2; }
                    case "DryerValve3": { return this.DryerValve3; }
                    case "DryerValve4": { return this.DryerValve4; }
                    case "A300DrainValve": { return this.A300DrainValve; }
                    case "StackAValve": { return this.StackAValve; }
                    case "StackBValve": { return this.StackBValve; }
                    case "StackCValve": { return this.StackCValve; }
                    case "PumpControl": { return this.PumpControl; }
                    case "PSV_A1": { return this.PSV_A1; }
                    case "PSV_A2": { return this.PSV_A2; }
                    case "PSV_B1": { return this.PSV_B1; }
                    case "PSV_B2": { return this.PSV_B2; }
                    case "PSV_C1": { return this.PSV_C1; }
                    case "PSV_C2": { return this.PSV_C2; }
                    case "StackAMonitorCurrent": { return this.StackAMonitorCurrent; }
                    case "StackBMonitorCurrent": { return this.StackBMonitorCurrent; }
                    case "StackCMonitorCurrent": { return this.StackCMonitorCurrent; }
                    case "PSFaultA1": { return this.PSFaultA1; }
                    case "PSFaultA2": { return this.PSFaultA2; }
                    case "PSFaultB1": { return this.PSFaultB1; }
                    case "PSFaultB2": { return this.PSFaultB2; }
                    case "PSFaultC1": { return this.PSFaultC1; }
                    case "PSFaultC2": { return this.PSFaultC2; }
                    case "PSUnitStatus": { return this.PSUnitStatus; }
                    case "PsCardEnableA1": { return this.PsCardEnableA1; }
                    case "PsCardEnableA2": { return this.PsCardEnableA2; }
                    case "PsCardEnableB1": { return this.PsCardEnableB1; }
                    case "PsCardEnableB2": { return this.PsCardEnableB2; }
                    case "PsCardEnableC1": { return this.PsCardEnableC1; }
                    case "PsCardEnableC2": { return this.PsCardEnableC2; }
                    case "PSV_A3": { return this.PSV_A3; }
                    case "PSV_B3": { return this.PSV_B3; }
                    case "PSV_C3": { return this.PSV_C3; }
                    case "PSFault_A3": { return this.PSFault_A3; }
                    case "PSFault_B3": { return this.PSFault_B3; }
                    case "PSFault_C3": { return this.PSFault_C3; }
                    case "PsCardEnable_A3": { return this.PsCardEnable_A3; }
                    case "PsCardEnable_B3": { return this.PsCardEnable_B3; }
                    case "PsCardEnable_C3": { return this.PsCardEnable_C3; }
                    case "SystemMode": { return this.SystemMode; }
                    case "OperationMode": { return this.OperationMode; }
                    case "SystemState": { return this.SystemState; }
                    case "WaterQuality": { return this.WaterQuality; }
                    case "WaterTemperature": { return this.WaterTemperature; }
                    case "SystemPressure": { return this.SystemPressure; }
                    case "ProductPressure": { return this.ProductPressure; }
                    case "CG220Level": { return this.CG220Level; }
                    case "HeatExchangerWaterTemp": { return this.HeatExchangerWaterTemp; }
                    case "System24VPower": { return this.System24VPower; }
                    case "System5VPower": { return this.System5VPower; }
                    case "System3VPower": { return this.System3VPower; }
                    case "HeatSinkTemperature": { return this.HeatSinkTemperature; }
                    case "AmbientTemperature": { return this.AmbientTemperature; }
                    case "DIWaterQuality": { return this.DIWaterQuality; }
                    case "HydrogenFlow": { return this.HydrogenFlow; }
                    case "ExtWaterQuality": { return this.ExtWaterQuality; }
                    case "AllSystemWarnings": { return this.AllSystemWarnings; }
                    case "AllSystemErrors": { return this.AllSystemErrors; }
                    default: { return this.Id; }
                }
            }
            set {
                switch (param) {
                    case "A200LevelFlooded": { this.A200LevelFlooded = (WaterLevel)value; break; }
                    case "A200LevelHigh": { this.A200LevelHigh = (WaterLevel)value; break; }
                    case "A200LevelLow": { this.A200LevelLow = (WaterLevel)value; break; }
                    case "A300LevelFlooded": { this.A300LevelFlooded = (WaterLevel)value; break; }
                    case "A300LevelHigh": { this.A300LevelHigh = (WaterLevel)value; break; }
                    case "A300LevelLow": { this.A300LevelLow = (WaterLevel)value; break; }
                    case "A200LevelEmpty": { this.A200LevelEmpty = (WaterLevel)value; break; }
                    case "A300LevelEmpty": { this.A300LevelEmpty = (WaterLevel)value; break; }
                    case "StackAWaterFlow": { this.StackAWaterFlow = (WaterFlow)value; break; }
                    case "StackBWaterFlow": { this.StackBWaterFlow = (WaterFlow)value; break; }
                    case "StackCWaterFlow": { this.StackCWaterFlow = (WaterFlow)value; break; }
                    case "VentValve": { this.VentValve = (ONOFF)value; break; }
                    case "A200DrainValve": { this.A200DrainValve = (ONOFF)value; break; }
                    case "A200InletValve": { this.A200InletValve = (ONOFF)value; break; }
                    case "DryerValve1": { this.DryerValve1 = (ONOFF)value; break; }
                    case "DryerValve2": { this.DryerValve2 = (ONOFF)value; break; }
                    case "DryerValve3": { this.DryerValve3 = (ONOFF)value; break; }
                    case "DryerValve4": { this.DryerValve4 = (ONOFF)value; break; }
                    case "A300DrainValve": { this.A300DrainValve = (ONOFF)value; break; }
                    case "StackAValve": { this.StackAValve = (ONOFF)value; break; }
                    case "StackBValve": { this.StackBValve = (ONOFF)value; break; }
                    case "StackCValve": { this.StackCValve = (ONOFF)value; break; }
                    case "PumpControl": { this.PumpControl = (ONOFF)value; break; }
                    case "PSV_A1": { this.PSV_A1 = (int)value; break; }
                    case "PSV_A2": { this.PSV_A2 = (int)value; break; }
                    case "PSV_B1": { this.PSV_B1 = (int)value; break; }
                    case "PSV_B2": { this.PSV_B2 = (int)value; break; }
                    case "PSV_C1": { this.PSV_C1 = (int)value; break; }
                    case "PSV_C2": { this.PSV_C2 = (int)value; break; }
                    case "StackAMonitorCurrent": { this.StackAMonitorCurrent = (int)value; break; }
                    case "StackBMonitorCurrent": { this.StackBMonitorCurrent = (int)value; break; }
                    case "StackCMonitorCurrent": { this.StackCMonitorCurrent = (int)value; break; }
                    case "PSFaultA1": { this.PSFaultA1 = (FaultState)value; break; }
                    case "PSFaultA2": { this.PSFaultA2 = (FaultState)value; break; }
                    case "PSFaultB1": { this.PSFaultB1 = (FaultState)value; break; }
                    case "PSFaultB2": { this.PSFaultB2 = (FaultState)value; break; }
                    case "PSFaultC1": { this.PSFaultC1 = (FaultState)value; break; }
                    case "PSFaultC2": { this.PSFaultC2 = (FaultState)value; break; }
                    case "PSUnitStatus": { this.PSUnitStatus = (FaultState)value; break; }
                    case "PsCardEnableA1": { this.PsCardEnableA1 = (EnableState)value; break; }
                    case "PsCardEnableA2": { this.PsCardEnableA2 = (EnableState)value; break; }
                    case "PsCardEnableB1": { this.PsCardEnableB1 = (EnableState)value; break; }
                    case "PsCardEnableB2": { this.PsCardEnableB2 = (EnableState)value; break; }
                    case "PsCardEnableC1": { this.PsCardEnableC1 = (EnableState)value; break; }
                    case "PsCardEnableC2": { this.PsCardEnableC2 = (EnableState)value; break; }
                    case "PSV_A3": { this.PSV_A3 = (int)value; break; }
                    case "PSV_B3": { this.PSV_B3 = (int)value; break; }
                    case "PSV_C3": { this.PSV_C3 = (int)value; break; }
                    case "PSFault_A3": { this.PSFault_A3 = (FaultState)value; break; }
                    case "PSFault_B3": { this.PSFault_B3 = (FaultState)value; break; }
                    case "PSFault_C3": { this.PSFault_C3 = (FaultState)value; break; }
                    case "PsCardEnable_A3": { this.PsCardEnable_A3 = (EnableState)value; break; }
                    case "PsCardEnable_B3": { this.PsCardEnable_B3 = (EnableState)value; break; }
                    case "PsCardEnable_C3": { this.PsCardEnable_C3 = (EnableState)value; break; }
                    case "SystemMode": { this.SystemMode = (SystemMode)value; break; }
                    case "OperationMode": { this.OperationMode = (OperationMode)value; break; }
                    case "SystemState": { this.SystemState = (SystemState)value; break; }
                    case "WaterQuality": { this.WaterQuality = (double)value; break; }
                    case "WaterTemperature": { this.WaterTemperature = (double)value; break; }
                    case "SystemPressure": { this.SystemPressure = (double)value; break; }
                    case "ProductPressure": { this.ProductPressure = (double)value; break; }
                    case "CG220Level": { this.CG220Level = (double)value; break; }
                    case "HeatExchangerWaterTemp": { this.HeatExchangerWaterTemp = (double)value; break; }
                    case "System24VPower": { this.System24VPower = (double)value; break; }
                    case "System5VPower": { this.System5VPower = (double)value; break; }
                    case "System3VPower": { this.System3VPower = (double)value; break; }
                    case "HeatSinkTemperature": { this.HeatSinkTemperature = (double)value; break; }
                    case "AmbientTemperature": { this.AmbientTemperature = (double)value; break; }
                    case "DIWaterQuality": { this.DIWaterQuality = (double)value; break; }
                    case "HydrogenFlow": { this.HydrogenFlow = (double)value; break; }
                    case "ExtWaterQuality": { this.ExtWaterQuality = (double)value; break; }
                    case "AllSystemWarnings": { this.AllSystemWarnings = (GeneratorSystemWarning)value; break; }
                    case "AllSystemErrors": { this.AllSystemErrors = (GeneratorSystemError)value; break; }
                }
            }
        }

        public WaterLevel A200LevelFlooded { get; set; }    //1
        public WaterLevel A200LevelHigh { get; set; }       //2
        public WaterLevel A200LevelLow { get; set; }        //3
        public WaterLevel A300LevelFlooded { get; set; }    //4
        public WaterLevel A300LevelHigh { get; set; }       //5
        public WaterLevel A300LevelLow { get; set; }        //6
        public WaterLevel A200LevelEmpty { get; set; }      //7
        public WaterLevel A300LevelEmpty { get; set; }      //8
        public WaterFlow StackAWaterFlow { get; set; }    //9
        public WaterFlow StackBWaterFlow { get; set; }    //10
        public WaterFlow StackCWaterFlow { get; set; }    //11
        public ONOFF VentValve { get; set; }            //12
        public ONOFF A200DrainValve { get; set; }      //13
        public ONOFF A200InletValve { get; set; }      //14
        public ONOFF DryerValve1 { get; set; }          //15
        public ONOFF DryerValve2 { get; set; }          //16
        public ONOFF DryerValve3 { get; set; }          //17
        public ONOFF DryerValve4 { get; set; }          //18
        public ONOFF A300DrainValve { get; set; }      //19
        public ONOFF StackAValve { get; set; }         //20
        public ONOFF StackBValve { get; set; }         //21
        public ONOFF StackCValve { get; set; }         //22
        public ONOFF PumpControl { get; set; }          //23

        public int PSV_A1 { get; set; }                 //24
        public int PSV_A2 { get; set; }              //25
        public int PSV_B1 { get; set; }                 //26
        public int PSV_B2 { get; set; }                 //27
        public int PSV_C1 { get; set; }                 //28
        public int PSV_C2 { get; set; }                 //29
        public int StackAMonitorCurrent { get; set; }//30
        public int StackBMonitorCurrent { get; set; }//31
        public int StackCMonitorCurrent { get; set; }//32
        public FaultState PSFaultA1 { get; set; }            //33
        public FaultState PSFaultA2 { get; set; }            //34
        public FaultState PSFaultB1 { get; set; }            //35
        public FaultState PSFaultB2 { get; set; }            //36
        public FaultState PSFaultC1 { get; set; }            //37
        public FaultState PSFaultC2 { get; set; }            //38
        public FaultState PSUnitStatus { get; set; }         //39
        public EnableState PsCardEnableA1 { get; set; }      //40
        public EnableState PsCardEnableA2 { get; set; }      //41
        public EnableState PsCardEnableB1 { get; set; }      //42
        public EnableState PsCardEnableB2 { get; set; }      //43
        public EnableState PsCardEnableC1 { get; set; }      //44       
        public EnableState PsCardEnableC2 { get; set; }      //45
        public int PSV_A3 { get; set; }                 //46
        public int PSV_B3 { get; set; }                 //47
        public int PSV_C3 { get; set; }                 //48
        public FaultState PSFault_A3 { get; set; }            //49
        public FaultState PSFault_B3 { get; set; }            //50
        public FaultState PSFault_C3 { get; set; }            //51
        public EnableState PsCardEnable_A3 { get; set; }      //52
        public EnableState PsCardEnable_B3 { get; set; }      //53
        public EnableState PsCardEnable_C3 { get; set; }      //54
        public SystemMode SystemMode { get; set; }            //55
        public OperationMode OperationMode { get; set; }         //56
        public SystemState SystemState { get; set; }           //57

        public double WaterQuality { get; set; }       //58
        public double WaterTemperature { get; set; }   //59
        public double SystemPressure { get; set; }     //60
        public double ProductPressure { get; set; }    //61
        public double CG220Level { get; set; }         //62
        public double HeatExchangerWaterTemp { get; set; }//63
        public double System24VPower { get; set; }        //64
        public double System5VPower { get; set; }         //65
        public double System3VPower { get; set; }         //66
        public double HeatSinkTemperature { get; set; }   //67
        public double AmbientTemperature { get; set; }    //68
        public double DIWaterQuality { get; set; }        //69
        public double HydrogenFlow { get; set; }          //70
        public double ExtWaterQuality { get; set; }       //71
        public GeneratorSystemWarning AllSystemWarnings { get; set; }     //72
        public GeneratorSystemError AllSystemErrors { get; set; }       //73
        public double ThermalControlValve { get; set; }   //74
    }

    public partial class AmmoniaControllerReading {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public int AmmoniaControllerId { get; set; }
        public AmmoniaController AmmoniaController { get; set; }

        public AmmoniaControllerAlert AmmoniaControllerAlert { get; set; }

        public AmmoniaControllerReading() { }

        public AmmoniaControllerReading(DateTime timestamp, AmmoniaController device) {
            this.TimeStamp = timestamp;
        }

        public int Tank1Weight { get; set; }
        public int Tank2Weight { get; set; }
        public int Tank3Weight { get; set; }
        public int Tank4Weight { get; set; }

        public int Tank1ZeroCal { get; set; }
        public int Tank1NonZeroCal { get; set; }
        public int Tank1Zero { get; set; }
        public int Tank1NonZero { get; set; }
        public int Tank1Total { get; set; }
        public int Tank1Gas { get; set; }

        public int Tank2ZeroCal { get; set; }
        public int Tank2NonZeroCal { get; set; }
        public int Tank2Zero { get; set; }
        public int Tank2NonZero { get; set; }
        public int Tank2Total { get; set; }
        public int Tank2Gas { get; set; }

        public int Tank3ZeroCal { get; set; }
        public int Tank3NonZeroCal { get; set; }
        public int Tank3Zero { get; set; }
        public int Tank3NonZero { get; set; }
        public int Tank3Total { get; set; }
        public int Tank3Gas { get; set; }

        public int Tank4ZeroCal { get; set; }
        public int Tank4NonZeroCal { get; set; }
        public int Tank4Zero { get; set; }
        public int Tank4NonZero { get; set; }
        public int Tank4Total { get; set; }
        public int Tank4Gas { get; set; }

        public int Tank1Tare { get; set; }
        public int Tank2Tare { get; set; }
        public int Tank3Tare { get; set; }
        public int Tank4Tare { get; set; }

        public double Tank1Temperature { get; set; }
        public double Tank2Temperature { get; set; }
        public double Tank3Temperature { get; set; }
        public double Tank4Temperature { get; set; }

        public int Heater1DutyCycle { get; set; }
        public int Heater2DutyCycle { get; set; }
        public int Heater3DutyCycle { get; set; }
        public int Heater4DutyCycle { get; set; }

        public bool Tank1Warning { get; set; }
        public bool Tank2Warning { get; set; }
        public bool Tank3Warning { get; set; }
        public bool Tank4Warning { get; set; }

        public bool Tank1Alarm { get; set; }
        public bool Tank2Alarm { get; set; }
        public bool Tank3Alarm { get; set; }
        public bool Tank4Alarm { get; set; }

        public void Set(List<int> regData, bool[] coilData) {
            for (int i = 0; i < 4; i++) {
                switch (i) {
                    case 0:
                        this.Tank1Weight = regData[i];
                        var tankData = regData.GetRange(4, 6).ToArray();
                        this.Tank1ZeroCal = tankData[0];
                        this.Tank1NonZeroCal = tankData[1];
                        this.Tank1Zero = tankData[2];
                        this.Tank1NonZero = tankData[3];
                        this.Tank1Total = tankData[4];
                        this.Tank1Gas = tankData[5];
                        this.Tank1Tare = regData[28];
                        this.Tank1Temperature = regData[32];
                        this.Heater1DutyCycle = regData[38];
                        this.Tank1Warning = coilData[2];
                        this.Tank1Alarm = coilData[6];
                        break;

                    case 1:
                        this.Tank2Weight = regData[i];
                        var tank2Data = regData.GetRange(10, 6).ToArray();
                        this.Tank2ZeroCal = tank2Data[0];
                        this.Tank2NonZeroCal = tank2Data[1];
                        this.Tank2Zero = tank2Data[2];
                        this.Tank2NonZero = tank2Data[3];
                        this.Tank2Total = tank2Data[4];
                        this.Tank2Gas = tank2Data[5];
                        this.Tank2Tare = regData[29];
                        this.Tank2Temperature = regData[33];
                        this.Heater2DutyCycle = regData[39];
                        this.Tank2Warning = coilData[3];
                        this.Tank2Alarm = coilData[7];
                        break;

                    case 2:
                        this.Tank3Weight = regData[i];
                        var tank3Data = regData.GetRange(16, 6).ToArray();
                        this.Tank3ZeroCal = tank3Data[0];
                        this.Tank3NonZeroCal = tank3Data[1];
                        this.Tank3Zero = tank3Data[2];
                        this.Tank3NonZero = tank3Data[3];
                        this.Tank3Total = tank3Data[4];
                        this.Tank3Gas = tank3Data[5];
                        this.Tank3Tare = regData[30];
                        this.Tank3Temperature = regData[34];
                        this.Heater3DutyCycle = regData[40];
                        this.Tank3Warning = coilData[4];
                        this.Tank3Alarm = coilData[8];
                        break;

                    case 3:
                        this.Tank4Weight = regData[i];
                        var tank4Data = regData.GetRange(22, 6).ToArray();
                        this.Tank4ZeroCal = tank4Data[0];
                        this.Tank4NonZeroCal = tank4Data[1];
                        this.Tank4Zero = tank4Data[2];
                        this.Tank4NonZero = tank4Data[3];
                        this.Tank4Total = tank4Data[4];
                        this.Tank4Gas = tank4Data[5];
                        this.Tank4Tare = regData[31];
                        this.Tank4Temperature = regData[35];
                        this.Heater4DutyCycle = regData[41];
                        this.Tank4Warning = coilData[5];
                        this.Tank4Alarm = coilData[9];
                        break;
                }
            }
        }

        public AmmoniaCalibrationData GetTankCalibration(int tank) {
            switch (tank) {
                case 1: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank1ZeroCal;
                    cal.CalNonZero = this.Tank1NonZeroCal;
                    cal.ActualZero = this.Tank1Zero;
                    cal.ActualNonZero = this.Tank1NonZero;
                    cal.GasWeight = this.Tank1Gas;
                    cal.TotalWeight = this.Tank1Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                case 2: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank2ZeroCal;
                    cal.CalNonZero = this.Tank2NonZeroCal;
                    cal.ActualZero = this.Tank2Zero;
                    cal.ActualNonZero = this.Tank2NonZero;
                    cal.GasWeight = this.Tank2Gas;
                    cal.TotalWeight = this.Tank2Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                case 3: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank3ZeroCal;
                    cal.CalNonZero = this.Tank3NonZeroCal;
                    cal.ActualZero = this.Tank3Zero;
                    cal.ActualNonZero = this.Tank3NonZero;
                    cal.GasWeight = this.Tank3Gas;
                    cal.TotalWeight = this.Tank3Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                case 4: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank4ZeroCal;
                    cal.CalNonZero = this.Tank4NonZeroCal;
                    cal.ActualZero = this.Tank4Zero;
                    cal.ActualNonZero = this.Tank4NonZero;
                    cal.GasWeight = this.Tank4Gas;
                    cal.TotalWeight = this.Tank4Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                default: return null;
            }
        }
    }
}

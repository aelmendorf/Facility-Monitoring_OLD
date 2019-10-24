using System;

namespace FacilityMonitoring.Common.Data.Entities {
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
}

﻿
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {

    public enum AnalogSensorType {
        H2,O2,NH3,N,NONE
    }

    public partial class ModbusDevice {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int SlaveAddress { get; set; }
        public string Status { get; set; }

        public ICollection<Reading> Readings { get; set; }

        public ModbusDevice() {
            this.Readings = new ObservableHashSet<Reading>();
        }

        public ModbusDevice(string identifier, string displayName,
            string ipAddress, int port, int slaveAddress, string status) {
            this.Identifier = identifier;
            this.DisplayName = displayName;
            this.IpAddress = ipAddress;
            this.Port = port;
            this.SlaveAddress = slaveAddress;
            this.Status = status;
        }
    }

    public partial class GenericMonitorBox : ModbusDevice {
        public int AnalogChannelId { get; set; }
        public virtual AnalogCalibration AnalogCalibration { get; set; }

        public int AnalogMappingId { get; set; }
        public virtual AnalogMapping AnalogMapping { get; set; }
    }

    public partial class AnalogMapping {
        public int Id { get; set; }

        public int GenericMonitorBoxId { get; set; }
        public virtual GenericMonitorBox GenericMonitorBox { get; set; }

        public bool Chan1Connected { get; set; }
        public string Chan1Name { get; set; }
        public AnalogSensorType Chan1Type { get; set; }

        public bool Chan2Connected { get; set; }
        public string Chan2Name { get; set; }
        public AnalogSensorType Chan2Type { get; set; }

        public bool Chan3Connected { get; set; }
        public string Chan3Name { get; set; }
        public AnalogSensorType Chan3Type { get; set; }

        public bool Chan4Connected { get; set; }
        public string Chan4Name { get; set; }
        public AnalogSensorType Chan4Type { get; set; }

        public bool Chan5Connected { get; set; }
        public string Chan5Name { get; set; }
        public AnalogSensorType Chan5Type { get; set; }

        public bool Chan6Connected { get; set; }
        public string Chan6Name { get; set; }
        public AnalogSensorType Chan6Type { get; set; }

        public bool Chan7Connected { get; set; }
        public string Chan7Name { get; set; }
        public AnalogSensorType Chan7Type { get; set; }

        public bool Chan8Connected { get; set; }
        public string Chan8Name { get; set; }
        public AnalogSensorType Chan8Type { get; set; }

        public bool Chan9Connected { get; set; }
        public string Chan9Name { get; set; }
        public AnalogSensorType Chan9Type { get; set; }

        public bool Chan10Connected { get; set; }
        public string Chan10Name { get; set; }
        public AnalogSensorType Chan10Type { get; set; }

        public bool Chan11Connected { get; set; }
        public string Chan11Name { get; set; }
        public AnalogSensorType Chan11Type { get; set; }

        public bool Chan12Connected { get; set; }
        public string Chan12Name { get; set; }
        public AnalogSensorType Chan12Type { get; set; }

        public bool Chan13Connected { get; set; }
        public string Chan13Name { get; set; }
        public AnalogSensorType Chan13Type { get; set; }

        public bool Chan14Connected { get; set; }
        public string Chan14Name { get; set; }
        public AnalogSensorType Chan14Type { get; set; }

        public bool Chan15Connected { get; set; }
        public string Chan15Name { get; set; }
        public AnalogSensorType Chan15Type { get; set; }

        public bool Chan16Connected { get; set; }
        public string Chan16Name { get; set; }
        public AnalogSensorType Chan16Type { get; set; }
    }

    public partial class DigitalMapping {

    }

    public partial class AnalogCalibration {
        public int Id { get; set; }
        public int GenericMonitorBoxId { get; set; }
        public virtual GenericMonitorBox GenericMonitorBox { get; set; }

        public float Chan1Slope { get; set; }
        public float Chan1Offset { get; set; }
        public float Chan1Resistance { get; set; }

        public float Chan2Slope { get; set; }
        public float Chan2Offset { get; set; }
        public float Chan2Resistance { get; set; }

        public float Chan3Slope { get; set; }
        public float Chan3Offset { get; set; }
        public float Chan3Resistance { get; set; }

        public float Chan4Slope { get; set; }
        public float Chan4Offset { get; set; }
        public float Chan4Resistance { get; set; }

        public float Chan5Slope { get; set; }
        public float Chan5Offset { get; set; }
        public float Chan5Resistance { get; set; }

        public float Chan6Slope { get; set; }
        public float Chan6Offset { get; set; }
        public float Chan6Resistance { get; set; }

        public float Chan7Slope { get; set; }
        public float Chan7Offset { get; set; }
        public float Chan7Resistance { get; set; }

        public float Chan8Slope { get; set; }
        public float Chan8Offset { get; set; }
        public float Chan8Resistance { get; set; }

        public float Chan9Slope { get; set; }
        public float Chan9Offset { get; set; }
        public float Chan9Resistance { get; set; }

        public float Chan10Slope { get; set; }
        public float Chan10Offset { get; set; }
        public float Chan10Resistance { get; set; }

        public float Chan11Slope { get; set; }
        public float Chan11Offset { get; set; }
        public float Chan11Resistance { get; set; }

        public float Chan12Slope { get; set; }
        public float Chan12Offset { get; set; }
        public float Chan12Resistance { get; set; }

        public float Chan13Slope { get; set; }
        public float Chan13Offset { get; set; }
        public float Chan13Resistance { get; set; }

        public float Chan14Slope { get; set; }
        public float Chan14Offset { get; set; }
        public float Chan14Resistance { get; set; }

        public float Chan15Slope { get; set; }
        public float Chan15Offset { get; set; }
        public float Chan15Resistance { get; set; }

        public float Chan16Slope { get; set; }
        public float Chan16Offset { get; set; }
        public float Chan16Resistance { get; set; }
    }

    public abstract class Reading {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Identifier { get; set; }
        public int ModbusDeviceId { get; set; }
        public ModbusDevice ModbusDevice { get; set; }
    }

    public partial class GenericBoxReading : Reading {
        public GenericBoxReading() { }

        public GenericBoxReading(DateTime timestamp, string identifier, ModbusDevice device) {
            this.TimeStamp = timestamp;
            this.Identifier = identifier;
            this.ModbusDevice = device;
        }
        
        public float AnalogCh1 { get; set; }
        public float AnalogCh2 { get; set; }
        public float AnalogCh3 { get; set; }
        public float AnalogCh4 { get; set; }
        public float AnalogCh5 { get; set; }
        public float AnalogCh6 { get; set; }
        public float AnalogCh7 { get; set; }
        public float AnalogCh8 { get; set; }
        public float AnalogCh9 { get; set; }
        public float AnalogCh10 { get; set; }
        public float AnalogCh11 { get; set; }
        public float AnalogCh12 { get; set; }
        public float AnalogCh13 { get; set; }
        public float AnalogCh14 { get; set; }
        public float AnalogCh15 { get; set; }
        public float AnalogCh16 { get; set; }
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
    }

    public partial class H2GenReading:Reading {

        public H2GenReading() { }

        public H2GenReading(DateTime timestamp,string identifier,ModbusDevice device) {
            this.TimeStamp = timestamp;
            this.Identifier = identifier;
            this.ModbusDevice = device;
        }

        public bool A200_Level_Flooded { get; set; }    //1
        public bool A200_Level_High { get; set; }       //2
        public bool A200_Level_Low { get; set; }        //3
        public bool A300_Level_Flooded { get; set; }    //4
        public bool A300_Level_High { get; set; }       //5
        public bool A300_Level_Low { get; set; }        //6
        public bool A200_Level_Empty { get; set; }      //7
        public bool A300_Level_Empty { get; set; }      //8
        public bool Stack_A_Water_Flow { get; set; }    //9
        public bool Stack_B_Water_Flow { get; set; }    //10
        public bool Stack_C_Water_Flow { get; set; }    //11
        public bool Vent_Valve { get; set; }            //12
        public bool A200_Drain_Valve { get; set; }      //13
        public bool A200_Inlet_Valve { get; set; }      //14
        public bool Dryer_Valve1 { get; set; }          //15
        public bool Dryer_Valve2 { get; set; }          //16
        public bool Dryer_Valve3 { get; set; }          //17
        public bool Dryer_Valve4 { get; set; }          //18
        public bool A300_Drain_Valve { get; set; }      //19
        public bool Stack_A_valve { get; set; }         //20
        public bool Stack_B_valve { get; set; }         //21
        public bool Stack_C_valve { get; set; }         //22
        public bool Pump_control { get; set; }          //23

        public int PSV_A1 { get; set; }                 //24
        public int PSV_A1_A2 { get; set; }              //25
        public int PSV_B1 { get; set; }                 //26
        public int PSV_B2 { get; set; }                 //27
        public int PSV_C1 { get; set; }                 //28
        public int PSV_C2 { get; set; }                 //29
        public int Stack_A_monitor_Current { get; set; }//30
        public int Stack_B_monitor_Current { get; set; }//31
        public int Stack_C_monitor_Current { get; set; }//32
        public int PS_Fault_A1 { get; set; }            //33
        public int PS_Fault_A2 { get; set; }            //34
        public int PS_Fault_B1 { get; set; }            //35
        public int PS_Fault_B2 { get; set; }            //36
        public int PS_Fault_C1 { get; set; }            //37
        public int PS_Fault_C2 { get; set; }            //38
        public int ps_unit_status { get; set; }         //39
        public int Ps_card_enable_A1 { get; set; }      //40
        public int Ps_card_enable_A2 { get; set; }      //41
        public int Ps_card_enable_B1 { get; set; }      //42
        public int Ps_card_enable_B2 { get; set; }      //43
        public int Ps_card_enable_C1 { get; set; }      //44       
        public int Ps_card_enable_C2 { get; set; }      //45
        public int PSV_A3 { get; set; }                 //46
        public int PSV_B3 { get; set; }                 //47
        public int PSV_C3 { get; set; }                 //48
        public int PS_Fault_A3 { get; set; }            //49
        public int PS_Fault_B3 { get; set; }            //50
        public int PS_Fault_C3 { get; set; }            //51
        public int Ps_card_enable_A3 { get; set; }      //52
        public int Ps_card_enable_B3 { get; set; }      //53
        public int Ps_card_enable_C3 { get; set; }      //54
        public int System_mode { get; set; }            //55
        public int Operation_Mode { get; set; }         //56
        public int System_State { get; set; }           //57

        public double Water_Quality { get; set; }       //58
        public double Water_Temperature { get; set; }   //59
        public double System_Pressure { get; set; }     //60
        public double Product_pressure { get; set; }    //61
        public double CG220_level { get; set; }         //62
        public double Heat_Exchanger_Water_Temp { get; set; }//63
        public double System_24V_power { get; set; }        //64
        public double System_5V_power { get; set; }         //65
        public double System_3V_power { get; set; }         //66
        public double Heat_sink_Temperature { get; set; }   //67
        public double Ambient_Temperature { get; set; }     //68
        public double DI_water_quality { get; set; }        //69
        public double Hydrogen_flow { get; set; }           //70
        public double Ext_Water_Quality { get; set; }       //71
        public string All_System_Warnings { get; set; }     //72
        public string All_System_Errors { get; set; }       //73
        public double Thermal_Control_Valve { get; set; }   //74

    }

    public partial class AmmoniaBoxReading:Reading {

        public AmmoniaBoxReading() { }

        public AmmoniaBoxReading(DateTime timestamp, string identifier, ModbusDevice device) {
            this.TimeStamp = timestamp;
            this.Identifier = identifier;
            this.ModbusDevice = device;
        }

        public string Scale1Status { get; set; }
        public string Scale2Status { get; set; }
        public string Scale3Status { get; set; }
        public string Scale4Status { get; set; }
        public string ActiveTank { get; set; }

        public int Tank1Weight { get; set; }
        public int Tank2Weight { get; set; }
        public int Tank3Weight { get; set; }
        public int Tank4Weight { get; set; }

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
    }
}

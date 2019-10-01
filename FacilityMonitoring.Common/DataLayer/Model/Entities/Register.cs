using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FacilityMonitoring.Common.Model {

    //Inputs
    //High: Logic High=Tripped
    //Low:  Logic Low=Tripped

    //Outputs
    // High: Logic High=Turn on
    // Low:  Logic Low=Turn on

    public enum FunctionCode {
        ReadCoil = 1,
        ReadDiscreteInput,
        ReadHoldingRegisters,
        ReadInputRegisters,
        WriteSingleCoil,
        WriteSingleHoldingRegister,
        WriteMultipleCoils,
        WriteMultipleHoldingRegisters
    }

    public enum WaterLevel {
        WET=1,
        DRY=0
    }

    public enum WaterFlow {
        FLOW=1,
        NOFLOW=0
    }

    public enum FaultState {
        FAULT=1,
        GOOD=0
    }

    public enum ONOFF {
        ON=1,
        OFF=0
    }

    public enum EnableState {
        ENABLE=1,
        OFF=0
    }

    public enum SystemMode {
        Normal=0,
        Service,
        Installation,
        Maintenance
    }

    public enum SystemError {
        E01_A1 = 0,
        E01_A2 = 1,
        E01_A3 = 2,
        E01_B1 = 3,
        E01_B2 = 4,
        E01_B3 = 5,
        E01_C1 = 6,
        E01_C2 = 7,
        E01_C3 = 8,
        E02_A1 = 9,
        E02_A2 = 10,
        E02_A3 = 11,
        E02_B1 = 12,
        E02_B2 = 13,
        E02_B3 = 14,
        E02_C1 = 15,
        E02_C2 = 16,
        E02_C3 = 17,
        E03_A = 18,
        E03_B = 19,
        E03_C = 20,
        E04_A = 21,
        E04_B = 22,
        E04_C = 23,
        E05_A1 = 24,
        E05_A2 = 25,
        E05_A3 = 26,
        E05_B1 = 27,
        E05_B2 = 28,
        E05_B3 = 29,
        E05_C1 = 30,
        E05_C2 = 31,
        E05_C3 = 32,
        E06 = 33,
        E07 = 34,
        E08 = 35,
        E09 = 36,
        E10 = 37,
        E11 = 38,
        E12 = 39,
        E13 = 40,
        E14 = 41,
        E15 = 42,
        E16_A = 43,
        E16_B = 44,
        E17 = 45,
        E18 = 46,
        E19 = 47,
        E20_A = 48,
        E20_B = 49,
        E21 = 50,
        E22 = 51,
        E23 = 52,
        E24 = 53,
        E25 = 54,
        E26 = 55,
        E27 = 56,
        E28 = 57,
        E29 = 58,
        E30 = 59,
        E31 = 60,
        E32 = 61,
        E33 = 62,
        E34 = 63,
        E35 = 64,
        E36_A = 65,
        E36_B = 66,
        E36_C = 67,
        E37 = 68,
        E38 = 69,
        E39 = 70,
        E40 = 71,
        E41 = 72,
        E42 = 73,
        E43 = 74,
        E44 = 75,
        E45 = 76,
        E46 = 77,
        E47 = 78,
        E48 = 79
    }

    public enum SystemWarning {
        W01 = 0,
        W02 = 1,
        W03 = 2,
        W04 = 3,
        W05 = 4,
        W06 = 5,
        W07 = 6,
        W08 = 7,
        W09 = 8,
        W10 = 9,
        W11 = 10,
        W12 = 11,
        W13 = 12,
        W14 = 13,
        W15 = 14,
        W16 = 15,
        W17 = 16,
        W18 = 17,
        W19 = 18,
        W20 = 19,
        W21 = 20,
        W22 = 21
    }

    public enum WarningErrorKey {
        NOT_OCCURED=0,
        OCCURED=1,
        OCCURED_CLEARED=2
    }

    public enum OperationMode {
        M01=0,
        M02=1,
        M09=8,
        M11=10,
        M12=11,
        M13=12,
        M14=13,
        M15=14,
        M16=15,
        NoModeYet=18,
        NoNormalMode=19
    }

    public enum SystemState {
        POWERUP_STATE=0,
        PRESTART_STATE=1,
        START_STATE=2,
        GENERATE_VENT_STATE=3,
        PRESSURIZED_SYSTEM_STATE=4,
        DEGRADED_CG_STATE=5,
        DEGRADED_VOLT_CUR_STATE=6,
        DEGRADED_TEMP_STATE=7,
        DEGRADED_PSU_STATE=8,
        IDLE_TANK_TOPPING_STATE=9,
        ERROR_STATE=11,
        IDLE_SERVICE_STATE=12,
        IDLE_STATE=13,
        STOP_STATE=14,
        CG_CAL_START_STATE=19,
        CG_CAL_STOP_STATE=20,
        CG_CAL_CANCEL_STATE=21,
        CALIBRATE_CG_SENSOR=23,
        STANDBY_STATE=27,
        IDLE_LOAD_FOLLOW_STATE=28,
        NO_STATE_YET=30
    }





    public enum H2Type {
        WATERLEVEL,
        WATERFLOW,
        ONOFF,
        INT32,
        FAULTSTATE,
        ENABLESTATE,
        SYSTEMMODE,
        OPERATIONMODE,
        SYSTEMSTATE,
        DOUBLE,
        GENERATORSYSTEMWARNING,
        GENERATORSYSTEMERROR
    }

    public enum LogicType { HIGH=0, LOW }
    public enum AlertAction { ALARM,WARN,SOFTWARN,MAINTENANCE,NOTHING }
    public enum OutputControl { HARDWARE,SOFTWARE}

    public abstract class Register {

        public int Id { get; set; }
        public string Name { get; set; }
        public int RegisterIndex { get; set; }
        public int RegisterLength { get; set; } //Added
        public bool Connected { get; set; }
        public bool Bypass { get; set; }
        public string PropertyMap { get; set; }
        public LogicType Logic { get; set; }
        public bool Display { get; set; }

        public int DeviceId { get; set; }
        public virtual ModbusDevice Device { get; set; }

        public int? SensorTypeId { get; set; }
        public virtual SensorType SensorType { get; set; }

    }

    public partial class AnalogChannel:Register{

        public double Slope { get; set; }
        public double Offset { get; set; }
        public double Resistance { get; set; }

        public double ZeroValue { get; set; }
        public double MaxValue { get; set; }

        public double Alarm1SetPoint { get; set; }
        public AlertAction Alarm1Action { get; set; }
        public bool Alarm1Enabled { get; set; }

        public double Alarm2SetPoint { get; set; }
        public AlertAction Alarm2Action { get; set; }
        public bool Alarm2Enabled { get; set; }

        public double Alarm3SetPoint { get; set; }
        public AlertAction Alarm3Action { get; set; }
        public bool Alarm3Enabled { get; set; }

        public double ValueDivisor { get; set; }

        public AnalogChannel(string name, int chnum,int regLength, bool connected, string pName){
            this.Name = name;
            this.RegisterIndex = chnum;
            this.RegisterLength = regLength;
            this.Connected = connected;
            this.PropertyMap = pName;
            this.Bypass = false;
            this.Logic = LogicType.HIGH;
        }

        public AnalogChannel() {

        }

        public Tuple<double,double> GetEquationParameters() {
            if (this.SensorType != null) {
                double slope = (this.SensorType.MaxPoint - this.SensorType.ZeroPoint) / (this.MaxValue - this.ZeroValue);
                double offset = (slope * (-1 * this.ZeroValue)) + this.SensorType.ZeroPoint;
                return new Tuple<double, double>(slope, offset);
            } else {
                return null;
            }
        }
    }

    public partial class DigitalInputChannel:Register  {

        public AlertAction AlarmAction { get; set; }

        public DigitalInputChannel(string name, int regNum,int regLength, bool connected, string pname, LogicType ltype) {
            this.Logic = ltype;
            this.Name = name;
            this.RegisterIndex = regNum;
            this.RegisterLength = regLength;
            this.Connected = connected;
            this.PropertyMap = pname;
            this.Bypass = false;
            this.AlarmAction = AlertAction.NOTHING;
        }

        public DigitalInputChannel() {

        }
    }

    public partial class DigitalOutputChannel:Register {

        public OutputControl OutputControl { get; set; }

        public DigitalOutputChannel(string name, int regIndex,int regLength, bool connected, string pname, LogicType ltype) {
            this.Logic = ltype;
            this.Name = name;
            this.RegisterIndex = regIndex;
            this.RegisterLength = regLength;
            this.Connected = connected;
            this.PropertyMap = pname;
            this.Bypass = false;
            this.OutputControl = OutputControl.SOFTWARE;
        }

        public DigitalOutputChannel() {

        }
    }

    public partial class GeneratorRegister:Register {
        public FunctionCode FunctionCode { get; set; }
        public H2Type DataType { get; set; }
    }

    //public partial class AmmoniaRegister:Register {
    //    public double Alarm1SetPoint { get; set; }
    //    public AlertAction Alarm1Action { get; set; }
    //    public bool Alarm1Enabled { get; set; }

    //    public double Alarm2SetPoint { get; set; }
    //    public AlertAction Alarm2Action { get; set; }
    //    public bool Alarm2Enabled { get; set; }

    //    public double ValueDivisor { get; set; }

    //    public AmmoniaRegister(string name, int chnum, int regLength, bool connected, string pName) {
    //        this.Name = name;
    //        this.RegisterIndex = chnum;
    //        this.RegisterLength = regLength;
    //        this.Connected = connected;
    //        this.PropertyMap = pName;
    //        this.Bypass = false;
    //        this.Logic = LogicType.HIGH;
    //    }

    //    public AmmoniaRegister() {

    //    }
    //}
}

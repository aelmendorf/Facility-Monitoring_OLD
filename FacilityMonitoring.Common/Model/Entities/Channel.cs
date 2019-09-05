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

    public enum LogicType { HIGH, LOW }
    public enum AlertAction { ALARM,WARN,SOFTWARN,MAINTENANCE,NOTHING }
    public enum OutputControl { HARDWARE,SOFTWARE}

    public abstract class Channel {

        public int Id { get; set; }
        public string Name { get; set; }
        public int ChannelNumber { get; set; }
        public bool Connected { get; set; }
        public bool Bypass { get; set; }
        public string PropertyMap { get; set; }
        public LogicType Logic { get; set; }

        public int GenericMonitorBoxId { get; set; }
        public virtual GenericMonitorBox GenericMonitorBox { get; set; }

    }

    public partial class AnalogChannel:Channel{
        public int? SensorTypeId { get; set; }
        public virtual SensorType SensorType { get; set; }

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

        public AnalogChannel(string name, int chnum, bool connected, string pName){
            this.Name = name;
            this.ChannelNumber = chnum;
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

    public partial class DigitalInputChannel:Channel  {

        public AlertAction AlarmAction { get; set; }

        public DigitalInputChannel(string name, int chnum, bool connected, string pname, LogicType ltype) {
            this.Logic = ltype;
            this.Name = name;
            this.ChannelNumber = chnum;
            this.Connected = connected;
            this.PropertyMap = pname;
            this.Bypass = false;
            this.AlarmAction = AlertAction.NOTHING;
        }

        public DigitalInputChannel() {

        }
    }

    public partial class DigitalOutputChannel:Channel {
        public OutputControl OutputControl { get; set; }

        public DigitalOutputChannel(string name, int chnum, bool connected, string pname, LogicType ltype) {
            this.Logic = ltype;
            this.Name = name;
            this.ChannelNumber = chnum;
            this.Connected = connected;
            this.PropertyMap = pname;
            this.Bypass = false;
            this.OutputControl = OutputControl.SOFTWARE;
        }

        public DigitalOutputChannel() {

        }
    }
}

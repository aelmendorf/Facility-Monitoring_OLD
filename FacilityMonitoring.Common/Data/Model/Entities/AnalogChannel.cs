using System;

namespace FacilityMonitoring.Common.Data.Entities {
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
}

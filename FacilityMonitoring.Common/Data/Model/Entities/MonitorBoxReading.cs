using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Data.Entities {
    public partial class MonitorBoxReading {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Identifier { get; set; }
        public int GenericMonitorBoxId { get; set; }
        public MonitorBox GenericMonitorBox { get; set; }

        public MonitorBoxReading() { }

        public MonitorBoxReading(DateTime timestamp, string identifier, MonitorBox device) {
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

        public MonitorBoxAlert MonitorBoxAlert { get; set; }

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
}

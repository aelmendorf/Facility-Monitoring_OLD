using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Data.Entities {
    public enum AnalogAlert { ALARM1, ALARM2, ALARM3, NONE }   
    public enum GenericAlert { WARNING=1, ALARM=2, NONE=0 }

    public partial class MonitorBoxAlert {
        public int Id { get; set; }
        public int MonitorBoxReadingId { get; set; }
        public MonitorBoxReading GenericMonitorBoxReading { get; set; }
       
        public AnalogAlert AnalogCh1 { get; set; }
        public AnalogAlert AnalogCh2 { get; set; }
        public AnalogAlert AnalogCh3 { get; set; }
        public AnalogAlert AnalogCh4 { get; set; }
        public AnalogAlert AnalogCh5 { get; set; }
        public AnalogAlert AnalogCh6 { get; set; }
        public AnalogAlert AnalogCh7 { get; set; }
        public AnalogAlert AnalogCh8 { get; set; }
        public AnalogAlert AnalogCh9 { get; set; }
        public AnalogAlert AnalogCh10 { get; set; }
        public AnalogAlert AnalogCh11 { get; set; }
        public AnalogAlert AnalogCh12 { get; set; }
        public AnalogAlert AnalogCh13 { get; set; }
        public AnalogAlert AnalogCh14 { get; set; }
        public AnalogAlert AnalogCh15 { get; set; }
        public AnalogAlert AnalogCh16 { get; set; }

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

        [NotMapped]
        public object this[string propertyName] {
            set {
                switch (propertyName) {
                    case "AnalogCh1":
                        AnalogCh1 = (AnalogAlert)value;
                        break;
                    case "AnalogCh2":
                        AnalogCh2 = (AnalogAlert)value;
                        break;
                    case "AnalogCh3":
                        AnalogCh3 = (AnalogAlert)value;
                        break;
                    case "AnalogCh4":
                        AnalogCh4 = (AnalogAlert)value;
                        break;
                    case "AnalogCh5":
                        AnalogCh5 = (AnalogAlert)value;
                        break;
                    case "AnalogCh6":
                        AnalogCh6 = (AnalogAlert)value;
                        break;
                    case "AnalogCh7":
                        AnalogCh7 = (AnalogAlert)value;
                        break;
                    case "AnalogCh8":
                        AnalogCh8 = (AnalogAlert)value;
                        break;
                    case "AnalogCh9":
                        AnalogCh9 = (AnalogAlert)value;
                        break;
                    case "AnalogCh10":
                        AnalogCh10 = (AnalogAlert)value;
                        break;
                    case "AnalogCh11":
                        AnalogCh11 = (AnalogAlert)value;
                        break;
                    case "AnalogCh12":
                        AnalogCh12 = (AnalogAlert)value;
                        break;
                    case "AnalogCh13":
                        AnalogCh13 = (AnalogAlert)value;
                        break;
                    case "AnalogCh14":
                        AnalogCh14 = (AnalogAlert)value;
                        break;
                    case "AnalogCh15":
                        AnalogCh15 = (AnalogAlert)value;
                        break;
                    case "AnalogCh16":
                        AnalogCh16 = (AnalogAlert)value;
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
                }
            }
            get {
                switch (propertyName) {
                    case "AnalogCh1":
                        return this.AnalogCh1;
                    case "AnalogCh2":
                        return this.AnalogCh2;
                    case "AnalogCh3":
                        return this.AnalogCh3;
                    case "AnalogCh4":
                        return this.AnalogCh4;
                    case "AnalogCh5":
                        return this.AnalogCh5;
                    case "AnalogCh6":
                        return this.AnalogCh6;
                    case "AnalogCh7":
                        return this.AnalogCh7;
                    case "AnalogCh8":
                        return this.AnalogCh8;
                    case "AnalogCh9":
                        return this.AnalogCh9;
                    case "AnalogCh10":
                        return this.AnalogCh10;
                    case "AnalogCh11":
                        return this.AnalogCh11;
                    case "AnalogCh12":
                        return this.AnalogCh12;
                    case "AnalogCh13":
                        return this.AnalogCh13;
                    case "AnalogCh14":
                        return this.AnalogCh14;
                    case "AnalogCh15":
                        return this.AnalogCh15;
                    case "AnalogCh16":
                        return this.AnalogCh16;
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
                    default: return null;
                }
            }
        }
    }
}

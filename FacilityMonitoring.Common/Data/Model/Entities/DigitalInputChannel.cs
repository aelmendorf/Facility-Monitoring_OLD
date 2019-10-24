namespace FacilityMonitoring.Common.Data.Entities {
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

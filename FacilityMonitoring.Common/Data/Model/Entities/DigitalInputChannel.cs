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
}

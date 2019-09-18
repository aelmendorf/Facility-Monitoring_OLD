using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Data {
    public class AmmoniaDataView {

    }

    public class AmmoniaTankView {
        public string Tank { get; set; }
        public string Status { get; set; }
        public decimal Weight { get; set; }
        public decimal Temperature { get; set; }
        public string Alarm { get; set; }
        public string Warning { get; set; }

        public AmmoniaTankView(string tank, string status, decimal weight, decimal temperature, bool alarm, bool warning) {
            this.Tank = tank;
            this.Status = status;
            this.Weight = weight;
            this.Temperature = temperature;
            this.Alarm = (alarm) ? "Ammonia Is Below Limit":"Okay";
            this.Warning = (warning) ? "Ammonia Is Getting Low" : "Okay";
        }
    }
}

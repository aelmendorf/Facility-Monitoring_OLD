using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.DataLayer {
    public class Tank {
        public string Identifier { get; set; }
        public DeviceState State { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double DutyCycle { get; set; }
    }
}

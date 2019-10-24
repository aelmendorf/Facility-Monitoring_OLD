using FacilityMonitoring.Common.Data.Entities;

namespace FacilityMonitoring.Common.Data.DTO {
    public class Tank {
        public string Identifier { get; set; }
        public DeviceState State { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double DutyCycle { get; set; }
    }
}

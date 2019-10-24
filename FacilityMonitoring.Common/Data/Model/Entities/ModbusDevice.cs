using System.Collections.Generic;

namespace FacilityMonitoring.Common.Data.Entities {
    public enum DeviceState { OKAY, WARNING, ALARM, MAINTENCE }

    public abstract class ModbusDevice {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int SlaveAddress { get; set; }
        public DeviceState State { get; set; }
        public string Status { get; set; }
        public bool BypassAll { get; set; }
        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ICollection<Register> Registers { get; set; }
    }
}

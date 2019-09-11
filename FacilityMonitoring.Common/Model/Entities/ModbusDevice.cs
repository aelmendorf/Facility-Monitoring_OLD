
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {
    public enum DeviceState { OKAY, WARNING, ALARM, MAINTENCE }

    public partial class ModbusDevice {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int SlaveAddress { get; set; }
        public DeviceState State { get; set; }
        public string Status { get; set; }
        public bool BypassAll { get; set; }

        public ICollection<Reading> Readings { get; set; }

        public ModbusDevice() {
            this.Readings = new ObservableHashSet<Reading>();
        }

        public ModbusDevice(string identifier, string displayName,
            string ipAddress, int port, int slaveAddress, string status) {
            this.Identifier = identifier;
            this.DisplayName = displayName;
            this.IpAddress = ipAddress;
            this.Port = port;
            this.SlaveAddress = slaveAddress;
            this.Status = status;
            this.BypassAll = false;
            this.State = DeviceState.OKAY;
        }
    }

    public partial class GenericMonitorBox : ModbusDevice {
        public int AnalogChannelCount { get; set; }
        public int DigitalInputChannelCount { get; set; }
        public int DigitalOutputChannelCount { get; set; }

        public int ModbusComAddr { get; set; }
        public int SoftwareMaintAddr { get; set; }
        public int WarningAddr { get; set; }
        public int AlarmAddr { get; set; }

        public int StateAddr { get; set; }

        public ICollection<Register> Registers { get; set; }

        public GenericMonitorBox() {
            this.Registers = new ObservableHashSet<Register>();
        }
    }

}

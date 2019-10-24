using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Data.Entities {
    public partial class MonitorBox : ModbusDevice {
        public int AnalogChannelCount { get; set; }
        public int DigitalInputChannelCount { get; set; }
        public int DigitalOutputChannelCount { get; set; }

        public int ModbusComAddr { get; set; }
        public int SoftwareMaintAddr { get; set; }
        public int WarningAddr { get; set; }
        public int AlarmAddr { get; set; }

        public int StateAddr { get; set; }

        public ICollection<MonitorBoxReading> BoxReadings { get; set; }

        public MonitorBox() {
            this.Registers = new ObservableHashSet<Register>();
            this.BoxReadings = new ObservableHashSet<MonitorBoxReading>();
        }

        public MonitorBox(string identifier, string displayName,
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

        [NotMapped]
        public MonitorBoxReading LastRead { get; set; }
    }
}

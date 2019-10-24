using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Data.Entities {
    public partial class H2Generator : ModbusDevice {
        public ICollection<H2GenReading> H2Readings { get; set; }
        public OperationMode OperationMode { get; set; }
        public SystemState SystemState { get; set;  }

        public H2Generator() {
            this.Registers = new ObservableHashSet<Register>();
            this.H2Readings = new ObservableHashSet<H2GenReading>();
        }

        public H2Generator(string identifier, string displayName,
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
        public H2GenReading LastRead { get; set; }
    }
}

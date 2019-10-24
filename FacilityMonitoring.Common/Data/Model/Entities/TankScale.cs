using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Data.Entities {
    public partial class TankScale : ModbusDevice {

        public int ActiveTank { get; set; }

        public int WarningSetPoint { get; set; }
        public int AlarmSetPoint { get; set; }

        public bool Tank1AlertEnabled { get; set; }
        public bool Tank2AlertEnabled { get; set; }
        public bool Tank3AlertEnabled { get; set; }
        public bool Tank4AlertEnabled { get; set; }

        public int RegisterBaseAddress { get; set; }
        public int ReadRegisterLength { get; set; }

        public int CoilBaseAddress { get; set; }
        public int ReadCoilLength { get; set; }

        public int CalInputBaseAddr { get; set; }
        public int CalInputLength { get; set; }

        public int DataForInputAddr { get; set; }
        public int CalModeAddr { get; set; }

        public ICollection<TankScaleReading> Readings { get; set; }

        public TankScale() {
            this.Registers = new ObservableHashSet<Register>();
            this.Readings = new ObservableHashSet<TankScaleReading>();
        }

        public TankScale(string identifier, string displayName,
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
        public TankScaleReading LastRead { get; set; }

    }
}

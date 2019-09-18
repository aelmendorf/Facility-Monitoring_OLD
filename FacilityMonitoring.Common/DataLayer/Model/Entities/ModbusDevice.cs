
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {
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

        public ICollection<Register> Registers { get; set; }
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

        public ICollection<GenericBoxReading> BoxReadings { get; set; }

        public GenericMonitorBox() {
            this.Registers = new ObservableHashSet<Register>();
            this.BoxReadings = new ObservableHashSet<GenericBoxReading>();
        }

        public GenericMonitorBox(string identifier, string displayName,
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

    public partial class AmmoniaController : ModbusDevice {

        public int ActiveTank { get; set; }

        public int RegisterBaseAddress { get; set; }
        public int ReadRegisterLength { get; set; }

        public int CoilBaseAddress { get; set; }
        public int ReadCoilLength { get; set; }

        public int CalInputBaseAddr { get; set; }
        public int CalInputLength { get; set; }

        public int DataForInputAddr { get; set; }
        public int CalModeAddr { get; set; }

        public ICollection<AmmoniaControllerReading> Readings { get; set; }

        public AmmoniaController() {
            this.Registers = new ObservableHashSet<Register>();
            this.Readings = new ObservableHashSet<AmmoniaControllerReading>();
        }

        public AmmoniaController(string identifier, string displayName,
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
    }
}

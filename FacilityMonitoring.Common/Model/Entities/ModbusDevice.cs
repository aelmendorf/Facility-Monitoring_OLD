
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {

    public enum AnalogSensorType {
        H2,O2,NH3,N,NONE
    }

    public partial class ModbusDevice {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int SlaveAddress { get; set; }
        public string Status { get; set; }

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
        }
    }

    public partial class GenericMonitorBox : ModbusDevice {
        public int AnalogChannelCount { get; set; }
        public int DigitalInputChannelCount { get; set; }
        public int DigitalOutputChannelCount { get; set; }

        //public ICollection<AnalogChannel> AnalogChannels { get; set; }
        //public ICollection<DigitalChannel> DigitalChannels { get; set; }
        public ICollection<Channel> Channels { get; set; }

        public GenericMonitorBox() {
            //this.DigitalChannels = new ObservableHashSet<DigitalChannel>();
            //this.AnalogChannels = new ObservableHashSet<AnalogChannel>();
            this.Channels = new ObservableHashSet<Channel>();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using Modbus.Device;

namespace FacilityMonitoring.Common.Hardware {
    public class MonitorBoxOperations : IMonitorBoxHardwareOperations {
        public GenericMonitorBox Device { get; set; }
        private IModbusOperations _modbus;

        public MonitorBoxOperations(GenericMonitorBox device,IModbusOperations modbus) {
            this.Device = device;
            this._modbus = modbus;
        }

        public Reading ReadAll() {
            
            return null;
        }

        public async Task<Reading> ReadAllAsync() {

            return null;
        }

        public bool SetAlarm(bool on_off) {

            return false;
        }

        public bool SetMaintenance(bool on_off) {
            return false;
        }

        public bool SetWarning(bool on_off) {

            return false;
        }
    }
}

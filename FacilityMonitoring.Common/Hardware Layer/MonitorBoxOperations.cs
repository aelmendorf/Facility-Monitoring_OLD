using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using Modbus.Device;

namespace FacilityMonitoring.Common.Hardware {
    //TODO:  Add Function codes and switch between input/read/etc etc

    public class MonitorBoxOperations : IMonitorBoxHardwareOperations {
        public GenericMonitorBox Device { get; set; }
        private IModbusOperations _modbus;

        public MonitorBoxOperations(GenericMonitorBox device) {
            this.Device = device;
            this._modbus = new ModbusOperations(this.Device.IpAddress,this.Device.Port,this.Device.SlaveAddress);
        }

        public GenericBoxReading ReadAll() {
            int regCount = Device.AnalogChannelCount + Device.DigitalOutputChannelCount;
            var data = this._modbus.ReadRegistersAndCoils(0, regCount, 0, this.Device.DigitalInputChannelCount);
            if (data != null) {
                ushort[] regData=data.Item1;
                bool[] coilData=data.Item2;
                GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", this.Device);
                foreach (var channel in this.Device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
                    double x = regData[channel.RegisterIndex];
                    x = (channel.ValueDivisor != 0) ? (x / channel.ValueDivisor) : x;
                    double y = channel.Slope * x + channel.Offset;
                    double current = (channel.Resistance != 0) ? (y / channel.Resistance) * 1000 : 0.00;

                    var equation = channel.GetEquationParameters();
                    if (equation != null) {
                        reading[channel.PropertyMap] = Math.Round((current * equation.Item1 + equation.Item2), 3);
                    } else {
                        reading[channel.PropertyMap] = Math.Round(current, 3);
                    }
                }

                foreach (var channel in this.Device.Registers.OfType<DigitalInputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = coilData[channel.RegisterIndex];
                }

                foreach (var channel in this.Device.Registers.OfType<DigitalOutputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = regData[channel.RegisterIndex] == 1 ? true : false;
                }

                return reading;
            } else {
                return null;
            }
        }

        public async Task<GenericBoxReading> ReadAllAsync() {
            int regCount = Device.AnalogChannelCount + Device.DigitalOutputChannelCount;
            var data = await this._modbus.ReadRegistersAndCoilsAsync(0, regCount, 0, this.Device.DigitalInputChannelCount);
            if (data != null) {
                ushort[] regData = data.Item1;
                bool[] coilData = data.Item2;
                GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", this.Device);
                foreach (var channel in this.Device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
                    double x = regData[channel.RegisterIndex];
                    x = (channel.ValueDivisor != 0) ? (x / channel.ValueDivisor) : x;
                    double y = channel.Slope * x + channel.Offset;
                    double current = (channel.Resistance != 0) ? (y / channel.Resistance) * 1000 : 0.00;

                    var equation = channel.GetEquationParameters();
                    if (equation != null) {
                        reading[channel.PropertyMap] = Math.Round((current * equation.Item1 + equation.Item2), 3);
                    } else {
                        reading[channel.PropertyMap] = Math.Round(current, 3);
                    }
                }

                foreach (var channel in this.Device.Registers.OfType<DigitalInputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = coilData[channel.RegisterIndex];
                }

                foreach (var channel in this.Device.Registers.OfType<DigitalOutputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = regData[channel.RegisterIndex] == 1 ? true : false;
                }

                return reading;
            } else {
                return null;
            }
        }

        public bool SetAlarm(bool on_off) {
            bool[] com = { true, false, false, on_off };
            return this._modbus.WriteCoils(this.Device.ModbusComAddr, com);
        }

        public async Task<bool> SetAlarmAsync(bool on_off) {
            bool[] com = { true, false, false, on_off };
            return await this._modbus.WriteCoilsAsync(this.Device.ModbusComAddr, com);
        }

        public bool SetMaintenance(bool on_off) {
            bool[] com = { true, on_off, false, false };
            return this._modbus.WriteCoils(this.Device.ModbusComAddr, com);
        }

        public async Task<bool> SetMaintenanceAsync(bool on_off) {
            bool[] com = { true, on_off, false, false };
            return await this._modbus.WriteCoilsAsync(this.Device.ModbusComAddr, com);
        }

        public bool SetWarning(bool on_off) {
            bool[] com = { true, false, on_off, false };
            return this._modbus.WriteCoils(this.Device.ModbusComAddr, com);
        }

        public async Task<bool> SetWarningAsync(bool on_off) {
            bool[] com = { true, false, on_off, false };
            return await this._modbus.WriteCoilsAsync(this.Device.ModbusComAddr, com);
        }
    }
}

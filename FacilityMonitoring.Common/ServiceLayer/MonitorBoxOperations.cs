using System;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Services.Interfaces;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace FacilityMonitoring.Common.Hardware {
    //TODO:  Add Function codes and switch between input/read/etc etc

    public class MonitorBoxOperations : IGenericBoxOperations {
        private readonly FacilityContext _context;
        private IModbusOperations _modbus;

        private GenericMonitorBox _device { get; set; }
        public GenericBoxReading LastRead { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device =(GenericMonitorBox)value;
        }

        public MonitorBoxOperations(FacilityContext context,GenericMonitorBox box) {
            this._context = context;
            this._device = box;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
        }

        public bool Read() {
            int regCount = _device.AnalogChannelCount + _device.DigitalOutputChannelCount;
            var data = this._modbus.ReadRegistersAndCoils(0, regCount, 0, this._device.DigitalInputChannelCount);
            if (data != null) {
                ushort[] regData = data.Item1;
                bool[] coilData = data.Item2;
                GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", this._device);
                foreach (var channel in this._device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
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

                foreach (var channel in this._device.Registers.OfType<DigitalInputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = coilData[channel.RegisterIndex];
                }

                foreach (var channel in this._device.Registers.OfType<DigitalOutputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = regData[channel.RegisterIndex] == 1 ? true : false;
                }

                return this.Save(reading);
            } else {
                return false;
            }
        }

        public async Task<bool> ReadAsync() {
            int regCount = _device.AnalogChannelCount + _device.DigitalOutputChannelCount;
            var data = await this._modbus.ReadRegistersAndCoilsAsync(0, regCount, 0, this._device.DigitalInputChannelCount);
            if (data != null) {
                ushort[] regData = data.Item1;
                bool[] coilData = data.Item2;
                GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", this._device);
                foreach (var channel in this._device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
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

                foreach (var channel in this._device.Registers.OfType<DigitalInputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = coilData[channel.RegisterIndex];
                }

                foreach (var channel in this._device.Registers.OfType<DigitalOutputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = regData[channel.RegisterIndex] == 1 ? true : false;
                }
                return this.Save(reading);
            } else {
                return false;
            }
        }

        public bool SetAlarm(bool on_off) {
            bool[] com = { true, false, false, on_off };
            return this._modbus.WriteCoils(this._device.ModbusComAddr, com);
        }

        public async Task<bool> SetAlarmAsync(bool on_off) {
            bool[] com = { true, false, false, on_off };
            return await this._modbus.WriteCoilsAsync(this._device.ModbusComAddr, com);
        }

        public bool SetMaintenance(bool on_off) {
            bool[] com = { true, on_off, false, false };
            return this._modbus.WriteCoils(this._device.ModbusComAddr, com);
        }

        public async Task<bool> SetMaintenanceAsync(bool on_off) {
            bool[] com = { true, on_off, false, false };
            return await this._modbus.WriteCoilsAsync(this._device.ModbusComAddr, com);
        }

        public bool SetWarning(bool on_off) {
            bool[] com = { true, false, on_off, false };
            return this._modbus.WriteCoils(this._device.ModbusComAddr, com);
        }

        public async Task<bool> SetWarningAsync(bool on_off) {
            bool[] com = { true, false, on_off, false };
            return await this._modbus.WriteCoilsAsync(this._device.ModbusComAddr, com);
        }

        private bool Save(GenericBoxReading reading) {
            this.LastRead = reading;
            this._device.BoxReadings.Add(reading);
            this._context.Entry<GenericMonitorBox>(this._device).State = EntityState.Modified;
            this._context.GenericBoxReadings.Add(reading);
            try {
                this._context.SaveChanges();
                return true;
            } catch {
                return false;
            }
        }
    }
}

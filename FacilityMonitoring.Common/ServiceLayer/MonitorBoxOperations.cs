using System;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Services.ModbusServices;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using System.Timers;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Hardware {
    public class MonitorBoxOperations : IGenericBoxOperations {
        private IModbusOperations _modbus;
        private readonly ILogger _logger;
        private readonly BufferBlock<IDeviceOperations> _bufferBlock;
        private Timer _timer;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;
        private GenericMonitorBox _device;

        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device=(GenericMonitorBox)value;
        }

        public Timer DeviceTimer {
            get =>this._timer;
            set => this._timer=value;
        }

        public MonitorBoxOperations(BufferBlock<IDeviceOperations> buffer,GenericMonitorBox box,ILogger<MonitorBoxOperations> logger) {
            this._bufferBlock = buffer;
            this._device = box;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._logger = logger;
            this.ReadInterval = box.ReadInterval*1000;
            this.SaveInterval = box.SaveInterval*1000;
            this._saveInterval = new TimeSpan(0, 0, (int)box.SaveInterval);
            this._timer = new Timer();
        }

        public async Task Start() {
            await this.ReadAsync();
            await this.SaveAsync();
            this._lastSave = DateTime.Now;
            this.Read();
            this.Save();
            this._timer.Interval = this.ReadInterval;
            this._timer.Elapsed += this._timer_Elapsed;
            this._timer.AutoReset = true;
            this._timer.Start();
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            this._timer.Enabled = false;
            await this.ReadAsync();
            if ((DateTime.Now - this._lastSave).TotalSeconds > this._saveInterval.TotalSeconds) {
                await this.SaveAsync();
                this._lastSave = DateTime.Now;
            }
            await this._bufferBlock.SendAsync(this);
            this._timer.Enabled = true;
        }

        public bool Read() {
            int regCount = _device.AnalogChannelCount + _device.DigitalOutputChannelCount;
            var data = this._modbus.ReadRegistersAndCoils(0, regCount, 0, this._device.DigitalInputChannelCount);
            if (data != null) {
                ushort[] regData = data.Item1;
                bool[] coilData = data.Item2;
                GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", this._device);
                GenericMonitorBoxAlert alert = new GenericMonitorBoxAlert();
                foreach (var channel in this._device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
                    double x = regData[channel.RegisterIndex];
                    x = (channel.ValueDivisor != 0) ? (x / channel.ValueDivisor) : x;
                    double y = channel.Slope * x + channel.Offset;
                    double current = (channel.Resistance != 0) ? (y / channel.Resistance) * 1000 : 0.00;
                    var equation = channel.GetEquationParameters();
                    if (equation != null) {
                        var value= Math.Round((current * equation.Item1 + equation.Item2), 3);                        
                        reading[channel.PropertyMap] = value;
                        if (value >= channel.Alarm1SetPoint) {
                            alert[channel.PropertyMap] = AnalogAlert.ALARM1;
                        } else if (value >= channel.Alarm2SetPoint) {
                            alert[channel.PropertyMap] = AnalogAlert.ALARM2;
                        } else if (value >= channel.Alarm3SetPoint) {
                            alert[channel.PropertyMap] = AnalogAlert.ALARM3;
                        } else {
                            alert[channel.PropertyMap] = AnalogAlert.NONE;
                        }
                    } else {
                        reading[channel.PropertyMap] = Math.Round(current, 3);
                        alert[channel.PropertyMap] = AnalogAlert.NONE;
                    }
                }

                foreach (var channel in this._device.Registers.OfType<DigitalInputChannel>().OrderBy(e => e.RegisterIndex)) {
                    if(channel.Connected) {
                        if (channel.Logic == LogicType.HIGH) {
                            alert[channel.PropertyMap] = coilData[channel.RegisterIndex];
                        } else {
                            alert[channel.PropertyMap] = !coilData[channel.RegisterIndex];
                        }
                    } else {
                        alert[channel.PropertyMap] = false;
                    }
                    reading[channel.PropertyMap] = coilData[channel.RegisterIndex];
                }

                foreach (var channel in this._device.Registers.OfType<DigitalOutputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = regData[channel.RegisterIndex] == 1 ? true : false;
                }
                this._device.LastRead = reading;
                this._device.LastRead.GenericMonitorBoxAlert = alert;
                this._device.LastRead.GenericMonitorBoxAlert.GenericMonitorBoxReading = this._device.LastRead;
                this._logger.LogInformation("{0} Read Succeeded", this.Device.Identifier);
                return true;
            } else {
                this._logger.LogError("{0} Read Failed", this.Device.Identifier);
                return false;
            }
        }

        public async Task<bool> ReadAsync() {
            int regCount = this._device.AnalogChannelCount + this._device.DigitalOutputChannelCount;
            var data = await this._modbus.ReadRegistersAndCoilsAsync(0, regCount, 0, this._device.DigitalInputChannelCount);
            if (data != null) {
                ushort[] regData = data.Item1;
                bool[] coilData = data.Item2;
                GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", this._device);
                GenericMonitorBoxAlert alert = new GenericMonitorBoxAlert();
                foreach (var channel in this._device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
                    double x = regData[channel.RegisterIndex];
                    x = (channel.ValueDivisor != 0) ? (x / channel.ValueDivisor) : x;
                    double y = channel.Slope * x + channel.Offset;
                    double current = (channel.Resistance != 0) ? (y / channel.Resistance) * 1000 : 0.00;
                    var equation = channel.GetEquationParameters();
                    if (equation != null) {
                        var value = Math.Round((current * equation.Item1 + equation.Item2), 3);
                        reading[channel.PropertyMap] = value;
                        if (value >= channel.Alarm1SetPoint) {
                            alert[channel.PropertyMap] = AnalogAlert.ALARM1;
                        } else if (value >= channel.Alarm2SetPoint) {
                            alert[channel.PropertyMap] = AnalogAlert.ALARM2;
                        } else if (value >= channel.Alarm3SetPoint) {
                            alert[channel.PropertyMap] = AnalogAlert.ALARM3;
                        } else {
                            alert[channel.PropertyMap] = AnalogAlert.NONE;
                        }
                    } else {
                        reading[channel.PropertyMap] = Math.Round(current, 3);
                        alert[channel.PropertyMap] = AnalogAlert.NONE;
                    }
                }

                foreach (var channel in this._device.Registers.OfType<DigitalInputChannel>().OrderBy(e => e.RegisterIndex)) {
                    if (channel.Connected) {
                        if (channel.Logic == LogicType.HIGH) {
                            alert[channel.PropertyMap] = !coilData[channel.RegisterIndex];
                        } else {
                            alert[channel.PropertyMap] = coilData[channel.RegisterIndex];
                        }
                    } else {
                        alert[channel.PropertyMap] = false;
                    }
                    reading[channel.PropertyMap] = coilData[channel.RegisterIndex];
                }

                foreach (var channel in this._device.Registers.OfType<DigitalOutputChannel>().OrderBy(e => e.RegisterIndex)) {
                    reading[channel.PropertyMap] = regData[channel.RegisterIndex] == 1 ? true : false;
                }
                this._device.LastRead = reading;
                this._device.LastRead.GenericMonitorBoxAlert = alert;
                this._device.LastRead.GenericMonitorBoxAlert.GenericMonitorBoxReading = this._device.LastRead;
                this._logger.LogInformation("{0} Read Succeeded", this.Device.Identifier);
                return true;
            } else {
                this._logger.LogError("{0} Read Failed", this.Device.Identifier);
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

        public void Check() {

        }

        public bool Save() {
            try {
                using var context = new FacilityContext();
                var device = context.ModbusDevices.Find(this._device.Id);
                if (device != null) {
                    this._device.LastRead.GenericMonitorBoxId = this._device.Id;
                    context.GenericBoxAlerts.Add(this._device.LastRead.GenericMonitorBoxAlert);
                    context.GenericBoxReadings.Add(this._device.LastRead);
                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    context.SaveChanges();
                    this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", this.Device.Identifier, this._device.BoxReadings.Count);
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", this.Device.Identifier);
                    return false;
                }

            } catch {
                this._logger.LogError("{0} Failed To Save", this._device.Identifier);
                return false;
            }
        }

        public async Task<bool> SaveAsync() {
            try {
                using var context = new FacilityContext();
                var device = await context.ModbusDevices.FindAsync(this._device.Id);
                if (device != null) {
                    this._device.LastRead.GenericMonitorBoxId = this._device.Id;
                    context.GenericBoxAlerts.Add(this._device.LastRead.GenericMonitorBoxAlert);
                    context.GenericBoxReadings.Add(this._device.LastRead);
                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", this.Device.Identifier, this._device.BoxReadings.Count);
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", this.Device.Identifier);
                    return false;
                }

            } catch {
                this._logger.LogError("{0} Failed To Save", this._device.Identifier);
                return false;
            }
        }

        
    }
}

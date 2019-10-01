using System;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Services.ModbusServices;
using FacilityMonitoring.Common.Model;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using FacilityMonitoring.Common.DataLayer;
using System.Timers;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.Server;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FacilityMonitoring.Common.Hardware {
    public class MonitorBoxOperations : IGenericBoxOperations {
        private IModbusOperations _modbus;
        private readonly ILogger _logger;
        private readonly BufferBlock<IDeviceOperations> _bufferBlock;
        private IAddDeviceReading _addReading;
        private System.Timers.Timer _timer;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;
        private GenericMonitorBox _device;

        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device=(GenericMonitorBox)value;
        }

        public System.Timers.Timer DeviceTimer {
            get =>this._timer;
            set => this._timer=value;
        }

        public MonitorBoxOperations(BufferBlock<IDeviceOperations> buffer,GenericMonitorBox box,ILogger<MonitorBoxOperations> logger,IAddDeviceReading addread) {
            this._bufferBlock = buffer;
            this._device = box;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._logger = logger;
            this._addReading = addread;
            this.ReadInterval = box.ReadInterval*1000;
            this.SaveInterval = box.SaveInterval*1000;
            this._saveInterval = new TimeSpan(0, 0, (int)box.SaveInterval);
            this._timer = new System.Timers.Timer();
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

        public async Task<bool> SaveAsync() {
            return await this._addReading.AddReadingAsync(this._device);
        }

        public bool Save() {
            return this._addReading.AddReading(this._device);
        }

    }

    public class MonitorBoxBackground : IMonitorBoxBackground, IHostedService, IDisposable {
        private IModbusOperations _modbus;
        private readonly ILogger _logger;
        private IAddDeviceReading _addReading;
        private readonly IHubContext<MonitorBoxHub, IMonitorBoxHub> _hub;
        private System.Threading.Timer _timer;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;
        private GenericMonitorBox _device;
        private WebData _data;
        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (GenericMonitorBox)value;
        }

        public System.Threading.Timer DeviceTimer {
            get => this._timer;
            set => this._timer = value;
        }

        public MonitorBoxBackground(IHubContext<MonitorBoxHub, IMonitorBoxHub> hub, ILogger<MonitorBoxOperations> logger,IAddDeviceReading addRead) {
            this._hub = hub;

            this._logger = logger;
            this._addReading = addRead;
            this._data = new WebData();
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            using(var context=new FacilityContext()) {
                var box = context.ModbusDevices.OfType<GenericMonitorBox>()
                    .Include(device => device.Registers)
                        .ThenInclude(register => register.SensorType)
                    .SingleOrDefault(e => e.Identifier== "GasBay");
                this._device = box;
                this.ReadInterval = box.ReadInterval * 1000;
                this.SaveInterval = box.SaveInterval * 1000;
                this._saveInterval = new TimeSpan(0, 0, (int)box.SaveInterval);
                this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            }

            List<string> headers = new List<string>();
            this._device.Registers.OfType<AnalogChannel>().ToList().ForEach(register => {
                    headers.Add(register.PropertyMap);
            });
            this._data.Headers=headers;
            this._lastSave = DateTime.Now;
            this.Read();
            this.Save();
            this._timer = new System.Threading.Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._device.ReadInterval));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            this._timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DoWork(object state) {
            await this.ReadAsync();
            if ((DateTime.Now - this._lastSave).TotalSeconds > this._saveInterval.TotalSeconds) {
                await this.SaveAsync();
                this._lastSave = DateTime.Now;
            }
        }

        public void Dispose() {
            this._timer?.Dispose();
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
                List<string> row = new List<string>();
                foreach(var header in this._data.Headers) {
                    row.Add(Convert.ToString(this._device.LastRead[header]));
                }
                this._data.Row = row;
                await this._hub.Clients.All.SendMonitorBoxReading(this._data);
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

        public async Task<bool> SaveAsync() {
            return await this._addReading.AddReadingAsync(this._device);
        }

        public bool Save() {
            return this._addReading.AddReading(this._device);
        }

    }
}

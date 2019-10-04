using System;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.DataLayer;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.Services;

namespace FacilityMonitoring.Common.Hardware {


    public class MonitorBoxController:IDeviceOperations  {
        private IModbusOperations _modbus;
        private MonitorBoxReadingAdd _addReading;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;
        private GenericMonitorBox _device;

        public string Data { get; set; }
        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (GenericMonitorBox)value;
        }

        public MonitorBoxController(GenericMonitorBox box) {
            this._device = box;
            this.ReadInterval = box.ReadInterval;
            this.SaveInterval = box.SaveInterval;
            this._saveInterval = new TimeSpan(0, 0, (int)box.SaveInterval);
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._addReading = new MonitorBoxReadingAdd();
            this.Data = "";
        }

        public async Task StartAsync() {
            await this.ReadAsync();
            await this.SaveAsync();
            this.ResetSaveTimer();       
        }

        public void Start() {
            this.Read();
            this.Save();
            this.ResetSaveTimer();
        }

        public bool CheckSaveTime() {
            return (DateTime.Now - this._lastSave).TotalSeconds > this._saveInterval.TotalSeconds;
        }

        public void ResetSaveTimer() {
            this._lastSave = DateTime.Now;
        }

        public bool Read() {
            int regCount = this._device.AnalogChannelCount + this._device.DigitalOutputChannelCount;
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
                this.Data = DateTime.Now.ToString() + ":::Reading: " + reading.AnalogCh6.ToString();
                return true;
            } else {
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
                this.Data =DateTime.Now.ToString()+":::Reading: "+reading.AnalogCh6.ToString();
                return true;
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

        public string GetData() {
            return this.Data;
        }

        public async Task<bool> SaveAsync() {
            return await this._addReading.AddReadingAsync(this._device);
        }

        public bool Save() {
            return this._addReading.AddReading(this._device);
        }

    }
}

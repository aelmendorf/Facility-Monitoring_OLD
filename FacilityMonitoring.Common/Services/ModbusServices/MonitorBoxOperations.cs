using System;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.DataLayer;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.Services;
using FacilityMonitoring.Common.DataLayer.DTOs;

namespace FacilityMonitoring.Common.Hardware {
    public class MonitorBoxOperations:IGenericBoxOperations  {
        private IModbusOperations _modbus;
        private MonitorBoxReadingAdd _addReading;
        private TimeSpan _saveInterval;
        private TimeSpan _readInterval;
        private DateTime _lastSave;
        private GenericBoxReading _lastReading;
        private GenericMonitorBox _device;
        private BoxReadingDTO _readingDTO;

        public BoxReadingDTO LastReading {
            get => this._readingDTO;
        }

        public double ReadInterval {
            get => this._readInterval.TotalSeconds;
        }

        public double SaveInterval {
            get => this._saveInterval.TotalSeconds;
        }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (GenericMonitorBox)value;
        }

        public MonitorBoxOperations(GenericMonitorBox box) {
            this._device = box;
            this._saveInterval = new TimeSpan(0, 0, (int)box.SaveInterval);
            this._readInterval = new TimeSpan(0, 0, (int)box.ReadInterval);
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._addReading = new MonitorBoxReadingAdd();
        }

        public async Task StartAsync() {
            await this.ReadAsync();
            await this.SaveAsync();
            this.GenerateTable();
            this.ResetSaveTimer();       
        }

        public void Start() {
            this.Read();
            this.Save();
            this.GenerateTable();
            this.ResetSaveTimer();
        }

        private void GenerateTable() {
            BoxReadingDTO reading = new BoxReadingDTO();
            var columns = this._device.Registers.Where(r => r.Display).ToList();
            columns.ForEach(read => {
                reading.Columns.Add(new Column() { ColumnName = read.PropertyMap, Header = read.Name });
            });
            reading.Rows.Add(this._lastReading);
        }

        public bool CheckSaveTime() {
            return (DateTime.Now - this._lastSave).TotalSeconds > this._saveInterval.TotalSeconds;
        }

        public void ResetSaveTimer() {
            this._lastSave = DateTime.Now;
        }

        public BoxReadingDTO Read() {
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
                this._device.LastRead.GenericMonitorBoxAlert.GenericBoxReadingId = this._device.LastRead.Id;
                this._lastReading = this._device.LastRead;
                this._readingDTO.Rows[0] = reading;
                this.Data = DateTime.Now.ToString() + ":::Reading: " + reading.AnalogCh6.ToString();
                return this._readingDTO;
            } else {
                return null;
            }
        }

        public async Task<BoxReadingDTO> ReadAsync() {
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
                this._device.LastRead.GenericMonitorBoxAlert.GenericBoxReadingId = this._device.LastRead.Id;
                this._lastReading = this._device.LastRead;
                this._readingDTO.Rows[0] = this._device.LastRead;
                return this._readingDTO;
            } else {
                return null;
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

        public ushort GetAnalogChannelRaw(int channel) {
            var regCount = this._device.AnalogChannelCount;
            if (channel < regCount && channel > 0) {
                var data = this._modbus.ReadRegisters(channel, 1);
                if (data.Length > 0 && data.Length < 2) {
                    return data[1];
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
        }

        public async Task<ushort> GetAnalogChannelRawAsync(int channel) {
            var regCount = this._device.AnalogChannelCount;
            if(channel<regCount && channel>0) {
                var data=await this._modbus.ReadRegistersAsync(channel,1);
                if(data.Length>0 && data.Length < 2) {
                    return data[1];
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
        }

        public double GetAnalogChannelVoltage(int channel) {
            var register = this._device.Registers.OfType<AnalogChannel>().SingleOrDefault(a => a.RegisterIndex == channel);
            if (register != null) {
                var regCount = this._device.AnalogChannelCount;
                if (channel < regCount && channel > 0) {
                    var data = this._modbus.ReadRegisters(channel, 1);
                    if (data.Length > 0 && data.Length < 2) {
                        return register.Slope * data[1] + register.Offset;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
        }

        public async Task<double> GetAnalogChannelVoltageAsync(int channel) {
            var register = this._device.Registers.OfType<AnalogChannel>().SingleOrDefault(a => a.RegisterIndex == channel);
            if (register != null) {
                var regCount = this._device.AnalogChannelCount;
                if (channel < regCount && channel > 0) {
                    var data = await this._modbus.ReadRegistersAsync(channel, 1);
                    if (data.Length > 0 && data.Length < 2) {
                        return register.Slope*data[1]+register.Offset;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
        }

        public async Task<bool> SaveAsync() {
            return await this._addReading.AddReadingAsync(this._device);
        }

        public bool Save() {
            return this._addReading.AddReading(this._device);
        }

    }
}

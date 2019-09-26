using System;
using System.Collections.Generic;
using System.Text;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Services.ModbusServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;
using System.Timers;

namespace FacilityMonitoring.Common.Hardware {
    public class AmmoniaControllerOperations : IAmmoniaOperations {
        private AmmoniaController _device { get; set; }
        private IModbusOperations _modbus;
        private readonly ILogger _logger;
        private readonly BufferBlock<IDeviceOperations> _bufferBlock;
        private Timer _timer;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;

        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (AmmoniaController)value;
        }

        public Timer DeviceTimer {
            get => this._timer;
            set => this._timer = value;
        }

        public AmmoniaControllerOperations(BufferBlock<IDeviceOperations> buffer,AmmoniaController device,ILogger<AmmoniaControllerOperations> logger) {
            this._device = device;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port,this._device.SlaveAddress);
            this._logger = logger;
            this._bufferBlock = buffer;
            this.ReadInterval = 2000;
            this.SaveInterval = 10000;
            this._saveInterval = new TimeSpan(0, 0, 10);
            this._timer = new Timer();
        }

        public async Task Start() {
            await this.ReadAsync();
            await this.SaveAsync();
            this._lastSave = DateTime.Now;
            this._timer.Interval = this.ReadInterval;
            this._timer.Elapsed += this._timer_Elapsed;
            this._timer.AutoReset = true;
            this._timer.Start();
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            this._timer.Enabled = false;
            var succeeded=await this.ReadAsync();
            if ((DateTime.Now - this._lastSave).TotalSeconds > this._saveInterval.TotalSeconds) {
                if (succeeded) {
                    await this.SaveAsync();
                    this._lastSave = DateTime.Now;
                }
            }
            await this._bufferBlock.SendAsync(this);
            this._timer.Enabled = true;
        }

        public bool Read() {
            var data = this._modbus.ReadRegistersAndCoils(this._device.RegisterBaseAddress, this._device.ReadRegisterLength, this._device.CoilBaseAddress, this._device.ReadCoilLength);
            if (data != null) {
                List<int> regValues = new List<int>();
                for (int i = 0; i < data.Item1.Length; i++) {
                    if (i >= 0 && i <= 55 && (i % 2 == 0)) {
                        regValues.Add(RegisterConverters.ToInt32(data.Item1[i], data.Item1[i + 1]));
                    } else if (i > 55 && i < data.Item1.Length) {
                        regValues.Add(data.Item1[i]);
                    }
                }//End loop
                AmmoniaControllerReading reading = new AmmoniaControllerReading(DateTime.Now, this._device);
                AmmoniaControllerAlert alert = new AmmoniaControllerAlert();
                reading.Set(regValues, data.Item2);
                alert.AmmoniaControllerReading = reading;
                reading.AmmoniaControllerAlert = alert;

                if (this._device.Tank1AlertEnabled) {
                    if (reading.Tank1Weight <= this._device.AlarmSetPoint) {
                        alert.Tank1Alert = GenericAlert.ALARM;
                    }else if (reading.Tank1Weight <= this._device.AlarmSetPoint) {
                        alert.Tank1Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank1Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank1Alert = GenericAlert.NONE;
                }


                if (this._device.Tank2AlertEnabled) {
                    if (reading.Tank2Weight <= this._device.AlarmSetPoint) {
                        alert.Tank2Alert = GenericAlert.ALARM;
                    } else if (reading.Tank2Weight <= this._device.AlarmSetPoint) {
                        alert.Tank2Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank2Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank2Alert = GenericAlert.NONE;
                }

                if (this._device.Tank3AlertEnabled) {
                    if (reading.Tank3Weight <= this._device.AlarmSetPoint) {
                        alert.Tank3Alert = GenericAlert.ALARM;
                    } else if (reading.Tank3Weight <= this._device.AlarmSetPoint) {
                        alert.Tank3Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank3Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank3Alert = GenericAlert.NONE;
                }

                if (this._device.Tank4AlertEnabled) {
                    if (reading.Tank4Weight <= this._device.AlarmSetPoint) {
                        alert.Tank4Alert = GenericAlert.ALARM;
                    } else if (reading.Tank4Weight <= this._device.AlarmSetPoint) {
                        alert.Tank4Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank4Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank4Alert = GenericAlert.NONE;
                }
                this._device.LastRead = reading;
                this._logger.LogInformation("{0} Read Succeeded", this.Device.Identifier);
                return true;
            } else {
                this._logger.LogError("{0} Read Failed", this.Device.Identifier);
                return false;
            }
        }

        public async Task<bool> ReadAsync() {
            var data = await this._modbus.ReadRegistersAndCoilsAsync(this._device.RegisterBaseAddress, this._device.ReadRegisterLength, this._device.CoilBaseAddress, this._device.ReadCoilLength);
            if (data != null) {
                List<int> regValues = new List<int>();
                for (int i = 0; i < data.Item1.Length; i++) {
                    if (i >= 0 && i <= 55 && (i % 2 == 0)) {
                        regValues.Add(RegisterConverters.ToInt32(data.Item1[i], data.Item1[i + 1]));
                    } else if (i > 55 && i < data.Item1.Length) {
                        regValues.Add(data.Item1[i]);
                    }
                }//End loop
                AmmoniaControllerReading reading = new AmmoniaControllerReading(DateTime.Now, this._device);
                AmmoniaControllerAlert alert = new AmmoniaControllerAlert();
                reading.Set(regValues, data.Item2);
                alert.AmmoniaControllerReading = reading;
                reading.AmmoniaControllerAlert = alert;

                if (this._device.Tank1AlertEnabled) {
                    if (reading.Tank1Weight <= this._device.AlarmSetPoint) {
                        alert.Tank1Alert = GenericAlert.ALARM;
                    } else if (reading.Tank1Weight <= this._device.WarningSetPoint) {
                        alert.Tank1Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank1Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank1Alert = GenericAlert.NONE;
                }


                if (this._device.Tank2AlertEnabled) {
                    if (reading.Tank2Weight <= this._device.AlarmSetPoint) {
                        alert.Tank2Alert = GenericAlert.ALARM;
                    } else if (reading.Tank2Weight <= this._device.WarningSetPoint) {
                        alert.Tank2Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank2Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank2Alert = GenericAlert.NONE;
                }

                if (this._device.Tank3AlertEnabled) {
                    if (reading.Tank3Weight <= this._device.AlarmSetPoint) {
                        alert.Tank3Alert = GenericAlert.ALARM;
                    } else if (reading.Tank3Weight <= this._device.WarningSetPoint) {
                        alert.Tank3Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank3Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank3Alert = GenericAlert.NONE;
                }

                if (this._device.Tank4AlertEnabled) {
                    if (reading.Tank4Weight <= this._device.AlarmSetPoint) {
                        alert.Tank4Alert = GenericAlert.ALARM;
                    } else if (reading.Tank4Weight <= this._device.WarningSetPoint) {
                        alert.Tank4Alert = GenericAlert.WARNING;
                    } else {
                        alert.Tank4Alert = GenericAlert.NONE;
                    }
                } else {
                    alert.Tank4Alert = GenericAlert.NONE;
                }
                this._device.LastRead = reading;
                this._logger.LogInformation("{0} Read Succeeded", this.Device.Identifier);
                return true;
            } else {
                this._logger.LogError("{0} Read Failed", this.Device.Identifier);
                return false;
            }
        }

        public bool SetCalibration(AmmoniaCalibrationData data) {
            var regData = RegisterConverters.ConvertCalToReg(data);
            if (this._modbus.WriteRegisters(this._device.CalInputBaseAddr, regData)) {
                if (this._modbus.WriteSingleCoil(this._device.DataForInputAddr, true)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public bool SetCalibrationMode(bool on_off) {
            return this._modbus.WriteSingleCoil(this._device.CalModeAddr, on_off);
        }

        public async Task<bool> SetCalibrationAsync(AmmoniaCalibrationData data) {
            var regData = RegisterConverters.ConvertCalToReg(data);
            var success = await this._modbus.WriteRegistersAsync(this._device.CalInputBaseAddr,regData);
            if (success) {
                return await this._modbus.WriteSingleCoilAsync(this._device.DataForInputAddr, true);
            } else {
                return false;
            }
        }

        public async Task<bool> SetCalibrationModeAsync(bool on_off) {
            return await this._modbus.WriteSingleCoilAsync(this._device.CalModeAddr, on_off);
        }

        public bool Save() {
            using var context = new FacilityContext();
            var device = context.ModbusDevices.Find(this._device.Id);
            if (device != null) {
                this._device.LastRead.AmmoniaControllerId = this._device.Id;
                //device.Readings.Add(this._device.LastRead);
                context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                context.AmmoniaControllerReadings.Add(this._device.LastRead);
            } else {
                this._logger.LogError("{0} Device Not Found", this.Device.Identifier);
                return false;
            }
            try {
                context.SaveChanges();
                this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                return true;
            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", this.Device.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }

                this._logger.LogError(builder.ToString());
                return false;
            }
        }

        public async Task<bool> SaveAsync() {
            try {
                using var context = new FacilityContext();
                var device = await context.ModbusDevices.FindAsync(this._device.Id);
                if (device != null) {
                    this._device.LastRead.AmmoniaControllerId = this._device.Id;

                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    context.AmmoniaControllerReadings.Add(this._device.LastRead);
                    context.AmmoniaControllerAlerts.Add(this._device.LastRead.AmmoniaControllerAlert);
                    await context.SaveChangesAsync();
                    this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", this.Device.Identifier);
                    return false;
                }
            } catch(Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", this.Device.Identifier)
                    .AppendFormat("Exception: {0}",e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }

                this._logger.LogError(builder.ToString());
                return false;
            }
        }
    }
}

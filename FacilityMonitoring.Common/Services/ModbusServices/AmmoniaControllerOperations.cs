using System;
using System.Collections.Generic;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Services;
using System.Threading.Tasks;
using FacilityMonitoring.Common.DataLayer;

namespace FacilityMonitoring.Common.Hardware {
    public class NH3Controller : IAmmoniaOperations {
        private AmmoniaController _device { get; set; }
        private IModbusOperations _modbus;
        private AddNHControllerReading _addReading;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;

        public string Data { get; set; }

        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (AmmoniaController)value;
        }

        public NH3Controller(AmmoniaController device) {
            this._device = device;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port,this._device.SlaveAddress);
            this._addReading = new AddNHControllerReading();
            this.ReadInterval = device.ReadInterval;
            this.SaveInterval = device.SaveInterval;
            this._saveInterval = new TimeSpan(0, 0, (int)this._device.SaveInterval);
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
                this.Data = "Tank 1 Weight: " + this._device.LastRead.Tank1Weight.ToString();
                return true;
            } else {
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
                this.Data = "Tank 1 Weight: " + this._device.LastRead.Tank1Weight.ToString();
                return true;
            } else {
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

        public async Task<bool> SaveAsync() {
            return await this._addReading.AddReadingAsync(this._device);
        }

        public bool Save() {
            return  this._addReading.AddReading(this._device);
        }
    }
}

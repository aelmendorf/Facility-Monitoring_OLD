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

namespace FacilityMonitoring.Common.Hardware {
    public class AmmoniaControllerOperations : IAmmoniaOperations {
        private AmmoniaController _device { get; set; }
        private readonly FacilityContext _context;
        private IModbusOperations _modbus;
        private readonly ILogger _logger;

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (AmmoniaController)value;
        }

        public AmmoniaControllerOperations(FacilityContext context,AmmoniaController device,ILogger<AmmoniaControllerOperations> logger) {
            this._context = context;
            this._device = device;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port,this._device.SlaveAddress);
            this._logger = logger;
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
                reading.Set(regValues, data.Item2);
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
                reading.Set(regValues, data.Item2);
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
            this._device.Readings.Add(this._device.LastRead);
            this._context.Entry<AmmoniaController>(this._device).State = EntityState.Modified;
            this._context.AmmoniaControllerReadings.Add(this._device.LastRead);
            try {
                this._context.SaveChanges();
                this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                return true;
            } catch {
                return false;
            }
        }

        public async Task<bool> SaveAsync() {
            this._device.Readings.Add(this._device.LastRead);
            this._context.Entry<AmmoniaController>(this._device).State = EntityState.Modified;
            this._context.AmmoniaControllerReadings.Add(this._device.LastRead);
            try {
                await this._context.SaveChangesAsync();
                this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                return true;
            } catch {
                this._logger.LogError("{0} Save Failed", this.Device.Identifier);
                return false;
            }
        }
    }
}

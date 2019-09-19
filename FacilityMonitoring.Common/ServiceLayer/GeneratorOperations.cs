using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.ModbusServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.Common.Hardware {
    public class GeneratorOperations : IGeneratorOperations {
        private H2Generator _device { get; set; }
        private IModbusOperations _modbus;
        private readonly FacilityContext _context;
        private readonly ILogger _logger;

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (H2Generator)value;
        }

        public GeneratorOperations(FacilityContext context,H2Generator device,ILogger<GeneratorOperations> logger) {
            this._context = context;
            this._device = device;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._logger = logger;
        }

        public bool Read() {
            H2GenReading reading = new H2GenReading();
            reading.TimeStamp = DateTime.Now;
            foreach(var register in this._device.Registers.OfType<GeneratorRegister>()) {
                switch (register.FunctionCode) {
                    case FunctionCode.ReadCoil: {
                        var coils = this._modbus.ReadCoils(register.RegisterIndex, register.RegisterLength);
                        if (coils != null) {
                            reading[register.PropertyMap] = reading[register.PropertyMap] = RegisterConverters.GetH2RegisterValue(register, coilData: coils);
                        } else {
                            this._logger.LogError("{0} Read Failed On {1} property",this.Device.Identifier,register.PropertyMap);
                            return false;
                        }
                        break;
                    }
                    case FunctionCode.ReadDiscreteInput: { break; }
                    case FunctionCode.ReadHoldingRegisters: 
                    case FunctionCode.ReadInputRegisters: 
                        var data = this._modbus.ReadRegisters(register.FunctionCode,register.RegisterIndex, register.RegisterLength);
                        if (data != null) {
                            var value= RegisterConverters.GetH2RegisterValue(register, regData: data);
                            if (value != null) {
                                reading[register.PropertyMap] = value;
                            } else {
                                this._logger.LogError("{0} Read Failed On {1} property", this.Device.Identifier, register.PropertyMap);
                                return false;
                            }
                        }
                        break;
                    case FunctionCode.WriteSingleCoil: { break; }

                    case FunctionCode.WriteSingleHoldingRegister: { break; }

                    case FunctionCode.WriteMultipleCoils: { break; }

                    case FunctionCode.WriteMultipleHoldingRegisters: { break; }
                }
            }
            this._logger.LogInformation("{0} Read Succeeded",this.Device.Identifier);
            return true;
        }

        public async Task<bool> ReadAsync() {
            H2GenReading reading = new H2GenReading();
            reading.TimeStamp = DateTime.Now;
            foreach (var register in this._device.Registers.OfType<GeneratorRegister>()) {
                switch (register.FunctionCode) {
                    case FunctionCode.ReadCoil: {
                        var coils = await this._modbus.ReadCoilsAsync(register.RegisterIndex, register.RegisterLength);
                        if (coils != null) {
                            reading[register.PropertyMap] = reading[register.PropertyMap] = RegisterConverters.GetH2RegisterValue(register, coilData: coils);
                        } else {
                            this._logger.LogError("{0} Read Failed On {1} property", this.Device.Identifier, register.PropertyMap);
                            return false;
                        }
                        break;
                    }
                    case FunctionCode.ReadDiscreteInput: { break; }
                    case FunctionCode.ReadHoldingRegisters:
                    case FunctionCode.ReadInputRegisters:
                        var data = await this._modbus.ReadRegistersAsync(register.FunctionCode, register.RegisterIndex, register.RegisterLength);
                        if (data != null) {
                            var value = RegisterConverters.GetH2RegisterValue(register, regData: data);
                            if (value != null) {
                                reading[register.PropertyMap] = value;
                            } else {
                                this._logger.LogError("{0} Read Failed On {1} property", this.Device.Identifier, register.PropertyMap);
                                return false;
                            }
                        }
                        break;
                    case FunctionCode.WriteSingleCoil: { break; }

                    case FunctionCode.WriteSingleHoldingRegister: { break; }

                    case FunctionCode.WriteMultipleCoils: { break; }

                    case FunctionCode.WriteMultipleHoldingRegisters: { break; }
                }

            }
            this._logger.LogInformation("{0} Read Succeeded", this.Device.Identifier);
            this._device.LastRead = reading;
            return true;
        }

        public bool Save() {
            try {
                this._context.GeneratorSystemErrors.Add(this._device.LastRead.AllSystemErrors);
                this._context.GeneratorSystemWarnings.Add(this._device.LastRead.AllSystemWarnings);
                this._device.H2Readings.Add(this._device.LastRead);
                this._device.SystemState = this._device.LastRead.SystemState;
                this._device.OperationMode = this._device.LastRead.OperationMode;
                this._context.Entry<H2Generator>(this._device).State = EntityState.Modified;
                this._context.H2GenReadings.Add(this._device.LastRead);
                this._context.SaveChanges();
                this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                return true;
            } catch {
                this._logger.LogError("{0} Failed To Save", this._device.Identifier);
                return false;
            }
        }

        public async Task<bool> SaveAsync() {
            try {
                this._context.GeneratorSystemErrors.Add(this._device.LastRead.AllSystemErrors);
                this._context.GeneratorSystemWarnings.Add(this._device.LastRead.AllSystemWarnings);
                this._device.H2Readings.Add(this._device.LastRead);
                this._device.SystemState = this._device.LastRead.SystemState;
                this._device.OperationMode = this._device.LastRead.OperationMode;
                this._context.Entry<H2Generator>(this._device).State = EntityState.Modified;
                this._context.H2GenReadings.Add(this._device.LastRead);
                this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                await this._context.SaveChangesAsync();
                return true;
            } catch {
                this._logger.LogError("{0} Failed To Save",this._device.Identifier);
                return false;
            }
        }
    }
}

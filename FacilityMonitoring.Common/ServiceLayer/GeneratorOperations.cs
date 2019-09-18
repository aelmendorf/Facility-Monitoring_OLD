using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacilityMonitoring.Common.Hardware {
    public class GeneratorOperations : IGeneratorOperations {
        private H2Generator _device { get; set; }
        public H2GenReading LastRead { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (H2Generator)value;
        }

        private IModbusOperations _modbus;
        private readonly FacilityContext _context;

        public GeneratorOperations(FacilityContext context,H2Generator device) {
            this._context = context;
            this._device = device;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
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
            return this.Save(reading);
        }

        private bool Save(H2GenReading reading) {
            try {
                this._context.GeneratorSystemErrors.Add(reading.AllSystemErrors);
                this._context.GeneratorSystemWarnings.Add(reading.AllSystemWarnings);
                this._device.H2Readings.Add(reading);
                this._device.SystemState = reading.SystemState;
                this._device.OperationMode = reading.OperationMode;
                this._context.Entry<H2Generator>(this._device).State = EntityState.Modified;
                this._context.H2GenReadings.Add(reading);
                this._context.SaveChanges();
                return true;
            } catch {
                return false;
            }
        }

        private async Task<bool> SaveAsync(H2GenReading reading) {
            try {
                this._context.GeneratorSystemErrors.Add(reading.AllSystemErrors);
                this._context.GeneratorSystemWarnings.Add(reading.AllSystemWarnings);
                this._device.H2Readings.Add(reading);
                this._device.SystemState = reading.SystemState;
                this._device.OperationMode = reading.OperationMode;
                this._context.Entry<H2Generator>(this._device).State = EntityState.Modified;
                this._context.H2GenReadings.Add(reading);
                await this._context.SaveChangesAsync();
                return true;
            } catch {
                return false;
            }
        }
    }
}

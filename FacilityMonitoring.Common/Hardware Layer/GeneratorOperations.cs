using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Model;

namespace FacilityMonitoring.Common.Hardware {
    public class GeneratorOperations : IGeneratorOperations {
        public H2Generator Device { get; set; }
        private IModbusOperations _modbus;

        public GeneratorOperations(H2Generator device) {
            this.Device = device;
            this._modbus = new ModbusOperations(this.Device.IpAddress, this.Device.Port, this.Device.SlaveAddress);
        }

        public H2GenReading ReadAll() {
            H2GenReading reading = new H2GenReading();
            reading.TimeStamp = DateTime.Now;
            foreach(var register in this.Device.Registers.OfType<GeneratorRegister>()) {
                switch (register.FunctionCode) {
                    case FunctionCode.ReadCoil: {
                        var coils = this._modbus.ReadCoils(register.RegisterIndex, register.RegisterLength);
                        reading[register.PropertyMap] = reading[register.PropertyMap] = RegisterConverters.GetH2RegisterValue(register,coilData:coils);
                        break;
                    }
                    case FunctionCode.ReadDiscreteInput: { break; }
                    case FunctionCode.ReadHoldingRegisters: 
                    case FunctionCode.ReadInputRegisters: 
                        var data = this._modbus.ReadRegisters(register.FunctionCode,register.RegisterIndex, register.RegisterLength);
                        reading[register.PropertyMap] = RegisterConverters.GetH2RegisterValue(register, regData: data);
                        break;
                    case FunctionCode.WriteSingleCoil: { break; }

                    case FunctionCode.WriteSingleHoldingRegister: { break; }

                    case FunctionCode.WriteMultipleCoils: { break; }

                    case FunctionCode.WriteMultipleHoldingRegisters: { break; }
                }

            }
            return reading;
        }
    }
}

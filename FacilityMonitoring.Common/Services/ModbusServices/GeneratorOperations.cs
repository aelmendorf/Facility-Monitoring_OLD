using System;
using System.Linq;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services;

namespace FacilityMonitoring.Common.Hardware {
    public class GeneratorController : IGeneratorOperations {

        private H2Generator _device { get; set; }
        private IModbusOperations _modbus;
        private AddGeneratorReading _addReading;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;
        public string Data { get; set; }

        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (H2Generator)value;
        }

        public GeneratorController(H2Generator generator) {
            this._device = generator;
            this.ReadInterval = generator.ReadInterval;
            this.SaveInterval = generator.SaveInterval;
            this._saveInterval = new TimeSpan(0, 0, (int)generator.SaveInterval);
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._addReading = new AddGeneratorReading();
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
            H2GenReading reading = new H2GenReading();
            reading.TimeStamp = DateTime.Now;
            foreach (var register in this._device.Registers.OfType<GeneratorRegister>()) {
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
                        var data = this._modbus.ReadRegisters(register.FunctionCode, register.RegisterIndex, register.RegisterLength);
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
            this._device.LastRead = reading;
            this.Data = this._device.Identifier + "SystemPressure: " + this._device.LastRead.SystemPressure;
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
            this._device.LastRead = reading;
            this.Data = this._device.Identifier + "SystemPressure: " + this._device.LastRead.SystemPressure;
            return true;
        }

        public bool Save() {
            return this._addReading.AddReading(this._device);
        }

        public async Task<bool> SaveAsync() {
            return await this._addReading.AddReadingAsync(this._device);
        }
    }

}
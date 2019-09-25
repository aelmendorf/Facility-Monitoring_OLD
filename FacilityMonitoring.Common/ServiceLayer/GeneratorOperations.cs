using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Timers;
using FacilityMonitoring.Common.Converters;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.ModbusServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.Common.Hardware {
    public class GeneratorOperations : IGeneratorOperations {
        private H2Generator _device { get; set; }
        private IModbusOperations _modbus;
        private readonly ILogger _logger;
        private Timer _timer;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;
        private readonly BufferBlock<IDeviceOperations> _bufferBlock;

        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public ModbusDevice Device {
            get => this._device;
            private set => this._device = (H2Generator)value;
        }

        public Timer DeviceTimer {
            get => this._timer;
            set => this._timer = value;
        }

        public GeneratorOperations(BufferBlock<IDeviceOperations> buffer,H2Generator device,ILogger<GeneratorOperations> logger) {
            this._device = device;
            this._modbus = new ModbusOperations(this._device.IpAddress, this._device.Port, this._device.SlaveAddress);
            this._logger = logger;
            this._bufferBlock = buffer;
            this.ReadInterval = 5000;
            this.SaveInterval = 30000;
            this._saveInterval = new TimeSpan(0, 0, (int)(this.SaveInterval/1000));
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
            await this.ReadAsync();
            if ((DateTime.Now - this._lastSave).TotalSeconds > this._saveInterval.TotalSeconds) {
                await this.SaveAsync();
                this._lastSave = DateTime.Now;
            }
            await this._bufferBlock.SendAsync(this);
            this._timer.Enabled = true;
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
                using var context = new FacilityContext();
                var device = context.ModbusDevices.Find(this._device.Id);
                if (device != null) {
                    this._device.LastRead.GeneratorId = this._device.Id;
                    context.GeneratorSystemErrors.Add(this._device.LastRead.AllSystemErrors);
                    context.GeneratorSystemWarnings.Add(this._device.LastRead.AllSystemWarnings);
                    //this._device.H2Readings.Add(this._device.LastRead);
                    this._device.SystemState = this._device.LastRead.SystemState;
                    this._device.OperationMode = this._device.LastRead.OperationMode;
                    context.Entry(device).State = EntityState.Modified;
                    context.H2GenReadings.Add(this._device.LastRead);
                    context.SaveChanges();
                    this._logger.LogInformation("{0} Save Succeeded", this.Device.Identifier);
                    GC.Collect();
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", this.Device.Identifier);
                    return false;
                }
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
                    this._device.LastRead.GeneratorId = this._device.Id;
                    context.GeneratorSystemErrors.Add(this._device.LastRead.AllSystemErrors);
                    context.GeneratorSystemWarnings.Add(this._device.LastRead.AllSystemWarnings);
                    //this._device.H2Readings.Add(this._device.LastRead);
                    this._device.SystemState = this._device.LastRead.SystemState;
                    this._device.OperationMode = this._device.LastRead.OperationMode;
                    context.Entry(device).State = EntityState.Modified;
                    context.H2GenReadings.Add(this._device.LastRead);
                    await context.SaveChangesAsync();
                    this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", this.Device.Identifier,this._device.H2Readings.Count);
                    GC.Collect();
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", this.Device.Identifier);
                    return false;
                }
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
    }
}

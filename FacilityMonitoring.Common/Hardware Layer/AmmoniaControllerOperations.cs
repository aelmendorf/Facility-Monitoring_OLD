using System;
using System.Collections.Generic;
using System.Text;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Converters;

namespace FacilityMonitoring.Common.Hardware {
    public class AmmoniaControllerOperations : IAmmoniaControllerOperations {
        public AmmoniaController Device { get; set; }
        private IModbusOperations _modbus;

        public AmmoniaControllerOperations(AmmoniaController device) {
            this.Device = device;
            this._modbus = new ModbusOperations(device.IpAddress, device.Port);
        }

        public AmmoniaControllerReading ReadAll() {
            var data = this._modbus.ReadRegistersAndCoils(this.Device.RegisterBaseAddress, this.Device.ReadRegisterLength, this.Device.CoilBaseAddress, this.Device.ReadCoilLength);
            if (data != null) {
                List<int> regValues = new List<int>();
                for (int i = 0; i < data.Item1.Length; i++) {
                    if (i >= 0 && i <= 55 && (i % 2 == 0)) {
                        regValues.Add(RegisterConverters.ToInt32(data.Item1[i], data.Item1[i+1]));
                    } else if (i > 55 && i < data.Item1.Length) {
                        regValues.Add(data.Item1[i]);
                    }
                }//End loop
                AmmoniaControllerReading reading = new AmmoniaControllerReading(DateTime.Now, this.Device);
                reading.Set(regValues, data.Item2);
                return reading;
            } else {
                return null;
            }
        }

        public AmmoniaCalibrationData ReadTankCalibration(int tank) {
            var reading = this.ReadAll();
            if (reading != null) {
                return reading.GetTankCalibration(tank);
            } else {
                return null;
            }
        }

        public bool SetCalibration(AmmoniaCalibrationData data,int tank) {
            var regData = RegisterConverters.ConvertCalToReg(data,tank);
            if(this._modbus.WriteRegisters(this.Device.CalInputBaseAddr, regData)) {
                if (this._modbus.WriteSingleCoil(this.Device.DataForInputAddr, true)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public bool SetCalibrationMode(bool on_off) {
            return this._modbus.WriteSingleCoil(this.Device.CalModeAddr,on_off);
        }
    }
}

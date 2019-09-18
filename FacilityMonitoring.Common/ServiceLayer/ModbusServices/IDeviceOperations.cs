using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services.Interfaces {
    public interface IDeviceOperations {
        ModbusDevice Device { get;}
        bool Read();
        Task<bool> ReadAsync();
        //event EventHandler DataReady;
    }

    public interface IGenericBoxOperations : IDeviceOperations {
        GenericBoxReading LastRead { get; set; }
        bool SetAlarm(bool on_off);
        Task<bool> SetAlarmAsync(bool on_off);
        bool SetWarning(bool on_off);
        Task<bool> SetWarningAsync(bool on_off);
        bool SetMaintenance(bool on_off);
        Task<bool> SetMaintenanceAsync(bool on_off);
    }

    public interface IAmmoniaOperations:IDeviceOperations {
        AmmoniaControllerReading LastRead { get; set; }
        bool SetCalibration(AmmoniaCalibrationData data);
        Task<bool> SetCalibrationAsync(AmmoniaCalibrationData data);
        bool SetCalibrationMode(bool on_off);
        Task<bool> SetCalibrationModeAsync(bool on_off);
    }

    public interface IGeneratorOperations: IDeviceOperations {
        H2GenReading LastRead { get; set; }
    }

    public interface IDeviceController {
        List<ModbusDevice> Devices { get; set; }
        bool ReadAll();
        bool Read(ModbusDevice device);
    }
}

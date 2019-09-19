using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services.ModbusServices {
    public interface IDeviceOperations {
        ModbusDevice Device { get;}
        bool Read();
        Task<bool> ReadAsync();
        bool Save();
        Task<bool> SaveAsync();
    }

    public interface IGenericBoxOperations : IDeviceOperations {

        bool SetAlarm(bool on_off);
        Task<bool> SetAlarmAsync(bool on_off);
        bool SetWarning(bool on_off);
        Task<bool> SetWarningAsync(bool on_off);
        bool SetMaintenance(bool on_off);
        Task<bool> SetMaintenanceAsync(bool on_off);
    }

    public interface IAmmoniaOperations:IDeviceOperations {

        bool SetCalibration(AmmoniaCalibrationData data);
        Task<bool> SetCalibrationAsync(AmmoniaCalibrationData data);
        bool SetCalibrationMode(bool on_off);
        Task<bool> SetCalibrationModeAsync(bool on_off);
    }

    public interface IGeneratorOperations: IDeviceOperations {

    }

    public interface IDeviceController {
        List<ModbusDevice> Devices { get; set; }
        bool ReadAll();
        bool Read(ModbusDevice device);
    }
}

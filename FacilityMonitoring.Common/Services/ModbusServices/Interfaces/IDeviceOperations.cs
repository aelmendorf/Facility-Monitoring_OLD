using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace FacilityMonitoring.Common.Services {
    public interface IDeviceOperations {
        double ReadInterval { get; }
        double SaveInterval { get; }
        string Data { get; }
        Task StartAsync();
        void Start();

        ModbusDevice Device { get;}
        string Read();
        Task<string> ReadAsync();
        bool Save();
        Task<bool> SaveAsync();
        bool CheckSaveTime();
        void ResetSaveTimer();
    }

    public interface IGenericBoxOperations : IDeviceOperations {
        GenericBoxReading LastReading { get; }
        bool SetAlarm(bool on_off);
        Task<bool> SetAlarmAsync(bool on_off);
        bool SetWarning(bool on_off);
        Task<bool> SetWarningAsync(bool on_off);
        bool SetMaintenance(bool on_off);
        Task<bool> SetMaintenanceAsync(bool on_off);
    }

    public interface IAmmoniaOperations:IDeviceOperations {
        AmmoniaControllerReading LastReading { get; }
        bool SetCalibration(AmmoniaCalibrationData data);
        Task<bool> SetCalibrationAsync(AmmoniaCalibrationData data);
        bool SetCalibrationMode(bool on_off);
        Task<bool> SetCalibrationModeAsync(bool on_off);
    }

    public interface IGeneratorOperations: IDeviceOperations {
        H2GenReading LastReading { get; }
    }

    public interface IDeviceController {
        List<ModbusDevice> Devices { get; set; }
        bool ReadAll();
        bool Read(ModbusDevice device);
    }
}

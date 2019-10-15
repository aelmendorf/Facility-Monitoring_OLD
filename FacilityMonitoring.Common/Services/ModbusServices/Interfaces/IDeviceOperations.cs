using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.DataLayer.DTOs;
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
        bool Save();
        Task<bool> SaveAsync();
        bool CheckSaveTime();
        void ResetSaveTimer();
    }

    public interface IGenericBoxOperations : IDeviceOperations {
        BoxReadingDTO LastReading { get; }
        BoxReadingDTO Read();
        Task<BoxReadingDTO> ReadAsync();
        bool SetAlarm(bool on_off);
        Task<bool> SetAlarmAsync(bool on_off);
        bool SetWarning(bool on_off);
        Task<bool> SetWarningAsync(bool on_off);
        bool SetMaintenance(bool on_off);
        Task<bool> SetMaintenanceAsync(bool on_off);

        Task<ushort> GetAnalogChannelRawAsync(int channel);
        Task<double> GetAnalogChannelVoltageAsync(int channel);
        ushort GetAnalogChannelRaw(int channel);
        double GetAnalogChannelVoltage(int channel);
    }

    public interface IAmmoniaOperations:IDeviceOperations {
        AmmoniaControllerReading LastReading { get; }
        AmmoniaControllerReading Read();
        Task<AmmoniaControllerReading> ReadAsync();

        bool SetCalibration(AmmoniaCalibrationData data);
        Task<bool> SetCalibrationAsync(AmmoniaCalibrationData data);
        bool SetCalibrationMode(bool on_off);
        Task<bool> SetCalibrationModeAsync(bool on_off);
    }

    public interface IGeneratorOperations: IDeviceOperations {
        H2GenReading LastReading { get; }
        H2GenReading Read();
        Task<H2GenReading> ReadAsync();
    }
}

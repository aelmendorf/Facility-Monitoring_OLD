using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.Data.Entities;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.ModbusServices.Operations {
    public interface IGenericBoxOperations : IDeviceOperations {
        BoxReadingDTO DeviceTable { get; }
        MonitorBoxReading LastReading { get; }
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
}

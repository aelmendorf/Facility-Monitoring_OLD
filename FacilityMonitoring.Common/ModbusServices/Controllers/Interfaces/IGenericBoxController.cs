using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.ModbusServices.Operations;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {
    public interface IGenericBoxController : IDeviceController {
        IGenericBoxOperations Operations { get; }
        MonitorBoxReading GetCurrentReading();
        BoxReadingDTO GetDeviceTable();

        bool SetAlarm(bool on_off);
        Task<bool> SetAlarmAsync( bool on_off);
        bool SetWarning(bool on_off);
        Task<bool> SetWarningAsync(bool on_off);
        bool SetMaintenance(bool on_off);
        Task<bool> SetMaintenanceAsync(bool on_off);

        bool GetDeviceState(out DeviceState state);

        Task<ushort> GetAnalogChannelRawAsync(int channel);
        Task<double> GetAnalogChannelVoltageAsync(int channel);
        ushort GetAnalogChannelRaw(int channel);
        double GetAnalogChannelVoltage(int channel);
    }
}

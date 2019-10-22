using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.DataLayer.DTOs;
using FacilityMonitoring.Common.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services {
    public interface IDeviceCollectionController {
        double ReadInterval { get; }
        Task StartAsync();
        void Start();
        void Stop();
        Task StopAsync();
        void TimeHandler(object state);
    }

    public interface IGeneratorCollectionController:IDeviceCollectionController {
        ConcurrentDictionary<IGeneratorOperations,H2GenReading> Operations { get; }
        GeneratorReadingDTO GetLastReading(string genId);
        IEnumerable<GeneratorReadingDTO> GetAllGenerators();
    }
    public interface IAmmoniaCollectionController : IDeviceCollectionController {
        IAmmoniaOperations Operations { get; }

    }

    public interface IBoxCollectionController : IDeviceCollectionController {
        ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading> Operations { get; }
        GenericBoxReading GetCurrentReading(string id);
        BoxReadingDTO GetDeviceTable(string id);

        bool SetAlarm(string id,bool on_off);
        Task<bool> SetAlarmAsync(string id, bool on_off);
        bool SetWarning(string id, bool on_off);
        Task<bool> SetWarningAsync(string id, bool on_off);
        bool SetMaintenance(string id, bool on_off);
        Task<bool> SetMaintenanceAsync(string id, bool on_off);

        bool GetDeviceState(string id, out DeviceState state);

        Task<ushort> GetAnalogChannelRawAsync(string id,int channel);
        Task<double> GetAnalogChannelVoltageAsync(string id, int channel);
        ushort GetAnalogChannelRaw(string id, int channel);
        double GetAnalogChannelVoltage(string id, int channel);
    }

    public interface IGenericBoxController : IDeviceCollectionController {
        IGenericBoxOperations Operations { get; }
        GenericBoxReading GetCurrentReading();
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

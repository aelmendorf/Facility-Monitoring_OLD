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
        H2GenReading GetLastReading(string genId);

    }
    public interface IAmmoniaCollectionController : IDeviceCollectionController {
        IAmmoniaOperations Operations { get; }

    }

    public interface IBoxCollectionController : IDeviceCollectionController {
        ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading> Operations { get; }
        GenericBoxReading GetLastReading(string id);

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
}

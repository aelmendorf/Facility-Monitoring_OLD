using FacilityMonitoring.Common.Model;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Hardware {

    public interface IMonitorBoxHardwareOperations {
        GenericMonitorBox Device { get; set; }
        bool SetAlarm(bool on_off);
        bool SetWarning(bool on_off);
        bool SetMaintenance(bool on_off);
        Reading ReadAll();
        Task<Reading> ReadAllAsync();
    }

    public interface IMonitorBoxData {

    }
}

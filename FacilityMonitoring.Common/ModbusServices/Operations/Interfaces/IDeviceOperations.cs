using System.Threading.Tasks;
using FacilityMonitoring.Common.Data.Entities;

namespace FacilityMonitoring.Common.ModbusServices.Operations {
    public interface IDeviceOperations {
        double ReadInterval { get; }
        double SaveInterval { get; }
        Task StartAsync();
        void Start();
        ModbusDevice Device { get;}
        bool Save();
        Task<bool> SaveAsync();
        bool CheckSaveTime();
        void ResetSaveTimer();
    }
}

using System.Threading.Tasks;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {
    public interface IDeviceController {
        double ReadInterval { get; }
        Task StartAsync();
        void Start();
        void Stop();
        Task StopAsync();
        void TimeHandler(object state);
    }
}

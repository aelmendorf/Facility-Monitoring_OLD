using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;

namespace FacilityMonitoring.Common.Server {
    public interface IMonitorBoxHub {
        Task RecieveAutoBoxReading(string data);
        Task RecieveBoxReadingCallBack(string data);
        Task SwitchMaintenanceCallBack(bool success);
        Task RecieveBoxStateCallBack(DeviceState state);
        Task RecieveChannelRawCallBack(string data);
    }

}

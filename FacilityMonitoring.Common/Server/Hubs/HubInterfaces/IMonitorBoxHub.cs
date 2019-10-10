using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Server.Hubs.HubInterfaces;

namespace FacilityMonitoring.Common.Server {
    public interface IMonitorBoxHub: IDeviceHub {
        Task RecieveAutoReading(string data);
        Task RecieveReadingCallBack(string data);
        Task SetMaintenanceCallBack(bool success);
        Task RecieveStateCallBack(DeviceState state);
        Task RecieveChannelRawCallBack(ushort data);
        Task RecieveChannelVoltageCallBack(double data);
    }

}

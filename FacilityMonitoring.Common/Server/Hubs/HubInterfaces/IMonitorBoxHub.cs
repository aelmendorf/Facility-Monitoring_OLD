using System.Threading.Tasks;
using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Server.Hubs.HubInterfaces;

namespace FacilityMonitoring.Common.Server {
    public interface IMonitorBoxHub: IDeviceHub {
        Task RecieveAutoReading(GenericBoxReading data);
        Task RecieveReadingCallBack(GenericBoxReading data);
        Task SetMaintenanceCallBack(bool success);
        Task SetAlarmCallBack(bool success);
        Task SetWarnCallBack(bool success);
        Task RecieveStateCallBack(DeviceState state);
        Task RecieveChannelRawCallBack(ushort data);
        Task RecieveChannelVoltageCallBack(double data);
    }

}

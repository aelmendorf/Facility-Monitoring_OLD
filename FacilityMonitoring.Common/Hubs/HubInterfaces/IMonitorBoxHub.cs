using System.Threading.Tasks;
using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.Data.Entities;

namespace FacilityMonitoring.Common.Hubs {
    public interface IMonitorBoxHub: IDeviceHub {
        Task RecieveAutoReading(BoxReadingDTO data);
        Task RecieveReadingCallBack(BoxReadingDTO data);
        Task SetMaintenanceCallBack(bool success);
        Task SetAlarmCallBack(bool success);
        Task SetWarnCallBack(bool success);
        Task RecieveStateCallBack(DeviceState state);
        Task RecieveChannelRawCallBack(ushort data);
        Task RecieveChannelVoltageCallBack(double data);
    }

}

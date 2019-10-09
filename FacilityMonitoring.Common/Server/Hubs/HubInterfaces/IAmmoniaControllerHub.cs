using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Server.Hubs.HubInterfaces;

namespace FacilityMonitoring.Common.Server {

    public interface IAmmoniaControllerHub:IDeviceHub {
        Task RecieveAutoReading(string data);
        Task RecieveReadingCallBack(string data);
        Task SetCalibrationModeCallBack(bool success);
        Task GetCalibrationCallBack(AmmoniaCalibrationData calData);
        Task SetCalibrationCallBack(bool sucess);
    }

}

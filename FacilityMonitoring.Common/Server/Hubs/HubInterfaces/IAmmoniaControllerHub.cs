using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Server.Hubs.HubInterfaces;

namespace FacilityMonitoring.Common.Server {

    public interface IAmmoniaControllerHub:IDeviceHub {
        Task RecieveAutoReading(IEnumerable<Tank> data);
        Task RecieveReadingCallBack(IEnumerable<Tank> data);
        Task SetCalibrationModeCallBack(bool success);
        Task GetCalibrationCallBack(AmmoniaCalibrationData calData);
        Task SetCalibrationCallBack(bool sucess);
    }

}

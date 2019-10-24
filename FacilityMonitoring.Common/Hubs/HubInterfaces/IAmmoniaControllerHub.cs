using System.Collections.Generic;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Data.DTO;

namespace FacilityMonitoring.Common.Hubs {

    public interface IAmmoniaControllerHub:IDeviceHub {
        Task RecieveAutoReading(IEnumerable<Tank> data);
        Task RecieveReadingCallBack(IEnumerable<Tank> data);
        Task SetCalibrationModeCallBack(bool success);
        Task GetCalibrationCallBack(AmmoniaCalibrationData calData);
        Task SetCalibrationCallBack(bool sucess);
    }

}

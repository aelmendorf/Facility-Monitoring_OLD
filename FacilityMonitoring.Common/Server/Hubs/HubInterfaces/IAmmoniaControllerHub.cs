using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data;

namespace FacilityMonitoring.Common.Server {

    public interface IAmmoniaControllerHub {
        Task SendAmmoniaReading(string data);
        Task RecieveTankCalibration(string data);
    }

}

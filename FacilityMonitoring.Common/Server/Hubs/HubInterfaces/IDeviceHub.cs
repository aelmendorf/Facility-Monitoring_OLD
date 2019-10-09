using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Server.Hubs.HubInterfaces {
    public interface IDeviceHub {
        Task RecieveErrorMessage(string message);
    }
}

using FacilityMonitoring.Common.Server.Hubs.HubInterfaces;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Server {
    public interface IGeneratorHub:IDeviceHub {
        Task SendGeneratorReading(string data);
        Task RecieveMessage(string message);
    }

}

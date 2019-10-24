using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Hubs {
    public interface IDeviceHub {
        Task RecieveErrorMessage(string message);
    }
}

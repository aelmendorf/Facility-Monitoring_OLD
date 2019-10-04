using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Server {
    public interface IGeneratorHub {
        Task SendGeneratorReading(string data);
        Task RecieveMessage(string message);
    }

}

using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Server.Hubs.HubInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Server {
    public interface IGeneratorHub:IDeviceHub {
        Task SendGeneratorReading(GeneratorReadingDTO data);
        Task RecieveMessage(string message);
        Task RecieveAllGenerators(IEnumerable<GeneratorReadingDTO> generators);
    }

}

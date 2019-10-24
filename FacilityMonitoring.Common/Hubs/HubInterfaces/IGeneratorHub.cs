using FacilityMonitoring.Common.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Hubs {
    public interface IGeneratorHub:IDeviceHub {
        Task SendGeneratorReading(GeneratorReadingDTO data);
        Task RecieveMessage(string message);
        Task RecieveAllGenerators(IEnumerable<GeneratorReadingDTO> generators);
    }

}

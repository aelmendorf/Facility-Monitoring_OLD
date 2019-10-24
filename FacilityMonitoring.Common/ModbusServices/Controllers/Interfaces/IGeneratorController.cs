using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.ModbusServices.Operations;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {
    public interface IGeneratorController:IDeviceController {
        ConcurrentDictionary<IGeneratorOperations,H2GenReading> Operations { get; }
        GeneratorReadingDTO GetLastReading(string genId);
        IEnumerable<GeneratorReadingDTO> GetAllGenerators();
    }
}

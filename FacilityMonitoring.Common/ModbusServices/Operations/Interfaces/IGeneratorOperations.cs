using FacilityMonitoring.Common.Data.Entities;
using System.Threading.Tasks;


namespace FacilityMonitoring.Common.ModbusServices.Operations {
    public interface IGeneratorOperations: IDeviceOperations {
        H2GenReading LastReading { get; }
        H2GenReading Read();
        Task<H2GenReading> ReadAsync();
    }
}

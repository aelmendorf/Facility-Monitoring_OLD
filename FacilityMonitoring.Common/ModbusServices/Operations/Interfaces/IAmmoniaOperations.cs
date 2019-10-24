using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Data.Entities;
using System.Threading.Tasks;


namespace FacilityMonitoring.Common.ModbusServices.Operations {
    public interface IAmmoniaOperations:IDeviceOperations {
        TankScaleReading LastReading { get; }
        TankScaleReading Read();
        Task<TankScaleReading> ReadAsync();

        bool SetCalibration(AmmoniaCalibrationData data);
        Task<bool> SetCalibrationAsync(AmmoniaCalibrationData data);
        bool SetCalibrationMode(bool on_off);
        Task<bool> SetCalibrationModeAsync(bool on_off);
    }
}

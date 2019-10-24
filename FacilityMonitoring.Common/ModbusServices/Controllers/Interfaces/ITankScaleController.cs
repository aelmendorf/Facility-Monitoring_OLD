using FacilityMonitoring.Common.ModbusServices.Operations;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {
    public interface ITankScaleController : IDeviceController {
        IAmmoniaOperations Operations { get; }

    }
}

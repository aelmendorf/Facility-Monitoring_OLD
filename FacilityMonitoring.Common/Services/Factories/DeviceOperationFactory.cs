using FacilityMonitoring.Common.Model;
using System;
using FacilityMonitoring.Common.Hardware;

namespace FacilityMonitoring.Common.Services {

    public static class DeviceOperationFactory {
        public static IDeviceOperations OperationFactory(FacilityContext context,ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(GenericMonitorBox)) {
                return new MonitorBoxController(context.GetMonitorBox(device.Identifier,false));
            } else if (type == typeof(H2Generator)) {
                return new GeneratorController(context.GetGenerator(device.Identifier, false));
            } else if (type == typeof(AmmoniaController)) {
                return new NH3Controller(context.GetNHController(device.Identifier, false));
            } else {
                return null;
            }
        }
    }
}

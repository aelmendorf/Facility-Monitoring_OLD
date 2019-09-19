using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.ModbusServices;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FacilityMonitoring.Common.Hardware;

namespace FacilityMonitoring.Common.ServiceLayer {

    public static class DeviceOperationFactory {
        public static IDeviceOperations OperationFactory(ModbusDevice device, IServiceProvider serviceProvider) {
            Type type = device.GetType();
            if (type == typeof(GenericMonitorBox)) {
                return new MonitorBoxOperations(new FacilityContext(),
                                                (GenericMonitorBox)device,
                                                serviceProvider.GetService<ILogger<MonitorBoxOperations>>());
            } else if (type == typeof(H2Generator)) {
                return new GeneratorOperations(new FacilityContext(),
                                               (H2Generator)device,
                                               serviceProvider.GetService<ILogger<GeneratorOperations>>());
            } else if (type == typeof(AmmoniaController)) {
                return new AmmoniaControllerOperations(new FacilityContext(),
                                                (AmmoniaController)device,
                                                serviceProvider.GetService<ILogger<AmmoniaControllerOperations>>());
            } else {
                return null;
            }
        }
    }
}

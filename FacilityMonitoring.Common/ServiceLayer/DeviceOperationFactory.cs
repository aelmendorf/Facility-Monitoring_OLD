using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.ModbusServices;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FacilityMonitoring.Common.Hardware;
using System.Threading.Tasks.Dataflow;
using FacilityMonitoring.Common.DataLayer;

namespace FacilityMonitoring.Common.ServiceLayer {

    public static class DeviceOperationFactory {
        public static IDeviceOperations OperationFactory(FacilityContext context,BufferBlock<IDeviceOperations> buffer, ModbusDevice device, IServiceProvider serviceProvider) {
            Type type = device.GetType();
            if (type == typeof(GenericMonitorBox)) {
                return new MonitorBoxOperations(buffer,context.GetMonitorBox(device.Identifier,false),serviceProvider.GetService<ILogger<MonitorBoxOperations>>(), serviceProvider.GetService<IAddDeviceReading>());
            } else if (type == typeof(H2Generator)) {
                return new GeneratorOperations(buffer, context.GetGenerator(device.Identifier, false), serviceProvider.GetService<ILogger<GeneratorOperations>>(),serviceProvider.GetService<IAddDeviceReading>());
            } else if (type == typeof(AmmoniaController)) {
                return new AmmoniaControllerOperations(buffer,context.GetNHController(device.Identifier, false), serviceProvider.GetService<ILogger<AmmoniaControllerOperations>>(),serviceProvider.GetService<IAddDeviceReading>());
            } else {
                return null;
            }
        }
    }


    //public static class DeviceOperationFactory {
    //    public static IDeviceOperations OperationFactory(FacilityContext context, BufferBlock<IDeviceOperations> buffer, ModbusDevice device, IServiceProvider serviceProvider) {
    //        Type type = device.GetType();
    //        if (type == typeof(GenericMonitorBox)) {
    //            return new MonitorBoxOperations(buffer, (GenericMonitorBox)device, serviceProvider.GetService<ILogger<MonitorBoxOperations>>());
    //        } else if (type == typeof(H2Generator)) {
    //            return new GeneratorOperations(buffer, (H2Generator)device, serviceProvider.GetService<ILogger<GeneratorOperations>>(), serviceProvider.GetService<IAddDeviceReading>());
    //        } else if (type == typeof(AmmoniaController)) {
    //            return new AmmoniaControllerOperations(buffer, (AmmoniaController)device, serviceProvider.GetService<ILogger<AmmoniaControllerOperations>>());
    //        } else {
    //            return null;
    //        }
    //    }
    //}
}

using FacilityMonitoring.Common.Model;
using System;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.ModbusServices.Operations;

namespace FacilityMonitoring.Common.Services {

    public static class DeviceOperationFactory {
        public static IDeviceOperations OperationFactory(FacilityContext context,ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(MonitorBox)) {
                return new MonitorBoxOperations(context.GetMonitorBox(device.Identifier,false));
            } else if (type == typeof(H2Generator)) {
                return new GeneratorOperations(context.GetGenerator(device.Identifier, false));
            } else if (type == typeof(TankScale)) {
                return new AmmoniaOperations(context.GetNHController(device.Identifier, false));
            } else {
                return null;
            }
        }

        internal static GeneratorOperations OperationFactory(FacilityContext context, H2Generator generator) => throw new NotImplementedException();
    }
}

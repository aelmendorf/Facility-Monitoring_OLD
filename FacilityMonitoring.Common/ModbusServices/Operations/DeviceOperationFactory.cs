using FacilityMonitoring.Common.Data.Context;
using System;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.ModbusServices.Operations;
using Microsoft.Extensions.DependencyInjection;
using FacilityMonitoring.Common.Data;
using Microsoft.Extensions.Logging;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using MediatR;

namespace FacilityMonitoring.Common.Services {
    public class DeviceOperationsFactory {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DeviceOperationsFactory> _logger;

        public DeviceOperationsFactory(IServiceProvider serviceProvider,ILogger<DeviceOperationsFactory> logger) {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        public IDeviceOperations GetOperations(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(MonitorBox)) {
                this._logger.LogInformation("Creating MonitorBoxOperations");
                return new MonitorBoxOperations((MonitorBox)device,
                    this._serviceProvider.GetRequiredService<FacilityContext>(),
                    this._serviceProvider.GetRequiredService<IAddMonitorBoxReading>(), 
                    this._serviceProvider.GetRequiredService<ILogger<IMonitorBoxOperations>>(),
                    this._serviceProvider.GetRequiredService<IMediator>());
            } else if (type == typeof(H2Generator)) {
                this._logger.LogInformation("Creating GeneratorOperations");
                return new GeneratorOperations((H2Generator)device, this._serviceProvider.GetRequiredService<IAddGeneratorReading>(), this._serviceProvider.GetRequiredService<ILogger<IGeneratorOperations>>());
            } else if (type == typeof(TankScale)) {
                this._logger.LogInformation("Creating TankScaleOperations");
                return new TankScaleOperations((TankScale)device, this._serviceProvider.GetRequiredService<IAddTankScaleReading>(), this._serviceProvider.GetRequiredService<ILogger<ITankScaleOperations>>());
            } else {
                return null;
            }
        }
    }
}

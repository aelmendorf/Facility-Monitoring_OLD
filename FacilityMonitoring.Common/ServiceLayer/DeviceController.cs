using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.ModbusServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using FacilityMonitoring.Common.ServiceLayer;


namespace FacilityMonitoring.Common.ServiceLayer {
    public class DeviceController {
        private FacilityContext _context;
        private BufferBlock<IDeviceOperations> _operationQueue;
        private Timer _timer;
        private Timer _genTime1, _genTime2, _genTime3, _boxTime, _nhTime;
        private List<IDeviceOperations> _deviceOperations;
        private IServiceProvider _serviceProvider;
        private ILogger _logger;

        public DeviceController() {
            this._context = new FacilityContext();
            this._operationQueue = new BufferBlock<IDeviceOperations>(new DataflowBlockOptions { BoundedCapacity = 5 });
            this._deviceOperations = new List<IDeviceOperations>();
        }

        public async Task Start() {
            var serviceCollection = new ServiceCollection();

            var controller = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<AmmoniaController>()
                .FirstOrDefault(e => e.Identifier == "AmmoniaController");

            var generator1 = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers)
                .FirstOrDefault(e => e.Identifier == "Generator 1");

            var generator2 = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers)
                .FirstOrDefault(e => e.Identifier == "Generator 2");

            var generator3 = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers)
                .FirstOrDefault(e => e.Identifier == "Generator 3");

            var device = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<GenericMonitorBox>()
                .Include(e => e.Registers)
                    .ThenInclude(e => e.SensorType)
                .FirstOrDefault(e => e.Identifier == "GasBay");

            serviceCollection.AddLogging(configure => { configure.AddFile(); configure.AddConsole(); });
            this._serviceProvider = serviceCollection.BuildServiceProvider();

            this._logger = this._serviceProvider.GetService<ILogger<DeviceController>>();

            //this._logger.LogInformation("{0}:Memory Before Init: {1}", DateTime.Now, GC.GetTotalMemory(false));

            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue,controller, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, generator1, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, generator2, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, generator3, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, device, this._serviceProvider));

            foreach(var dev in this._deviceOperations) {
                await dev.Start();
            }
        }

        public async Task Run() {
            while (await this._operationQueue.OutputAvailableAsync()) {
                var operation = await this._operationQueue.ReceiveAsync();
            }
        }
    }
}

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
using FacilityMonitoring.Common.DataLayer;

namespace FacilityMonitoring.Common.ServiceLayer {
    public class DeviceController {
        private FacilityContext _context;
        private BufferBlock<IDeviceOperations> _operationQueue;
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

            //var controller = this._context.GetNHController("AmmoniaController",false);
            //var generator1 = this._context.GetGenerator("Generator 1",false);
            //var generator2 = this._context.GetGenerator("Generator 2", false);
            //var generator3 = this._context.GetGenerator("Generator 3", false);
            //var device = this._context.GetMonitorBox("GasBay", false);

            var devices = this._context.GetAllDevices();


            serviceCollection.AddLogging(configure => { configure.AddFile(); configure.AddConsole();});
            serviceCollection.AddTransient<IAddGeneratorReading,AddGeneratorReading>();
            serviceCollection.AddTransient<IAddNHControllerReading,AddNHControllerReading >();
            serviceCollection.AddTransient<IAddMonitorBoxReading,AddMonitorBoxReading>();
            serviceCollection.AddTransient<IAddDeviceReading, AddDeviceReading>();
            this._serviceProvider = serviceCollection.BuildServiceProvider();
            this._logger = this._serviceProvider.GetService<ILogger<DeviceController>>();


            this._context.ModbusDevices.AsNoTracking().ToList().ForEach(device => {
                this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._context,this._operationQueue,device ,this._serviceProvider));
            });

            //this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue,controller, this._serviceProvider));
            //this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, generator1, this._serviceProvider));
            //this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, generator2, this._serviceProvider));
            //this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, generator3, this._serviceProvider));
            //this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(this._operationQueue, device, this._serviceProvider));

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

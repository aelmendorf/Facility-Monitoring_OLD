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
        private List<IDeviceOperations> _deviceOperations;
        private IServiceProvider _serviceProvider;
        private ILogger _logger;

        public DeviceController() {
            this._context = new FacilityContext();
            this._operationQueue = new BufferBlock<IDeviceOperations>(new DataflowBlockOptions { BoundedCapacity = 5 });
            this._timer = new Timer();
            this._deviceOperations = new List<IDeviceOperations>();
        }

        public void Start() {
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
            this._logger.LogInformation("{0}:Memory Before Init: {1}", DateTime.Now, GC.GetTotalMemory(false));

            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(controller, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(generator1, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(generator2, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(generator3, this._serviceProvider));
            this._deviceOperations.Add(DeviceOperationFactory.OperationFactory(device, this._serviceProvider));
            this._logger.LogInformation("{0}:Memory At Start Timer: {1}", DateTime.Now, GC.GetTotalMemory(false));
            this._timer.AutoReset = true;
            this._timer.Interval = 10000;
            this._timer.Elapsed += this._timer_Elapsed;
            this._timer.Start();
        }

        public async Task Run() {
            while (await this._operationQueue.OutputAvailableAsync()) {
                var operation = await this._operationQueue.ReceiveAsync();
            }
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            this._timer.Enabled = false;
            this._logger.LogInformation("{0}:Timer Handler Memory Before: {1}", DateTime.Now, GC.GetTotalMemory(false));
            List<Task> task = new List<Task>();
            foreach (var operation in this._deviceOperations) {
                task.Add(this.Produce(operation));
            }

            await Task.WhenAll(task);
            this._logger.LogInformation("{0}:Timer Handler Memory After: {1}",DateTime.Now,GC.GetTotalMemory(false));
            this._timer.Enabled = true;
        }

        private async Task Produce(IDeviceOperations operation) {
            if (await operation.ReadAsync()) {
                await operation.SaveAsync();
            }
            await this._operationQueue.SendAsync(operation);
        }
    }
}

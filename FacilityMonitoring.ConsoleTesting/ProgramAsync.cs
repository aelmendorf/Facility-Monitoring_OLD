using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;

namespace FacilityMonitoring.ConsoleTesting {
    class ProgramAsync {
        static async Task<int> Main(string[] args) {
            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection);
            DeviceController controller = new DeviceController();
            controller.Start();
            var task=controller.Run();
            await Task.WhenAll(task);
            return 0;
        }

        static void ConfigureService(IServiceCollection services) {
            services.AddTransient<DeviceController>();
        }
    }

    public class DeviceController {
        private FacilityContext _context;
        private BufferBlock<IDeviceOperations> _operationQueue;
        private Timer _timer;
        private List<IDeviceOperations> _deviceOperations;
        
        public DeviceController() {
            this._context = new FacilityContext();
            this._operationQueue= new BufferBlock<IDeviceOperations>(new DataflowBlockOptions { BoundedCapacity = 5 });
            this._timer = new Timer();
            this._deviceOperations = new List<IDeviceOperations>();
        }

        public void Start() {
            var controller = this._context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
            var generator1 = this._context.ModbusDevices.OfType<H2Generator>().Include(e => e.H2Readings).Include(e => e.Registers).FirstOrDefault(e => e.Identifier == "Generator 1");
            var generator2 = this._context.ModbusDevices.OfType<H2Generator>().Include(e => e.H2Readings).Include(e => e.Registers).FirstOrDefault(e => e.Identifier == "Generator 2");
            var generator3 = this._context.ModbusDevices.OfType<H2Generator>().Include(e => e.H2Readings).Include(e => e.Registers).FirstOrDefault(e => e.Identifier == "Generator 3");
            var device = this._context.ModbusDevices.OfType<GenericMonitorBox>().Include(e => e.Registers)
                .ThenInclude(e => e.SensorType)
            .Include(e => e.BoxReadings)
            .FirstOrDefault(e => e.Identifier == "GasBay");
            this._deviceOperations.Add(this.OperationFactory(controller));
            this._deviceOperations.Add(this.OperationFactory(generator1));
            this._deviceOperations.Add(this.OperationFactory(generator2));
            this._deviceOperations.Add(this.OperationFactory(generator3));
            this._deviceOperations.Add(this.OperationFactory(device));

            this._timer.AutoReset = true;
            this._timer.Interval = 5000;
            this._timer.Elapsed += this._timer_Elapsed;
            this._timer.Start();
        }

        public async Task Run() {
            while (await this._operationQueue.OutputAvailableAsync()) {
                var operation = await this._operationQueue.ReceiveAsync();
                Console.WriteLine("Time: {0} Device: {1} ", DateTime.Now, operation.Device.Identifier);
            }
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            this._timer.Enabled = false;

            List<Task> task = new List<Task>();
            foreach(var operation in this._deviceOperations) {
                task.Add(this.Produce(operation));
            }
            await Task.WhenAll(task);
            this._timer.Enabled = true;
        }

        private async Task Produce(IDeviceOperations operation) {
            var success=await operation.ReadAsync();
            await this._operationQueue.SendAsync(operation);
        }

        private IDeviceOperations OperationFactory(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(GenericMonitorBox)) {
                return new MonitorBoxOperations(new FacilityContext(),(GenericMonitorBox)device);
            } else if (type == typeof(H2Generator)) {
                return new GeneratorOperations(new FacilityContext(), (H2Generator)device);
            } else if (type == typeof(AmmoniaController)) {
                return new AmmoniaControllerOperations(new FacilityContext(), (AmmoniaController)device);
            } else {
                return null;
            }
        }
    }
}

using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services {
    public class GeneratorCollectionController : IGeneratorCollectionController {
        private List<IGeneratorOperations> _generatorOperations;
        private List<Timer> _timers;
        private readonly FacilityContext _context;
        private readonly ILogger<GeneratorCollectionController> _logger;
        private double _readInterval=10.0;

        public GeneratorCollectionController(FacilityContext context, ILogger<GeneratorCollectionController> logger) {
            this._context = context;
            this._logger = logger;
            this._generatorOperations = new List<IGeneratorOperations>();
        }

        public List<IGeneratorOperations> Operations {
            get =>this._generatorOperations;
            private set => this._generatorOperations = value;

        }

        public double ReadInterval {
            get=>this._readInterval;
        }


        public void Start() {
            var generators = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers).ToList();
            foreach (var generator in generators) {
                var controller = (GeneratorOperations)DeviceOperationFactory.OperationFactory(this._context, generator);
                if (controller != null) {
                    this._generatorOperations.Add(controller);
                    controller.Start();
                }
            }
            this._readInterval = this.Operations.Min(gen => gen.ReadInterval);
        }

        public async Task StartAsync() {
            var generators = await this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers).ToListAsync();
            List<Task> taskList = new List<Task>();
            foreach (var generator in generators) {
                var controller = (GeneratorOperations)DeviceOperationFactory.OperationFactory(this._context, generator);
                if (controller != null) {
                    taskList.Add(controller.StartAsync());
                    this._generatorOperations.Add(controller);
                }
            }
            await Task.WhenAll(taskList);
            this._readInterval=this.Operations.Min(gen => gen.ReadInterval);
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("GeneratorController Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("GeneratorController Stopping");
        }
    }
}

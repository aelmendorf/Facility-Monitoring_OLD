using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.ModbusServices.Operations;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Hubs;
using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.Services;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {
    public class GeneratorController : IGeneratorController {
        private static GeneratorController _instance = null;
        private ConcurrentDictionary<IGeneratorOperations,H2GenReading> _generatorOperations;
        private readonly FacilityContext _context;
        private readonly DeviceOperationsFactory _operationsFactory;
        private readonly ILogger<IGeneratorController> _logger;
        private readonly IHubContext<GeneratorHub, IGeneratorHub> _generatorHub;
        private double _readInterval=10.0;

        public GeneratorController(FacilityContext context,DeviceOperationsFactory operationsFactory, ILogger<IGeneratorController> logger, IHubContext<GeneratorHub, IGeneratorHub> generatorHub) {
            this._context = context;
            this._logger = logger;
            this._generatorHub = generatorHub;
            this._operationsFactory = operationsFactory;
            this._generatorOperations = new ConcurrentDictionary<IGeneratorOperations,H2GenReading>();
        }

        public static GeneratorController Instance {
            get => _instance;
        }

        public ConcurrentDictionary<IGeneratorOperations, H2GenReading> Operations {
            get =>this._generatorOperations;
        }

        public double ReadInterval {
            get=>this._readInterval;
        }

        public GeneratorReadingDTO GetLastReading(string genId) {
            var key = this._generatorOperations.Keys.SingleOrDefault(op => op.Device.Identifier == genId);

            if (key == null)
                return null;

            H2GenReading temp = new H2GenReading();
            this._generatorOperations.TryGetValue(key,out temp);
            if(temp!=null) {
                return new GeneratorReadingDTO(temp);
            } else {
                return null;
            }
        }

        public void Start() {
            var generators = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers).ToList();
            foreach (var generator in generators) {
                //var controller = (GeneratorOperations)DeviceOperationFactory.OperationFactory(this._context, generator);
                var operations = (GeneratorOperations)this._operationsFactory.GetOperations(generator);
                if (operations != null) {
                    this._generatorOperations.TryAdd(operations, new H2GenReading());
                    operations.Start();
                }
            }
            this._readInterval = this.Operations.Min(gen => gen.Key.ReadInterval);

        }

        public async Task StartAsync() {
            var generators = await this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers).ToListAsync();

            List<Task> taskList = new List<Task>();
            foreach (var generator in generators) {
                var operations = (GeneratorOperations)this._operationsFactory.GetOperations(generator);
                if (operations != null) {
                    this._generatorOperations.TryAdd(operations, new H2GenReading());
                    await operations.StartAsync();
                }
            }
            await Task.WhenAll(taskList);
            this._readInterval = this.Operations.Min(gen => gen.Key.ReadInterval);
        }

        public async void TimeHandler(object state) {
            List<Task> readTaskList = new List<Task>();
            List<Task> saveTaskList = new List<Task>();
            List<Task> broadcastTaskList = new List<Task>();
            foreach (var operation in this.Operations.Keys) {

                readTaskList.Add(operation.ReadAsync().ContinueWith(async (data) => {
                    if (data.Result!=null) {
                        await this._generatorHub.Clients.All.SendGeneratorReading(new GeneratorReadingDTO(data.Result));
                        this.Operations[operation] = data.Result;
                        this._logger.LogInformation(data.Result.Identifier+" Read Success");
                    } else {
                        this._logger.LogError("Read Error");
                    }
                }, TaskContinuationOptions.OnlyOnRanToCompletion));

                if (operation.CheckSaveTime()) {
                    saveTaskList.Add(operation.SaveAsync());
                }
            }
            await Task.WhenAll(readTaskList);
            await Task.WhenAll(saveTaskList);
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("GeneratorController Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("GeneratorController Stopping");
        }

        public IEnumerable<GeneratorReadingDTO> GetAllGenerators() {
            List<GeneratorReadingDTO> generators = new List<GeneratorReadingDTO>();
            foreach(var pair in this._generatorOperations) {
                generators.Add(new GeneratorReadingDTO(pair.Value));
            }
            return generators.AsEnumerable();
        }
    }
}

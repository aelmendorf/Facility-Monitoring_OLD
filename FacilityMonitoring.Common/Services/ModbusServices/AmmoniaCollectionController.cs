using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services.ModbusServices {
    public class AmmoniaCollectionController : IAmmoniaCollectionController {
        private IAmmoniaOperations _ammoniaOperations;
        private readonly FacilityContext _context;
        private readonly ILogger<IAmmoniaCollectionController> _logger;
        private double _readInterval = 10.0;


        public GeneratorCollectionController(FacilityContext context, ILogger<IAmmoniaCollectionController> logger) {
            this._context = context;
            this._logger = logger;
        }

        public IAmmoniaOperations Operations {
            get => this._ammoniaOperations;
            private set => this._ammoniaOperations = value;
        }

        public double ReadInterval {
            get => this._readInterval;
        }

        public void Start() {
            var ammoniaController = this._context.ModbusDevices.AsNoTracking().OfType<AmmoniaController>().SingleOrDefault();
            if (ammoniaController != null) {
                var operations = (IAmmoniaOperations)DeviceOperationFactory.OperationFactory(this._context, ammoniaController);
                if (operations != null) {
                    this._ammoniaOperations = operations;
                    this._ammoniaOperations.Start();
                    this._readInterval = this._ammoniaOperations.ReadInterval;
                }
            }
        }

        public async Task StartAsync() {
            var ammoniaController = await this._context.ModbusDevices.AsNoTracking().OfType<AmmoniaController>().SingleOrDefaultAsync();
            if (ammoniaController != null) {
                var operations = (IAmmoniaOperations)DeviceOperationFactory.OperationFactory(this._context, ammoniaController);
                if (operations != null) {
                    this._ammoniaOperations = operations;
                    await this._ammoniaOperations.StartAsync();
                    this._readInterval = this._ammoniaOperations.ReadInterval;
                }
            }
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("Ammonia Controller Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("Ammonia Controller Stopping");
        }
    }
}

using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.Hubs;
using FacilityMonitoring.Common.ModbusServices.Operations;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {
    public class TankScaleController : ITankScaleController {
        private ITankScaleOperations _tankScaleOperations;
        private readonly FacilityContext _context;
        private readonly ILogger<ITankScaleController> _logger;
        private readonly IHubContext<TankScaleHub, ITankScaleHub> _tankScaleHub;
        private readonly DeviceOperationsFactory _operationsFactory;
        private double _readInterval = 10.0;


        public TankScaleController(FacilityContext context,DeviceOperationsFactory operationsFactory,ILogger<ITankScaleController> logger, IHubContext<TankScaleHub, ITankScaleHub> ammoniaHub) {
            this._context = context;
            this._logger = logger;
            this._tankScaleHub = ammoniaHub;
            this._operationsFactory = operationsFactory;
        }

        public ITankScaleOperations Operations {
            get => this._tankScaleOperations;
            private set => this._tankScaleOperations = value;
        }

        public double ReadInterval {
            get => this._readInterval;
        }

        public void Start() {
            var ammoniaController = this._context.ModbusDevices.AsNoTracking().OfType<TankScale>().SingleOrDefault();
            if (ammoniaController != null) {

                var operations = (ITankScaleOperations)this._operationsFactory.GetOperations(ammoniaController);
                if (operations != null) {
                    this._tankScaleOperations = operations;
                    this._tankScaleOperations.Start();
                    this._readInterval = this._tankScaleOperations.ReadInterval;
                }
            }
        }

        public async Task StartAsync() {
            var ammoniaController = await this._context.ModbusDevices.AsNoTracking().OfType<TankScale>().SingleOrDefaultAsync();
            if (ammoniaController != null) {
                var operations = (ITankScaleOperations)this._operationsFactory.GetOperations(ammoniaController);
                if (operations != null) {
                    this._tankScaleOperations = operations;
                    await this._tankScaleOperations.StartAsync();
                    this._readInterval = this._tankScaleOperations.ReadInterval;
                    this._logger.LogInformation("TankScaleController Started");
                } else {
                    this._logger.LogError("TankScallController Start Failed");
                }
            }
        }

        public async void TimeHandler(object state) {
            this._logger.LogInformation("{0}: NH3 Controller Service Read,Broadcast, and Save", DateTime.Now);
            var reading=await this._tankScaleOperations.ReadAsync();
            if (reading != null) {
                await this._tankScaleHub.Clients.All.RecieveAutoReading(reading.GetDataTransfer());
                if (this._tankScaleOperations.CheckSaveTime()) {
                    await this._tankScaleOperations.SaveAsync();
                    this._tankScaleOperations.ResetSaveTimer();
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

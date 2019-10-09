using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.Common.Services.ModbusServices {
    public class BoxCollectionController : IBoxCollectionController {
        private List<IGenericBoxOperations> _boxOperations;
        private readonly FacilityContext _context;
        private readonly ILogger<IBoxCollectionController> _logger;
        private double _readInterval = 10.0;

        public BoxCollectionController(FacilityContext context,ILogger<IBoxCollectionController> logger) {
            this._context = context;
            this._logger = logger;
            this._boxOperations = new List<IGenericBoxOperations>();
        }

        public List<IGenericBoxOperations> Operations {
            get => this._boxOperations;
            private set => this._boxOperations = value;
        }

        public double ReadInterval {
            get=>this._readInterval;
        }

        public void Start() {
            var boxes = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers).ToList();
            foreach (var box in boxes) {
                var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context,box);
                if (controller != null) {
                    this._boxOperations.Add(controller);
                    controller.Start();
                }
            }
            this._readInterval = this.Operations.Min(gen => gen.ReadInterval);
        }

        public async Task StartAsync() {
            var boxes = await this._context.ModbusDevices
                .AsNoTracking()
                .OfType<H2Generator>()
                .Include(e => e.Registers).ToListAsync();
            List<Task> taskList = new List<Task>();
            foreach (var box in boxes) {
                var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
                if (controller != null) {
                    this._boxOperations.Add(controller);
                    taskList.Add(controller.StartAsync());
                }
            }
            await Task.WhenAll(taskList);
            this._readInterval = this.Operations.Min(gen => gen.ReadInterval);
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("MonitorBox Controller Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("MonitorBox Controller Stopping");
        }
    }
}

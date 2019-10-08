using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Server.Services {
    public class GeneratorsHubService : IHubServicere {
        private readonly ILogger<GeneratorsHubService> _logger;
        private readonly IHubContext<GeneratorHub, IGeneratorHub> _monitorHub;
        private List<GeneratorController> _controllers;
        private readonly FacilityContext _context;
        private Timer _timer;

        public GeneratorsHubService(ILogger<GeneratorsHubService> logger, IHubContext<GeneratorHub, IGeneratorHub> monitorHub,FacilityContext context) {
            this._logger = logger;
            this._context = context;
            this._monitorHub = monitorHub;
            this._controllers = new List<GeneratorController>();
        }

        public List<GeneratorController> Controllers {
            get;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            var generators = this._context.ModbusDevices
            .AsNoTracking()
            .OfType<H2Generator>()
            .Include(e => e.Registers).ToList();
             //await this._context.GetAllGeneratorsAsync();
            generators.ForEach(generator => {
                var controller = (GeneratorController)DeviceOperationFactory.OperationFactory(this._context, generator);
                if (controller != null) {
                    this._controllers.Add(controller);
                }
            });

            foreach(var generator in this._controllers) {
                await generator.StartAsync();
            }
            this._timer = new Timer(TimerHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._controllers[0].ReadInterval));
            this._logger.LogInformation("{0}:GeneratorHubService Service Started", DateTime.Now);
        }

        public async void TimerHandler(object state) {
            this._logger.LogInformation("{0}:GeneratorHubService Read,Broadcast, and Save", DateTime.Now);
            foreach(var controller in this._controllers) {
                await controller.ReadAsync();
                await this._monitorHub.Clients.All.SendGeneratorReading(controller.Data);
                if (controller.CheckSaveTime()) {
                    await controller.SaveAsync();
                    controller.ResetSaveTimer();
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await Task.Run(() => {
                this._timer?.Change(Timeout.Infinite, 0);
            });
            this._logger.LogInformation("{0}:GeneratorHubService Service Stopping", DateTime.Now);
        }

        public void Dispose() {
            this._timer?.Dispose();
        }
    }

    public class GeneratorHubService : IHubService {
        private readonly ILogger<GeneratorHubService> _logger;
        private readonly IHubContext<GeneratorHub, IGeneratorHub> _monitorHub;
        private GeneratorController _controller;
        private Timer _timer;

        public IDeviceOperations Controller {
            get => this._controller;
            private set => this._controller = value is GeneratorController ? (GeneratorController)value : null;
        }

        public GeneratorHubService(ILogger<GeneratorHubService> logger, IHubContext<GeneratorHub, IGeneratorHub> monitorHub, IDeviceOperations controller) {
            this._logger = logger;
            this.Controller = controller;
            this._monitorHub = monitorHub;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(TimerHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}:GeneratorHubService Service Started", DateTime.Now);
        }

        public async void TimerHandler(object state) {
            this._logger.LogInformation("{0}:GeneratorHubService Read,Broadcast, and Save", DateTime.Now);
            await this._controller.ReadAsync();
            await this._monitorHub.Clients.All.SendGeneratorReading(this._controller.Data);
            if (this._controller.CheckSaveTime()) {
                await this._controller.SaveAsync();
                this._controller.ResetSaveTimer();
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await Task.Run(() => {
                this._timer?.Change(Timeout.Infinite, 0);
            });
            this._logger.LogInformation("{0}:GeneratorHubService Service Stopping", DateTime.Now);
        }

        public void Dispose() {
            this._timer?.Dispose();
        }
    }
}

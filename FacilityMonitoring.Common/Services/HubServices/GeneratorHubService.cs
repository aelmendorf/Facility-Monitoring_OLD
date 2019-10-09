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
    public class GeneratorsHubService : IHubService {
        private readonly ILogger<GeneratorsHubService> _logger;
        private readonly IHubContext<GeneratorHub, IGeneratorHub> _monitorHub;
        private IGeneratorCollectionController _controller;
        private readonly FacilityContext _context;
        private Timer _timer;

        public GeneratorsHubService(ILogger<GeneratorsHubService> logger, IHubContext<GeneratorHub, IGeneratorHub> monitorHub,FacilityContext context,IGeneratorCollectionController controller) {
            this._logger = logger;
            this._context = context;
            this._monitorHub = monitorHub;
            this._controller = controller;
        }


        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(TimerHandler,null,TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}:GeneratorHubService Service Started", DateTime.Now);
        }

        public async void TimerHandler(object state) {
           foreach (var operation in this._controller.Operations) {
                List<Task> readTaskList = new List<Task>();
                List<Task> saveTaskList = new List<Task>();
                List<Task> broadcastTaskList = new List<Task>();
                readTaskList.Add(operation.ReadAsync().ContinueWith(async (data) => {
                    if (!string.IsNullOrEmpty(data.Result)) {
                        await this._monitorHub.Clients.All.SendGeneratorReading(data.Result);
                    }
                }, TaskContinuationOptions.OnlyOnRanToCompletion));

                if (operation.CheckSaveTime()) {
                    saveTaskList.Add(operation.SaveAsync());
                }
                await Task.WhenAll(readTaskList);
                await Task.WhenAll(saveTaskList);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await Task.Run(() => {
                this._timer?.Change(Timeout.Infinite, 0);
            });
            await this._controller.StopAsync();
            this._logger.LogInformation("{0}:GeneratorHubService Service Stopping", DateTime.Now);
        }

        public void Dispose() {
            this._timer?.Dispose();
        }
    }

    public class GeneratorHubService : IHubService {
        private readonly ILogger<GeneratorHubService> _logger;
        private readonly IHubContext<GeneratorHub, IGeneratorHub> _monitorHub;
        private GeneratorOperations _controller;
        private Timer _timer;

        public IDeviceOperations Controller {
            get => this._controller;
            private set => this._controller = value is GeneratorOperations ? (GeneratorOperations)value : null;
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

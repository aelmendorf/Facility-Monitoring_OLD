﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.ServiceLayer;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.Common.Server.Services {
    public class MonitorHubService : IHubService{
        private readonly ILogger<MonitorHubService> _logger;
        private readonly IHubContext<MonitorBoxHub, IMonitorBoxHub> _monitorHub;
        private IBoxCollectionController _controller;
        private Timer _timer;


        public MonitorHubService(ILogger<MonitorHubService> logger, IHubContext<MonitorBoxHub, IMonitorBoxHub> monitorHub, IBoxCollectionController controller) {
            this._logger = logger;
            this._controller = controller;
            _monitorHub = monitorHub;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(TimerHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}:MonitorBoxHub Service Started", DateTime.Now);
        }

        public async void TimerHandler(object state) {
            foreach(var operation in this._controller.Operations) {
                
            }


            this._logger.LogInformation("{0}:MonitorBoxHub Service Read,Broadcast, and Save", DateTime.Now);
            
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await Task.Run(() => {
                this._timer?.Change(Timeout.Infinite, 0);
            });
            this._logger.LogInformation("{0}:MonitorBoxHub Service Stopping", DateTime.Now);
        }

        public void Dispose() {
            this._timer?.Dispose();
        }
    }
}

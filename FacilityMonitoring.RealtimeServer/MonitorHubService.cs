using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.ServiceLayer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.RealtimeServer {
#region Worker
    public class MonitorHubService : IHostedService,IDisposable
    {
        private readonly ILogger<MonitorHubService> _logger;
        private readonly IHubContext<MonitorBoxHub, IMonitorBoxHub> _monitorHub;
        private MonitorBoxController _controller;
        private Timer _timer;

        public MonitorHubService(ILogger<MonitorHubService> logger, IHubContext<MonitorBoxHub, IMonitorBoxHub> monitorHub, MonitorBoxController controller)
        {
            this._logger = logger;
            this._controller = controller;
            _monitorHub = monitorHub;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(TimerHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}:MonitorBoxHub Service Started",DateTime.Now);
        }

        private async void TimerHandler(object state) {
            this._logger.LogInformation("{0}:MonitorBoxHub Service Read,Broadcast, and Save", DateTime.Now);
            await this._controller.ReadAsync();
            await this._monitorHub.Clients.All.SendMonitorBoxReading(this._controller.Data);
            if (this._controller.CheckSaveTime()) {
                await this._controller.SaveAsync();
                this._controller.ResetSaveTimer();
            }
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
#endregion
}
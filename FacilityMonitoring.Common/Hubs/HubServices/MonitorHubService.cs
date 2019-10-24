using System;
using System.Threading;
using System.Threading.Tasks;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.Common.Hubs.HubServices {
    public class MonitorHubService : IHubService{
        private readonly ILogger<MonitorHubService> _logger;
        private IGenericBoxController _controller;
        private Timer _timer;

        public MonitorHubService(ILogger<MonitorHubService> logger, IGenericBoxController controller) {
            this._logger = logger;
            this._controller = controller;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(this._controller.TimeHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}:MonitorBoxHub Service Started", DateTime.Now);
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

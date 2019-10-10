using System;
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
    public class AmmoniaHubService:IHostedService,IDisposable {
        private readonly ILogger<AmmoniaHubService> _logger;
        private readonly IHubContext<AmmoniaControllerHub, IAmmoniaControllerHub> _ammoniaHub;
        private IAmmoniaCollectionController _controller;
        private Timer _timer;

        public AmmoniaHubService(ILogger<AmmoniaHubService> logger, IHubContext<AmmoniaControllerHub, IAmmoniaControllerHub> ammoniaHub, IAmmoniaCollectionController controller) {
            this._logger = logger;
            this._controller = controller;
            this._ammoniaHub = ammoniaHub;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(this._controller.TimeHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}: NH3 Controller Service Started", DateTime.Now);
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await Task.Run(() => {
                this._timer?.Change(Timeout.Infinite, 0);
            });
            this._logger.LogInformation("{0}: NH3 Controller Service Stopping", DateTime.Now);
        }

        public void Dispose() {
            this._timer?.Dispose();
        }

    }
}

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
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<MonitorBoxHub, IMonitorBoxHub> _monitorHub;
        private readonly BufferBlock<MonitorBoxController> _bufferBlock;
        private MonitorBoxController _operation;

        public Worker(ILogger<Worker> logger, IHubContext<MonitorBoxHub, IMonitorBoxHub> monitorHub)
        {
            this._logger = logger;
            this._bufferBlock = new BufferBlock<MonitorBoxController>(new DataflowBlockOptions { BoundedCapacity = 5 });
            this._operation = new MonitorBoxController(monitorHub);
            _monitorHub = monitorHub;

        }

        public override async Task StartAsync(CancellationToken cancellationToken) {
            await this._operation.StartAsync();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {
                 _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                //await _monitorHub.Clients.All.SendMonitorBoxReading(this._operation.GetData());
                await Task.Delay(1000);
            }
        }
    }
#endregion
}
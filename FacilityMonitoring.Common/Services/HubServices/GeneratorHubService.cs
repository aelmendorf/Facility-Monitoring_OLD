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
        //private readonly IHubContext<GeneratorHub, IGeneratorHub> _monitorHub;
        private IGeneratorCollectionController _controller;
        private readonly FacilityContext _context;
        private Timer _timer;

        //public GeneratorsHubService(ILogger<GeneratorsHubService> logger, IHubContext<GeneratorHub, IGeneratorHub> monitorHub,FacilityContext context,IGeneratorCollectionController controller) {
        //    this._logger = logger;
        //    this._context = context;
        //    this._monitorHub = monitorHub;
        //    this._controller = controller;
        //}

        public GeneratorsHubService(ILogger<GeneratorsHubService> logger, FacilityContext context, IGeneratorCollectionController controller) {
            this._logger = logger;
            this._context = context;
            this._controller = controller;
        }


        public async Task StartAsync(CancellationToken cancellationToken) {
            await this._controller.StartAsync();
            this._timer = new Timer(this._controller.TimeHandler,null,TimeSpan.Zero, TimeSpan.FromSeconds(this._controller.ReadInterval));
            this._logger.LogInformation("{0}:GeneratorHubService Service Started", DateTime.Now);
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

        public void TimerHandler(object state) => throw new NotImplementedException();
    }

}

using System;
using System.Threading;
using System.Threading.Tasks;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Collections.Generic;
using System.Collections.Concurrent;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;

namespace FacilityMonitoring.Common.Services {
    public class AlertService:IHostedService,IDisposable{
        private Timer _timer;
        private readonly IEmailService _emailService;
        private readonly ILogger<AlertService> _logger;
        private List<AlertSetting> _alertSettings;
        //private readonly IGeneratorController _generatorController;
        //private readonly IMonitorBoxController _monitorBoxController;
        //private readonly ITankScaleController _tankScaleControlller;
        private readonly FacilityContext _context;

        public AlertService(FacilityContext context,IEmailService emailService,ILogger<AlertService> logger, IGeneratorController generatorController, IMonitorBoxController monitorBoxController, ITankScaleController tankScaleControlller) {
            this._emailService = emailService;
            this._logger = logger;
            this._logger.LogInformation("AlertService Initiated!");
            //this._generatorController = generatorController;
            //this._monitorBoxController = monitorBoxController;
            //this._tankScaleControlller = tankScaleControlller;
            this._context = context;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            this._alertSettings = await this._context.GetAlertSettingsAsync();
            //this._timer = new Timer(this.TimeHandler, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        //public async Task<AlertServiceResponce> Handle(MonitorBoxAlertCommand request, CancellationToken cancellationToken) {
        //    var register = request.Register;
        //    var reading = request.Reading;

        //}

        //public async void TimeHandler(object state) {

        //}

        private void CheckAlerts() {

        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

        public void Dispose() {

        }
    }
}

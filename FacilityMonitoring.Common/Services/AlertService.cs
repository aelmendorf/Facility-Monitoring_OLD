using System;
using System.Threading;
using System.Threading.Tasks;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;

namespace FacilityMonitoring.Common.Services {
    public class AlertService:IHostedService,IDisposable,IRequestHandler<AlertServiceCommand,AlertServiceResponce> {


        private Timer _timer;
        private readonly IEmailService _emailService;
        private readonly ILogger<AlertService> _logger;

        public AlertService(IEmailService emailService,ILogger<AlertService> logger) {
            this._emailService = emailService;
            this._logger = logger;
            this._logger.LogInformation("AlertService Initiated!");
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

        public async void TimerHandler(object state) {

        }

        public async Task<AlertServiceResponce> Handle(AlertServiceCommand request, CancellationToken cancellationToken) {
            string responce = "Not Initialized";
            await this._emailService.SendMessageAsync(request.Message).ContinueWith(res=> {
                if (res.IsCompletedSuccessfully) {
                    responce = "Success";
                } else {
                    responce = "Failed to Send";
                }
            
            });
            AlertServiceResponce res = new AlertServiceResponce() { Responce = responce };
            return res;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

        public void Dispose() {

        }
    }
}

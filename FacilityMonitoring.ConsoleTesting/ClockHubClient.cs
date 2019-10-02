using Console_Table;
using FacilityMonitoring.Common.Server;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacilityMonitoring.ConsoleTesting {
    public partial class MonitorHubClient :IMonitorBoxHub, IHostedService {
        #region ClockHubClientCtor
        private readonly ILogger<MonitorHubClient> _logger;
        private HubConnection _connection;

        public MonitorHubClient(ILogger<MonitorHubClient> logger) {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl)
                .Build();

            _connection.On<string>(Strings.Events.ReadingSent,
                data => _ = SendMonitorBoxReading(data));
        }

        public Task SendMonitorBoxReading(string data) {
            _logger.LogInformation("Some Data {0}", data);

            return Task.CompletedTask;
        }

        //public Task ShowTime(DateTime currentTime) {
        //    _logger.LogInformation("{CurrentTime}", currentTime.ToShortTimeString());
        //    Task.Delay(100);
        //    return Task.CompletedTask;
        //}


        #endregion

        #region StartAsync
        public async Task StartAsync(CancellationToken cancellationToken) {
            // Loop is here to wait until the server is running
            while (true) {
                try {
                    await _connection.StartAsync(cancellationToken);

                    break;
                } catch {
                    await Task.Delay(1000);
                }
            }
        }
        #endregion
        #region StopAsync
        public Task StopAsync(CancellationToken cancellationToken) {
            return _connection.DisposeAsync();
        }
    }
    #endregion
}

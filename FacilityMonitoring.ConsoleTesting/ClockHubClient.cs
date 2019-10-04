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
    public partial class MonitorHubClient : IMonitorBoxHub, IHostedService {

        private readonly ILogger<MonitorHubClient> _logger;
        private HubConnection _connection;

        public MonitorHubClient(ILogger<MonitorHubClient> logger) {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl(HubConstants.HubUrl)
                .Build();
            _connection.On<string>(HubConstants.Events.MonitorReadingSent,
                data => _ = SendMonitorBoxReading(data));
        }

        public Task SendMonitorBoxReading(string data) {
            this._logger.LogInformation("MonitorBox Data {0}", data);
            return Task.CompletedTask;
        }

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

        public Task StopAsync(CancellationToken cancellationToken) {
            return _connection.DisposeAsync();
        }
    }


    public partial class GeneratorHubClient : IGeneratorHub, IHostedService {
        private readonly ILogger<GeneratorHubClient> _logger;
        private HubConnection _connection;

        public GeneratorHubClient(ILogger<GeneratorHubClient> logger) {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl(HubConstants.GeneratorHubUrl)
                .Build();

            this._connection.On<string>(HubConstants.Events.GeneratorReadingSent,
                data => _ = this.SendGeneratorReading(data));
        }

        public Task SendGeneratorReading(string data) {
            this._logger.LogInformation(data);
            return Task.CompletedTask;
        }

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

        public Task StopAsync(CancellationToken cancellationToken) {
            return _connection.DisposeAsync();
        }
    }
}

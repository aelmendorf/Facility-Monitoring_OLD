using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Console_Table;
using FacilityMonitoring.Common.Server;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FacilityMonitoring.ConsoleTesting {
    class Program2 {
        static async Task Main(string[] args) {

            //var connection = new HubConnectionBuilder()
            //    .WithUrl("http://localhost:5000/hubs/monitor")
            //    .Build();

            //connection.On<string>(Strings.Events.ReadingSent, (data) => {
            //    Console.WriteLine(data);
            //});


            //// Loop is here to wait until the server is running
            //while (true) {
            //    try {
            //        await connection.StartAsync();
            //        break;
            //    } catch {
            //        await Task.Delay(1000);
            //    }
            //}

            //Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            //Console.ReadLine();

            var host = new HostBuilder()
                .ConfigureLogging(logging => {
                    logging.AddConsole();
                })
                .ConfigureServices((services) => {
                    services.AddHostedService<MonitorHubClient>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}

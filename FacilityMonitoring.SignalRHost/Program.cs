using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.Hardware;
using Microsoft.Extensions.DependencyInjection;
using FacilityMonitoring.Common.DataLayer;

namespace FacilityMonitoring.SignalRHost {
    public class Program {
        public static async Task Main(string[] args) {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureAppConfiguration((context, config) => {

            }).ConfigureServices((hostContext, services) => {
                services.AddSignalR();
                services.AddTransient<IAddGeneratorReading, AddGeneratorReading>();
                services.AddTransient<IAddNHControllerReading, AddNHControllerReading>();
                services.AddTransient<IAddMonitorBoxReading, AddMonitorBoxReading>();
                services.AddTransient<IAddDeviceReading, AddDeviceReading>();
                services.AddHostedService<MonitorBoxBackground>();
            }).ConfigureWebHostDefaults(webBuilder=> {
                webBuilder.UseStartup<Startup>();
            });
    }
}

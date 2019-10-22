using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FacilityMonitoring.RealtimeServer {
    public class Program {
        public static async Task Main(string[] args) {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration((context, config) => {
                    // Configure the app here.
                })
                .ConfigureServices((hostContext, services) => {
                    //services.AddHostedService<ServiceA>();
                    //services.AddHostedService<ServiceB>();
                })
                // Only required if the service responds to requests.
                .ConfigureWebHostDefaults(webBuilder => {
                    //webBuilder.UseUrls("http://172.20.4.209:443/");
                    webBuilder.UseUrls("http://localhost:5000/");
                    webBuilder.UseStartup<Startup>();
                });
    }
}

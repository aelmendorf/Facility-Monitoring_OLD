using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.Server.Services;
using FacilityMonitoring.Common.ServiceLayer;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMonitoring.RealtimeServer {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSignalR();
            services.AddTransient<FacilityContext>(provider => {
                return new FacilityContext();
            });
            services.AddSingleton<IGeneratorCollectionController,GeneratorCollectionController>();
            services.AddSingleton<IBoxCollectionController, BoxCollectionController>();
            services.AddSingleton<IAmmoniaCollectionController, AmmoniaCollectionController>();
            services.AddHostedService<GeneratorsHubService>();
            services.AddHostedService<MonitorHubService>();
            services.AddHostedService<AmmoniaHubService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                //app.ApplicationServices.GetServices<>
                endpoints.MapHub<MonitorBoxHub>("/hubs/monitor");
                endpoints.MapHub<GeneratorHub>("/hubs/generator");
                endpoints.MapHub<AmmoniaControllerHub>("/hubs/controller");
            });
        }
    }
}


//public void ConfigureServices(IServiceCollection services) {
//    services.AddSignalR();
//    using var context = new FacilityContext();
//    var box = context.GetMonitorBox("GasBay", false);
//    var generator1 = context.GetGenerator("Generator 1", false);

//    if (box != null) {
//        MonitorBoxController controller = new MonitorBoxController(box);
//        services.AddSingleton<MonitorBoxController>(controller);

//    } else {
//        throw new Exception("Error: Specific Monitor Box Not Found");
//    }

//    if (generator1 != null) {
//        GeneratorController controller = new GeneratorController(generator1);
//        services.AddSingleton<GeneratorController>(controller);
//    } else {
//        throw new Exception("Error: Specific Generator Not Found");
//    }

//    services.AddHostedService<MonitorHubService>();
//    services.AddHostedService<GeneratorHubService>();
//}

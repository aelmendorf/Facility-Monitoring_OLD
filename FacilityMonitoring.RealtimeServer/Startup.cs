using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMonitoring.RealtimeServer {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            // Only required if the service uses Razor Pages.
            //services.AddRazorPages()
            //    .AddNewtonsoftJson();
            services.AddSignalR();
            services.AddHostedService<Worker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            // This pipeline is only required if the service uses Razor Pages.

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            //app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapHub<MonitorBoxHub>("/hubs/monitor");
            });
        }
    }
}

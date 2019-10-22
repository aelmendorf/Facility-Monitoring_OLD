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
            services.AddCors();
            services.AddSignalR();
            services.AddTransient<FacilityContext>(provider => {
                return new FacilityContext();
            });
            services.AddSingleton<IGeneratorCollectionController,GeneratorCollectionController>();
            services.AddSingleton<IGenericBoxController, GasBayController>();
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
            app.UseCors(builder => {
                builder.WithOrigins("http://localhost:51864")
                //builder.WithOrigins("http://172.20.4.202")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseEndpoints(endpoints => {
                //app.ApplicationServices.GetServices<>
                endpoints.MapHub<GasBayHub>("/hubs/gasbay");
                endpoints.MapHub<GeneratorHub>("/hubs/generator");
                endpoints.MapHub<AmmoniaControllerHub>("/hubs/ammonia");
            });
        }
    }
}

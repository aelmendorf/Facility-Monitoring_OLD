using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Hubs;
using FacilityMonitoring.Common.Hubs.HubServices;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Services;
using MediatR.Registration;
using MediatR.Pipeline;
using MediatR;

namespace FacilityMonitoring.RealtimeServer {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddCors();
            services.AddSignalR();
            services.AddTransient<FacilityContext>(provider => {
                return new FacilityContext();
            });            
            services.AddTransient<DeviceOperationsFactory>();

            services.AddTransient<IAddMonitorBoxReading, AddMonitorBoxReading>();
            services.AddTransient<IAddTankScaleReading, AddTankScaleReading>();
            services.AddTransient<IAddGeneratorReading, AddGeneratorReading>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddSingleton<StartupHostedServiceCheck>();
            services.AddSingleton<IGeneratorController,GeneratorController>();
            services.AddSingleton<IMonitorBoxController, GasBayController>();
            services.AddSingleton<ITankScaleController, TankScaleController>();
            services.AddHostedService<GeneratorsHubService>();
            services.AddHostedService<MonitorHubService>();
            services.AddHostedService<AmmoniaHubService>();
            services.AddHostedService<AlertService>();
            services.AddMediatR(typeof(AlertService).Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();
            app.UseCors(builder => {
                //builder.WithOrigins("http://localhost:51864")
                builder.WithOrigins("http://172.20.4.202")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseEndpoints(endpoints => {
                //app.ApplicationServices.GetServices<>
                endpoints.MapHub<GasBayHub>(HubConstants.GasBayHubRoute);
                endpoints.MapHub<GeneratorHub>(HubConstants.GeneratorRoute);
                endpoints.MapHub<TankScaleHub>(HubConstants.TankScaleRoute);
            });
        }
    }
}


//services.AddTransient<FacilityContext>(provider => {
//    return new FacilityContext();
//});

//var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("FacilityConnection"));
//builder.Password = Configuration["DBPassword"];
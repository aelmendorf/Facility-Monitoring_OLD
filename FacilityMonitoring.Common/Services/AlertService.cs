using System;
using System.Threading;
using System.Threading.Tasks;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Collections.Generic;
using System.Collections.Concurrent;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace FacilityMonitoring.Common.Services {
    public class AlertService:IHostedService,IDisposable,IRequestHandler<MonitorBoxAlertCommand,bool>{
        private Timer _timer;
        private readonly IEmailService _emailService;
        private readonly ILogger<AlertService> _logger;
        private List<AlertSetting> _alertSettings;
        private List<MonitorBoxAlertCommand> _alerts;
        //private readonly IGeneratorController _generatorController;
        //private readonly IMonitorBoxController _monitorBoxController;
        //private readonly ITankScaleController _tankScaleControlller;
        private readonly FacilityContext _context;
        private readonly StartupHostedServiceCheck _initalized;

        public AlertService(FacilityContext context,IEmailService emailService,ILogger<AlertService> logger,StartupHostedServiceCheck serviceCheck, 
            IGeneratorController generatorController, IMonitorBoxController monitorBoxController, ITankScaleController tankScaleControlller) {
            this._emailService = emailService;
            this._logger = logger;
            this._alerts = new List<MonitorBoxAlertCommand>();
            //this._generatorController = generatorController;
            //this._monitorBoxController = monitorBoxController;
            //this._tankScaleControlller = tankScaleControlller;
            this._context = context;
            this._initalized = serviceCheck;
            this._alertSettings = this._context.AlertSettings.ToList();
            this._logger.LogWarning("AlertService Initiated!");
            this._initalized.AlertHandlerStarted = true;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            //await this._context.AlertSettings.LoadAsync();
            //this._alertSettings=this._context.AlertSettings.ToList();
            //this._alertSettings = this._context.AlertSettings.ToList();
            //this._logger.LogWarning("AlertService Initiated!");
            //this._initalized.AlertHandlerStarted = true;
            return Task.CompletedTask;
        }

        public async Task<bool> Handle(MonitorBoxAlertCommand request, CancellationToken cancellationToken) {
            bool sendEmail = false;
            this._logger.LogWarning("Recieved MonitorBoxAlertCommand");
            var registers = request.AlertRegisters;
            var reading = request.Device.LastRead;
            var alert = reading.MonitorBoxAlert;
            StringBuilder builder = new StringBuilder();
            foreach (var analog in registers.OfType<AnalogChannel>()) {
                bool modified = false;
                var reg =await this._context.Registers.OfType<AnalogChannel>()
                    .Include(e=>e.SensorType)
                    .SingleOrDefaultAsync(e => e.Id == analog.Id);
                if (reg != null) {
                    if (!reg.Bypass) {
                        AlertSetting action;
                        var analogAlert = ((AnalogAlert)alert[reg.PropertyMap]);
                        switch (analogAlert) {
                            case AnalogAlert.ALARM1:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.Alarm1Action);
                                break;
                            case AnalogAlert.ALARM2:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.Alarm2Action);
                                break;
                            case AnalogAlert.ALARM3:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.Alarm3Action);
                                break;
                            case AnalogAlert.NONE:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == AlertAction.NOTHING);
                                break;
                            default:
                                action = null;
                                break;
                        }
                        if (action.Notification == NotificationType.EMAIL) {
                            if (reg.LastAlert.HasValue) {
                                if (reg.PreviousAlert!=analogAlert) {
                                    builder.AppendFormat("{3}: Detector: {0} Alarm: {1} Value: {2}", reg.Name, reg.SensorType.Name, (double)reading[reg.PropertyMap], action.AlertAction.ToString()).AppendLine();
                                    reg.LastAlert = DateTime.Now;
                                    reg.PreviousAlert = analogAlert;
                                    modified = true;
                                    sendEmail = true;
                                    this._context.Entry<AnalogChannel>(reg).State = EntityState.Modified;
                                } else {
                                    if ((DateTime.Now - reg.LastAlert.Value).TotalMinutes >= action.Frequency) {
                                        builder.AppendFormat("{3}: Detector: {0} Alarm: {1} Value: {2}", reg.Name, reg.SensorType.Name, (double)reading[reg.PropertyMap], action.AlertAction.ToString()).AppendLine();
                                        reg.LastAlert = DateTime.Now;
                                        reg.PreviousAlert = analogAlert;
                                        modified = true;
                                        sendEmail = true;
                                        this._context.Entry<AnalogChannel>(reg).State = EntityState.Modified;
                                    }
                                }
                            } else {
                                builder.AppendFormat("{3}: Detector: {0} Alarm: {1} Value: {2}", reg.Name, reg.SensorType.Name, (double)reading[reg.PropertyMap], action.AlertAction.ToString()).AppendLine();
                                reg.LastAlert = DateTime.Now;
                                reg.PreviousAlert = analogAlert;
                                modified = true;
                                sendEmail = true;
                                this._context.Entry<AnalogChannel>(reg).State = EntityState.Modified;
                            }
                        }
                        if (modified)
                            await this._context.SaveChangesAsync();
                    }

                }

            }

            foreach (var digital in registers.OfType<DigitalInputChannel>()) {
                bool modified = false;
                var reg = await this._context.Registers.OfType<DigitalInputChannel>()
                    .SingleOrDefaultAsync(e => e.Id == digital.Id);
                if (reg != null) {
                    if (!reg.Bypass) {
                        AlertSetting action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.AlarmAction);
                        if (action.Notification == NotificationType.EMAIL) {
                            if (reg.LastAlert.HasValue) {
                                if ((DateTime.Now - reg.LastAlert.Value).TotalMinutes >= action.Frequency) {
                                    builder.AppendFormat("{0}: Item: {1}", action.AlertAction.ToString(), reg.Name);
                                    reg.LastAlert = DateTime.Now;
                                    modified = true;
                                    sendEmail = true;
                                    this._context.Entry<DigitalInputChannel>(reg).State = EntityState.Modified;
                                }
                            } else {
                                builder.AppendFormat("{0}: Item: {1}", action.AlertAction.ToString(), reg.Name);
                                reg.LastAlert = DateTime.Now;
                                modified = true;
                                sendEmail = true;
                                this._context.Entry<DigitalInputChannel>(reg).State = EntityState.Modified;
                            }
                        }
                        if (modified)
                            await this._context.SaveChangesAsync();
                    }
                }
            }
            if (sendEmail)
                await this._emailService.SendMessageAsync(builder.ToString());

            return true;
        }



        private void CheckAlerts() {

        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

        public void Dispose() {

        }
    }
}

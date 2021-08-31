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
        private readonly IMessageBuilder _messageBuilder;
        private List<AlertSetting> _alertSettings;
        private List<MonitorBoxAlertCommand> _alerts;

        //private readonly IGeneratorController _generatorController;
        //private readonly IMonitorBoxController _monitorBoxController;
        //private readonly ITankScaleController _tankScaleControlller;
        private readonly FacilityContext _context;
        private readonly StartupHostedServiceCheck _initalized;

        public AlertService(FacilityContext context,IEmailService emailService,ILogger<AlertService> logger,StartupHostedServiceCheck serviceCheck,IMessageBuilder messageBuilder) {
            this._emailService = emailService;
            this._messageBuilder = messageBuilder;
            this._logger = logger;
            this._alerts = new List<MonitorBoxAlertCommand>();
            this._context = context;
            this._initalized = serviceCheck;
            this._alertSettings = this._context.AlertSettings.ToList();
            this._logger.LogWarning("AlertService Initiated!");
            this._initalized.AlertHandlerStarted = true;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

        public async Task<bool> Handle(MonitorBoxAlertCommand request, CancellationToken cancellationToken) {
            bool sendEmail = false;
            this._logger.LogWarning("Recieved MonitorBoxAlertCommand");
            var registers = request.AlertRegisters;
            var reading = request.Device.LastRead;
            var alert = reading.MonitorBoxAlert;
            var statReg = request.AllReg;
            StringBuilder builder = new StringBuilder();
            this._messageBuilder.StartMessage();
            foreach (var analog in registers.OfType<AnalogChannel>()) {
                bool modified = false;
                var reg =await this._context.Registers.OfType<AnalogChannel>()
                    .Include(e=>e.SensorType)
                    .SingleOrDefaultAsync(e => e.Id == analog.Id);
                if (reg != null) {
                    if (!reg.Bypass) {
                        AlertSetting action;
                        var analogAlert = ((AnalogAlert)alert[reg.PropertyMap]);
                        string alertName = "";
                        switch (analogAlert) {
                            case AnalogAlert.ALARM1:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.Alarm1Action);
                                alertName = "Warning";
                                break;
                            case AnalogAlert.ALARM2:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.Alarm2Action);
                                alertName = "Alarm 1";
                                break;
                            case AnalogAlert.ALARM3:
                                action = this._alertSettings.SingleOrDefault(e => e.AlertAction == reg.Alarm3Action);
                                alertName = "Alarm 2";
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
                                    this._messageBuilder.AppendAlert(reg.Name, alertName , ((double)reading[reg.PropertyMap]).ToString());
                                    reg.LastAlert = DateTime.Now;
                                    reg.PreviousAlert = analogAlert;
                                    modified = true;
                                    sendEmail = true;
                                    this._context.Entry<AnalogChannel>(reg).State = EntityState.Modified;
                                } else {
                                    if ((DateTime.Now - reg.LastAlert.Value).TotalMinutes >= action.Frequency) {
                                        this._messageBuilder.AppendAlert(reg.Name, alertName, ((double)reading[reg.PropertyMap]).ToString());
                                        reg.LastAlert = DateTime.Now;
                                        reg.PreviousAlert = analogAlert;
                                        modified = true;
                                        sendEmail = true;
                                        this._context.Entry<AnalogChannel>(reg).State = EntityState.Modified;
                                    }
                                }
                            } else {
                                this._messageBuilder.AppendAlert(reg.Name, alertName, ((double)reading[reg.PropertyMap]).ToString());
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
                                    this._messageBuilder.AppendAlert(reg.Name,"Alarm", "Tripped");
                                    reg.LastAlert = DateTime.Now;
                                    modified = true;
                                    sendEmail = true;
                                    this._context.Entry<DigitalInputChannel>(reg).State = EntityState.Modified;
                                }
                            } else {
                                this._messageBuilder.AppendAlert(reg.Name,"Alarm", "Tripped");
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
            if (sendEmail) {

                foreach(var analog in statReg.OfType<AnalogChannel>()) {
                    this._messageBuilder.AppendStatus(analog.Name, ((double)reading[analog.PropertyMap]).ToString());
                }

                foreach (var digital in statReg.OfType<DigitalInputChannel>()) {
                    var trigger =(bool)reading[digital.PropertyMap];
                    string value;
                    if (digital.Logic == LogicType.HIGH) {
                        value = (trigger) ? "Tripped" : "Okay";
                    } else {
                        value = (!trigger) ? "Tripped" : "Okay";
                    }
                    this._messageBuilder.AppendStatus(digital.Name,value);
                }

                await this._emailService.SendMessageAsync(this._messageBuilder.FinishMessage());
            }
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

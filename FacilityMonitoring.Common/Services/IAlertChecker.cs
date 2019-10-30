using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacilityMonitoring.Common.Services {
    public interface IAlertChecker {
        List<AlertSetting> AlertSettings { get; set; }
        void CheckAlerts(ModbusDevice device);

    }

    public class AlertChecker : IAlertChecker {
        private List<AlertSetting> _alertSettings;
        private readonly FacilityContext _context;

        public List<AlertSetting> AlertSettings {
            get {
                this._alertSettings ??= this._context.GetAlertSettings();
                return this._alertSettings;
            }
            set => _alertSettings = value;
        }

        public AlertChecker(FacilityContext context) {
            this._context = context;
            this._alertSettings = this._context.GetAlertSettings();
        }

        public void CheckAlerts(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(MonitorBox)) {
                this.CheckMonitorBox((MonitorBox)device);
            } else if (type == typeof(H2Generator)) {
                this.CheckGenerator((H2Generator)device);
            } else if (type == typeof(TankScale)) {
                this.CheckTankScale((TankScale)device);
            }
        }

        public void CheckMonitorBox(MonitorBox device) {
            var reg = device.Registers.Where(e => e.Connected).ToList();
            foreach(var val in reg) {
                if (reg.GetType() == typeof(AnalogChannel)) {

                } else if(reg.GetType()==typeof(DigitalInputChannel)) {

                }
            }
        }

        public void CheckGenerator(H2Generator device) {

        }

        public void CheckTankScale(TankScale device) {

        }
    }


}

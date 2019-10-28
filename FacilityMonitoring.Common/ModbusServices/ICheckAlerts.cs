using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.ModbusServices {
    //public interface ICheckAlerts {
    //    IList<AlertSettings> AlertSettings { get; }

    //}

    //public class AlertChecker {
    //    private AlertChecker _instance = null;
    //    private List<AlertSettings> _alertSettings;
    //    private readonly ILogger<AlertChecker> _logger;
    //    private readonly IEmailService _emailService;
    //    private readonly FacilityContext _context;

    //    public AlertChecker(FacilityContext context,ILogger<AlertChecker> logger,IEmailService emailService) {
    //        this._logger = logger;
    //        this._emailService = emailService;
    //        this._context = context;
    //    }


    //    /// <summary>Checks for alerts</summary>
    //    /// <param name="device">Device of Type ModbusDevice</param>
    //    public async Task CheckAlerts(ModbusDevice device) {
    //        Type type = device.GetType();
    //        if (type == typeof(MonitorBox)) {


    //        }else if (type == typeof(H2Generator)) {
                
    //        }else if (type == typeof(TankScale)) {

    //        }

    //    }

    //    public async Task CheckMonitorBoxAlert(ModbusDevice device) {
    //        List<Register> registers;
    //    }

    //    public async Task CheckGeneratorAlert(ModbusDevice device) {
    //        List<Register> registers;
    //    }

    //    public async Task CheckTankScaleAlert(ModbusDevice device) {

    //    }
    //}
}

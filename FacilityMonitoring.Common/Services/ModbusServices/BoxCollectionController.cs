using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.DataLayer.DTOs;
using FacilityMonitoring.Common.DataLayer;

namespace FacilityMonitoring.Common.Services {

    public class GasBayController : IGenericBoxController {
        private readonly FacilityContext _context;
        private readonly ILogger<IGenericBoxController> _logger;
        private readonly IHubContext<GasBayHub, IMonitorBoxHub> _boxHub;
        private IGenericBoxOperations _operations;

        private double _readInterval = 10.0;

        public GasBayController(FacilityContext context, ILogger<IGenericBoxController> logger, IHubContext<GasBayHub, IMonitorBoxHub> boxHub) {
            this._context = context;
            this._logger = logger;
            this._boxHub = boxHub;
        }

        public IGenericBoxOperations Operations {
            get => this._operations;
        }

        public double ReadInterval {
            get => this._readInterval;
        }

        public void Start() {
            var box = this._context.ModbusDevices
            .AsNoTracking()
            .OfType<GenericMonitorBox>()
            .Include(e => e.Registers)
                .ThenInclude(e => e.SensorType).SingleOrDefault(e=>e.Identifier=="GasBay");
            var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
            if (controller != null) {
                this._operations = controller;
                controller.Start();
                this._readInterval = this._operations.ReadInterval;
                this._logger.LogInformation("GasBay service started");
            } else {
                this._logger.LogError("{0}: Error starting GasBay service");
            }
        }

        public async Task StartAsync() {
            this._context.Registers.Include(e => e.SensorType).Load();
            var box = await this._context.ModbusDevices
            .AsNoTracking()
            .OfType<GenericMonitorBox>()
            .Include(e => e.Registers)
                .ThenInclude(e => e.SensorType).SingleOrDefaultAsync(e => e.Identifier == "GasBay");
            var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
            if (controller != null) {
                this._operations = controller;
                await controller.StartAsync();
                this._readInterval = this._operations.ReadInterval;
                this._logger.LogInformation("GasBay service started");
            } else {
                this._logger.LogError("{0}: Error starting GasBay service");
            }
        }

        public async void TimeHandler(object state) {
            var reading = await this._operations.ReadAsync();
            await this._boxHub.Clients.All.RecieveAutoReading(reading);
            if (this._operations.CheckSaveTime()) {
                await this._operations.SaveAsync();
                this._operations.ResetSaveTimer();
            }
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("MonitorBox Controller Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("MonitorBox Controller Stopping");
        }

        public bool SetAlarm( bool on_off) {
            return this._operations.SetAlarm(on_off);
        }

        public async Task<bool> SetAlarmAsync(bool on_off) {
            return await this._operations.SetAlarmAsync(on_off);
        }

        public bool SetWarning(bool on_off) {
            return this._operations.SetWarning(on_off);
        }

        public async Task<bool> SetWarningAsync(bool on_off) {
            return await this._operations.SetWarningAsync(on_off);
        }

        public bool SetMaintenance(bool on_off) {
            return this._operations.SetMaintenance(on_off);
        }

        public async Task<bool> SetMaintenanceAsync(bool on_off) {
            return await this._operations.SetMaintenanceAsync(on_off);
        }

        public bool GetDeviceState(out DeviceState state) {
            state = this._operations.Device.State;
            return true;
        }

        public ushort GetAnalogChannelRaw(int channel) {
            return this._operations.GetAnalogChannelRaw(channel);
        }

        public async Task<ushort> GetAnalogChannelRawAsync(int channel) {
            return await this._operations.GetAnalogChannelRawAsync(channel);
        }

        public async Task<double> GetAnalogChannelVoltageAsync(int channel) {
            return await this._operations.GetAnalogChannelVoltageAsync(channel);
        }

        public double GetAnalogChannelVoltage(int channel) {
            return this._operations.GetAnalogChannelVoltage(channel);
        }

        public BoxReadingDTO GetDeviceTable() {
            return this._operations.DeviceTable;
        }

        public GenericBoxReading GetCurrentReading() {
            return this._operations.LastReading;
        }
    }
}

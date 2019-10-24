using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Hubs;
using FacilityMonitoring.Common.ModbusServices.Operations;
using FacilityMonitoring.Common.Services;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.Data.DTO;

namespace FacilityMonitoring.Common.ModbusServices.Controllers {

    public class GasBayController : IMonitorBoxController {
        private readonly FacilityContext _context;
        private readonly ILogger<IMonitorBoxController> _logger;
        private readonly IHubContext<GasBayHub, IMonitorBoxHub> _boxHub;
        private readonly DeviceOperationsFactory _operationsFactory;
        private IMonitorBoxOperations _gasBayOperations;
        private double _readInterval = 10.0;

        public GasBayController(FacilityContext context,DeviceOperationsFactory operationsFactory,ILogger<IMonitorBoxController> logger, IHubContext<GasBayHub, IMonitorBoxHub> boxHub) {
            this._context = context;
            this._logger = logger;
            this._boxHub = boxHub;
            this._operationsFactory = operationsFactory;
        }

        public IMonitorBoxOperations Operations {
            get => this._gasBayOperations;
        }

        public double ReadInterval {
            get => this._readInterval;
        }

        public void Start() {
            var box = this._context.ModbusDevices
            .AsNoTracking()
            .OfType<MonitorBox>()
            .Include(e => e.Registers)
                .ThenInclude(e => e.SensorType).SingleOrDefault(e=>e.Identifier=="GasBay");
            var controller = (IMonitorBoxOperations)this._operationsFactory.GetOperations(box);
            if (controller != null) {
                this._gasBayOperations = controller;
                controller.Start();
                this._readInterval = this._gasBayOperations.ReadInterval;
                this._logger.LogInformation("GasBay service started");
            } else {
                this._logger.LogError("{0}: Error starting GasBay service");
            }
        }

        public async Task StartAsync() {
            this._context.Registers.Include(e => e.SensorType).Load();
            var box = await this._context.ModbusDevices
            .AsNoTracking()
            .OfType<MonitorBox>()
            .Include(e => e.Registers)
                .ThenInclude(e => e.SensorType).SingleOrDefaultAsync(e => e.Identifier == "GasBay");
            
            var controller = (IMonitorBoxOperations)this._operationsFactory.GetOperations(box);
            if (controller != null) {
                this._gasBayOperations = controller;
                await controller.StartAsync();
                this._readInterval = this._gasBayOperations.ReadInterval;
                this._logger.LogInformation("GasBay service started");
            } else {
                this._logger.LogError("{0}: Error starting GasBay service");
            }
        }

        public async void TimeHandler(object state) {
            var reading = await this._gasBayOperations.ReadAsync();
            await this._boxHub.Clients.All.RecieveAutoReading(reading);
            if (this._gasBayOperations.CheckSaveTime()) {
                await this._gasBayOperations.SaveAsync();
                this._gasBayOperations.ResetSaveTimer();
            }
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("MonitorBox Controller Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("MonitorBox Controller Stopping");
        }

        public bool SetAlarm( bool on_off) {
            return this._gasBayOperations.SetAlarm(on_off);
        }

        public async Task<bool> SetAlarmAsync(bool on_off) {
            return await this._gasBayOperations.SetAlarmAsync(on_off);
        }

        public bool SetWarning(bool on_off) {
            return this._gasBayOperations.SetWarning(on_off);
        }

        public async Task<bool> SetWarningAsync(bool on_off) {
            return await this._gasBayOperations.SetWarningAsync(on_off);
        }

        public bool SetMaintenance(bool on_off) {
            return this._gasBayOperations.SetMaintenance(on_off);
        }

        public async Task<bool> SetMaintenanceAsync(bool on_off) {
            return await this._gasBayOperations.SetMaintenanceAsync(on_off);
        }

        public bool GetDeviceState(out DeviceState state) {
            state = this._gasBayOperations.Device.State;
            return true;
        }

        public ushort GetAnalogChannelRaw(int channel) {
            return this._gasBayOperations.GetAnalogChannelRaw(channel);
        }

        public async Task<ushort> GetAnalogChannelRawAsync(int channel) {
            return await this._gasBayOperations.GetAnalogChannelRawAsync(channel);
        }

        public async Task<double> GetAnalogChannelVoltageAsync(int channel) {
            return await this._gasBayOperations.GetAnalogChannelVoltageAsync(channel);
        }

        public double GetAnalogChannelVoltage(int channel) {
            return this._gasBayOperations.GetAnalogChannelVoltage(channel);
        }

        public BoxReadingDTO GetDeviceTable() {
            return this._gasBayOperations.DeviceTable;
        }

        public MonitorBoxReading GetCurrentReading() {
            return this._gasBayOperations.LastReading;
        }
    }
}

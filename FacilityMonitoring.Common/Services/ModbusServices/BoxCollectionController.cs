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
            var boxes = this._context.ModbusDevices
            .AsNoTracking()
            .OfType<GenericMonitorBox>()
            .Include(e => e.Registers)
                .ThenInclude(e => e.SensorType).ToList();
            foreach (var box in boxes) {
                var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
                if (controller != null) {
                    this._operations = controller;
                    controller.Start();
                }
            }
            this._readInterval = this._operations.ReadInterval;
        }

        public async Task StartAsync() {
            this._context.Registers.Include(e => e.SensorType).Load();
            var boxes = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<GenericMonitorBox>()
                .Include(e => e.Registers)
                    .ThenInclude(e => e.SensorType).ToList();
            List<Task> taskList = new List<Task>();
            foreach (var box in boxes) {
                var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
                if (controller != null) {
                    this._operations = controller;
                    taskList.Add(controller.StartAsync());
                }
            }
            await Task.WhenAll(taskList);
            this._readInterval = this._operations.ReadInterval;
        }

        public async void TimeHandler(object state) {
            var reading = await this._operations.ReadAsync();
            await this._boxHub.Clients.All.RecieveAutoReading(reading);
            if (this._operations.CheckSaveTime()) {
                await this._operations.SaveAsync();
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

    //public class BoxCollectionController : IBoxCollectionController {
    //    private ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading> _boxOperations;
    //    private readonly FacilityContext _context;
    //    private readonly ILogger<IGenericBoxController> _logger;
    //    private readonly IHubContext<MonitorBoxHub, IMonitorBoxHub> _boxHub;

    //    private double _readInterval = 10.0;

    //    public BoxCollectionController(FacilityContext context, ILogger<IGenericBoxController> logger, IHubContext<MonitorBoxHub, IMonitorBoxHub> boxHub) {
    //        this._context = context;
    //        this._logger = logger;
    //        this._boxHub = boxHub;
    //        this._boxOperations = new ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading>();
    //    }

    //    public ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading> Operations {
    //        get => this._boxOperations;
    //    }

    //    public double ReadInterval {
    //        get => this._readInterval;
    //    }

    //    public void Start() {
    //        var boxes = this._context.ModbusDevices
    //        .AsNoTracking()
    //        .OfType<GenericMonitorBox>()
    //        .Include(e => e.Registers)
    //            .ThenInclude(e => e.SensorType).ToList();
    //        foreach (var box in boxes) {
    //            var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
    //            if (controller != null) {
    //                this._boxOperations.TryAdd(controller, new GenericBoxReading());
    //                controller.Start();
    //            }
    //        }
    //        this._readInterval = this.Operations.Min(gen => gen.Key.ReadInterval);
    //    }

    //    public async Task StartAsync() {
    //        this._context.Registers.Include(e => e.SensorType).Load();
    //        var boxes = this._context.ModbusDevices
    //            .AsNoTracking()
    //            .OfType<GenericMonitorBox>()
    //            .Include(e => e.Registers)
    //                .ThenInclude(e => e.SensorType).ToList();
    //        List<Task> taskList = new List<Task>();
    //        foreach (var box in boxes) {
    //            var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
    //            if (controller != null) {
    //                this._boxOperations.TryAdd(controller, new GenericBoxReading());
    //                taskList.Add(controller.StartAsync());
    //            }
    //        }
    //        await Task.WhenAll(taskList);
    //        this._readInterval = this.Operations.Min(gen => gen.Key.ReadInterval);
    //    }

    //    public void TimeHandler(object state) {
    //        List<Task> readTasks = new List<Task>();
    //        List<Task> saveTasks = new List<Task>();
    //        foreach (var operation in this.Operations.Keys) {
    //            readTasks.Add(operation.ReadAsync().ContinueWith(async (data) => {
    //                if (data.Result != null) {
    //                    this._boxOperations[operation] = data.Result;
    //                    await this._boxHub.Clients.All.RecieveAutoReading(data.Result);
    //                }
    //            }));
    //            if (operation.CheckSaveTime()) {
    //                saveTasks.Add(operation.SaveAsync());
    //            }

    //        }

    //        Task.WhenAll(readTasks);
    //        Task.WhenAll(saveTasks);
    //    }

    //    public async Task StopAsync() {
    //        await Task.Run(() => this._logger.LogInformation("MonitorBox Controller Stopping"));
    //    }

    //    public void Stop() {
    //        this._logger.LogInformation("MonitorBox Controller Stopping");
    //    }

    //    public bool SetAlarm(string id, bool on_off) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return false;

    //        return boxOperation.SetAlarm(on_off);
    //    }

    //    public async Task<bool> SetAlarmAsync(string id, bool on_off) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return false;

    //        return await boxOperation.SetAlarmAsync(on_off);
    //    }

    //    public bool SetWarning(string id, bool on_off) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return false;

    //        return boxOperation.SetWarning(on_off);
    //    }

    //    public async Task<bool> SetWarningAsync(string id, bool on_off) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return false;

    //        return await boxOperation.SetWarningAsync(on_off);
    //    }

    //    public bool SetMaintenance(string id, bool on_off) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return false;

    //        return boxOperation.SetMaintenance(on_off);
    //    }

    //    public async Task<bool> SetMaintenanceAsync(string id, bool on_off) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return false;

    //        return await boxOperation.SetMaintenanceAsync(on_off);
    //    }

    //    public bool GetDeviceState(string id, out DeviceState state) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null) {
    //            state = DeviceState.OKAY;
    //            return false;
    //        }
    //        state = boxOperation.Device.State;
    //        return true;
    //    }

    //    public ushort GetAnalogChannelRaw(string id, int channel) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return 0;

    //        return boxOperation.GetAnalogChannelRaw(channel);
    //    }

    //    public async Task<ushort> GetAnalogChannelRawAsync(string id, int channel) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return 0;

    //        return await boxOperation.GetAnalogChannelRawAsync(channel);
    //    }

    //    public async Task<double> GetAnalogChannelVoltageAsync(string id, int channel) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return 0;

    //        return await boxOperation.GetAnalogChannelVoltageAsync(channel);
    //    }

    //    public double GetAnalogChannelVoltage(string id, int channel) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return 0;

    //        return boxOperation.GetAnalogChannelVoltage(channel);
    //    }

    //    public BoxReadingDTO GetDeviceTable(string id) {
    //        var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (boxOperation == null)
    //            return null;

    //        return boxOperation.DeviceTable;
    //    }

    //    public GenericBoxReading GetCurrentReading(string id) {
    //        var key = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
    //        if (key == null)
    //            return null;

    //        GenericBoxReading temp = new GenericBoxReading();
    //        this._boxOperations.TryGetValue(key, out temp);
    //        return temp;
    //    }
    //}
}

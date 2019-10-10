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

namespace FacilityMonitoring.Common.Services {
    public class BoxCollectionController : IBoxCollectionController {
        private ConcurrentDictionary<IGenericBoxOperations,GenericBoxReading> _boxOperations;
        private readonly FacilityContext _context;
        private readonly ILogger<IBoxCollectionController> _logger;
        private readonly IHubContext<MonitorBoxHub, IMonitorBoxHub> _boxHub;
        private double _readInterval = 10.0;

        public BoxCollectionController(FacilityContext context,ILogger<IBoxCollectionController> logger, IHubContext<MonitorBoxHub, IMonitorBoxHub> boxHub) {
            this._context = context;
            this._logger = logger;
            this._boxHub = boxHub;
            this._boxOperations = new ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading>();
        }

        public ConcurrentDictionary<IGenericBoxOperations, GenericBoxReading> Operations {
            get => this._boxOperations;
        }

        public double ReadInterval {
            get=>this._readInterval;
        }

        public void Start() {
            var boxes=this._context.ModbusDevices
            .AsNoTracking()
            .OfType<GenericMonitorBox>()
            .Include(e => e.Registers)
                .ThenInclude(e => e.SensorType).ToList();
            foreach (var box in boxes) {
                var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context,box);
                if (controller != null) {
                    this._boxOperations.TryAdd(controller,new GenericBoxReading());
                    controller.Start();
                }
            }
            this._readInterval = this.Operations.Min(gen => gen.Key.ReadInterval);
        }

        public async Task StartAsync() {
            this._context.Registers.Include(e=>e.SensorType).Load();
            var boxes = this._context.ModbusDevices
                .AsNoTracking()
                .OfType<GenericMonitorBox>()
                .Include(e => e.Registers)
                    .ThenInclude(e => e.SensorType).ToList();
            List<Task> taskList = new List<Task>();
            foreach (var box in boxes) {
                var controller = (IGenericBoxOperations)DeviceOperationFactory.OperationFactory(this._context, box);
                if (controller != null) {
                    this._boxOperations.TryAdd(controller, new GenericBoxReading());
                    taskList.Add(controller.StartAsync());
                }
            }
            await Task.WhenAll(taskList);
            this._readInterval = this.Operations.Min(gen => gen.Key.ReadInterval);
        }

        public void TimeHandler(object state) {
            List<Task> readTasks = new List<Task>();
            List<Task> saveTasks = new List<Task>();
            foreach(var operation in this.Operations.Keys) {
                readTasks.Add(operation.ReadAsync().ContinueWith(async (data) => {
                    if (data.Result != null) {
                        this._boxOperations[operation] = data.Result;
                        await this._boxHub.Clients.All.RecieveAutoReading(data.Result.TimeStamp+": "+"A6: "+data.Result.AnalogCh5);   
                    }
                }));
                if (operation.CheckSaveTime()) {
                    saveTasks.Add(operation.SaveAsync());
                }
            
            }
            
            Task.WhenAll(readTasks);
            Task.WhenAll(saveTasks);
        }

        public async Task StopAsync() {
            await Task.Run(() => this._logger.LogInformation("MonitorBox Controller Stopping"));
        }

        public void Stop() {
            this._logger.LogInformation("MonitorBox Controller Stopping");
        }

        public GenericBoxReading GetLastReading(string id) {
            var key = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (key == null)
                return null;

            GenericBoxReading temp = new GenericBoxReading();
            this._boxOperations.TryGetValue(key, out temp);
            return temp;
        }

        public bool SetAlarm(string id, bool on_off) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return false;

          return boxOperation.SetAlarm(on_off);
        }

        public async Task<bool> SetAlarmAsync(string id, bool on_off) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return false;

            return await boxOperation.SetAlarmAsync(on_off);
        }

        public bool SetWarning(string id, bool on_off) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return false;

            return boxOperation.SetWarning(on_off);
        }

        public async Task<bool> SetWarningAsync(string id, bool on_off) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return false;

            return await boxOperation.SetWarningAsync(on_off);
        }

        public bool SetMaintenance(string id, bool on_off) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return false;

            return boxOperation.SetMaintenance(on_off);
        }

        public async Task<bool> SetMaintenanceAsync(string id, bool on_off) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return false;

            return await boxOperation.SetMaintenanceAsync(on_off);
        }

        public bool GetDeviceState(string id,out DeviceState state) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null) {
                state = DeviceState.OKAY;
                return false;
            }
            state = boxOperation.Device.State;
            return true;
        }

        public ushort GetAnalogChannelRaw(string id, int channel) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return 0;

            return boxOperation.GetAnalogChannelRaw(channel);
        }

        public async Task<ushort> GetAnalogChannelRawAsync(string id, int channel) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return 0;

            return await boxOperation.GetAnalogChannelRawAsync(channel);
        }

        public async Task<double> GetAnalogChannelVoltageAsync(string id, int channel) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return 0;

            return await boxOperation.GetAnalogChannelVoltageAsync(channel);
        }

        public double GetAnalogChannelVoltage(string id, int channel) {
            var boxOperation = this._boxOperations.Keys.SingleOrDefault(op => op.Device.Identifier == id);
            if (boxOperation == null)
                return 0;

            return boxOperation.GetAnalogChannelVoltage(channel);
        }
    }
}

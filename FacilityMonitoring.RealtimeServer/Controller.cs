using Castle.Core.Logging;
using FacilityMonitoring.Common.Hardware;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FacilityMonitoring.RealtimeServer {
    public class Controller {
        private readonly ILogger<Controller> _logger;
        private readonly BufferBlock<MonitorBoxOperations> _bufferBlock;
        private MonitorBoxOperations _operation;
        private System.Timers.Timer _timer;
        private TimeSpan _saveInterval;
        private DateTime _lastSave;

        public Controller() {
            this._bufferBlock = new BufferBlock<MonitorBoxOperations>(new DataflowBlockOptions { BoundedCapacity = 5 });

        }

        public async Task Start() {

        }



        public string GetLastRead() {
            return this._operation.GetData();
        }
    }
}

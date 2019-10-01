﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {

    public class WebData {
        public IEnumerable<string> Headers { get; set; }
        public IEnumerable<string> Row { get; set; }
    }

    public interface IGeneratorHub {
        Task SendGeneratorReading(WebData data);
    }

    public interface IMonitorBoxHub {
        Task SendMonitorBoxReading(WebData data);
    }

    public interface IAmmoniaControllerHub {
        Task SendAmmoniaReading(WebData data);
    }

    public class MonitorBoxHub : Hub<IMonitorBoxHub> {
        public async Task SendMonitorBoxReading(WebData data) {
            await Clients.All.SendMonitorBoxReading(data);
        }
    }

}

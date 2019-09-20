using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services.ModbusServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using FacilityMonitoring.Common.ServiceLayer;

namespace FacilityMonitoring.ConsoleTesting {
    class ProgramAsync {
        static async Task<int> Main(string[] args) {

            DeviceController controller = new DeviceController();
            controller.Start();
            await controller.Run();
            //await Task.WhenAll(task);
            return 0;
        }
    }


}

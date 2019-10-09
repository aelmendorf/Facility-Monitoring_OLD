using System.Threading.Tasks;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace FacilityMonitoring.Common.Server {
    public class MonitorBoxHub : Hub<IMonitorBoxHub> {
        private readonly IBoxCollectionController _controller;

        public MonitorBoxHub(IBoxCollectionController controller) {
            this._controller = controller;
        }

        public async Task SetMaintenance(string identifier,bool onOff) {
            var operations=this._controller.Operations.FirstOrDefault(op=>op.Device.Identifier==identifier);
            if (operations != null) {
                var success=await operations.SetMaintenanceAsync(onOff);
                await Clients.Caller.SetMaintenanceCallBack(success);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error Switching Maintenance");
            }
        }

        public async Task GetCurrentReading(string identifier) {
            var operations = this._controller.Operations.FirstOrDefault(op => op.Device.Identifier == identifier);
            if (operations != null) {
                //Get last reading here
                await Clients.Caller.RecieveReadingCallBack(operations.Data);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error Retrieving Reading");
            }
        }

        public async Task GetAnalogChannelRaw(string identifier,int channel) {
            var operations = this._controller.Operations.FirstOrDefault(op => op.Device.Identifier == identifier);
            if (operations != null) {
                await Clients.Caller.RecieveChannelRawCallBack("Raw Data: 4546");
            } else {
                await Clients.Caller.RecieveErrorMessage("Error Retrieving Raw Data");
            }
        }

        public async Task GetBoxState(string identifier) {
            var operations = this._controller.Operations.FirstOrDefault(op => op.Device.Identifier == identifier);
            if (operations != null) {
                //Get last reading here
                await Clients.Caller.RecieveStateCallBack(DeviceState.MAINTENCE);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error Getting Device State");
            }
        }
    }

}

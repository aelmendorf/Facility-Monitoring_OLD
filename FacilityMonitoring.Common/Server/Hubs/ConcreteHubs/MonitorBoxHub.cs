using System.Threading.Tasks;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {
    public class MonitorBoxHub : Hub<IMonitorBoxHub> {
        private readonly MonitorBoxController _controller;

        public MonitorBoxHub(MonitorBoxController controller) {
            this._controller = controller;
        }

        public async Task SendMonitorBoxReading(string data) {
            await Clients.All.RecieveAutoBoxReading(data);
        }

        [HubMethodName("SetMaintenanceOn")]
        public async Task SetMaintenanceOn() {
            bool success=await this._controller.SetWarningAsync(true);
            await Clients.Caller.SwitchMaintenanceCallBack(success);
        }

        [HubMethodName("GetCurrentReading")]
        public async Task GetCurrentReading() {
            //Call last reading here
            await Clients.Caller.RecieveBoxReadingCallBack("Sent Requested");
        }

        [HubMethodName("GetAnalogChannelRaw")]
        public async Task GetAnalogChannelRaw(int channel) {
            await Clients.Caller.RecieveChannelRawCallBack("Raw Data: 4546");
        }

        [HubMethodName("GetAnalogChannelRaw")]
        public async Task GetBoxState() {
            await Clients.Caller.RecieveBoxStateCallBack(DeviceState.MAINTENCE);
        }
    }

}

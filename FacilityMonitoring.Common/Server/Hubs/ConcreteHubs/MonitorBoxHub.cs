using System.Threading.Tasks;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.DataLayer;
using System.Linq;
using System;

namespace FacilityMonitoring.Common.Server {
    public class MonitorBoxHub : Hub<IMonitorBoxHub> {
        private readonly IBoxCollectionController _controller;

        public MonitorBoxHub(IBoxCollectionController controller) {
            this._controller = controller;
        }

        public async Task SetMaintenance(string identifier,bool onOff) {
            var success=await this._controller.SetMaintenanceAsync(identifier, onOff);
            await Clients.Caller.SetMaintenanceCallBack(success);
        }

        public async Task GetCurrentReading(string identifier) {
            var reading = this._controller.GetLastReading(identifier);
            if (reading != null) {
                await Clients.Caller.RecieveReadingCallBack(reading);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error retrieving box");
            }
        }

        public async Task GetAnalogChannelRaw(string identifier,int channel) {
            var value = await this._controller.GetAnalogChannelRawAsync(identifier, channel);
            await Clients.Caller.RecieveChannelRawCallBack(value);
        }

        public async Task GetAnalogChannelVoltage(string identifier, int channel) {
            var value = await this._controller.GetAnalogChannelVoltageAsync(identifier, channel);
            await Clients.Caller.RecieveChannelVoltageCallBack(value);
        }

        public async Task GetBoxState(string identifier) {
            DeviceState state;
            var success = this._controller.GetDeviceState(identifier, out state);
            if (success) {
                await Clients.Caller.RecieveStateCallBack(state);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error Retrieving Requested Device");
            }
        }
    }
}

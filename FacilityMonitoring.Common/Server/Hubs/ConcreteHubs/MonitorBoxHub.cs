using System.Threading.Tasks;
using FacilityMonitoring.Common.Hardware;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;
using FacilityMonitoring.Common.DataLayer;
using System.Linq;
using System;

namespace FacilityMonitoring.Common.Server {
    public class GasBayHub : Hub<IMonitorBoxHub> {
        private readonly IGenericBoxController _controller;

        public GasBayHub(IGenericBoxController controller) {
            this._controller = controller;
        }

        public async Task SetMaintenance(bool onOff) {
            var success=await this._controller.SetMaintenanceAsync(onOff);
            await Clients.Caller.SetMaintenanceCallBack(success);
        }

        public async Task SetAlarmDebug(bool onOff) {
            var success = await this._controller.SetAlarmAsync(onOff);
            await Clients.Caller.SetAlarmCallBack(success);
        }

        public async Task SetWarnDebug(string identifier, bool onOff) {
            var success = await this._controller.SetWarningAsync(onOff);
            await Clients.Caller.SetWarnCallBack(success);
        }

        //public async Task GetCurrentReading(string identifier) {
        //    var reading = this._controller.GetLastReading(identifier);
        //    if (reading != null) {
        //        await Clients.Caller.RecieveReadingCallBack(reading);
        //    } else {
        //        await Clients.Caller.RecieveErrorMessage("Error retrieving box");
        //    }
        //}

        public GenericBoxReading GetCurrentReading() {
            return this._controller.GetCurrentReading();
        }

        public BoxReadingDTO GetDeviceTable() {
            return this._controller.GetDeviceTable();
        }

        public async Task GetAnalogChannelRaw(int channel) {
            var value = await this._controller.GetAnalogChannelRawAsync(channel);
            await Clients.Caller.RecieveChannelRawCallBack(value);
        }

        public async Task GetAnalogChannelVoltage(int channel) {
            var value = await this._controller.GetAnalogChannelVoltageAsync( channel);
            await Clients.Caller.RecieveChannelVoltageCallBack(value);
        }

        public async Task GetBoxState(string identifier) {
            DeviceState state;
            var success = this._controller.GetDeviceState(out state);
            if (success) {
                await Clients.Caller.RecieveStateCallBack(state);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error Retrieving Requested Device");
            }
        }
    }
}

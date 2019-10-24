using System.Threading.Tasks;
using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Hubs {
    public class GasBayHub : Hub<IMonitorBoxHub> {
        private readonly IGenericBoxController _controller;

        public GasBayHub(IGenericBoxController controller) {
            this._controller = controller;
        }

        public async Task SetMaintenance(bool onOff) {
            var success=await this._controller.SetMaintenanceAsync(onOff);
            await Clients.Caller.SetMaintenanceCallBack(success);
        }

        public async Task<bool> SetMaint(bool onOff) {
            return await this._controller.SetMaintenanceAsync(onOff);
        }

        public async Task<bool> SetAlarmDebug(bool onOff) {
            return await this._controller.SetAlarmAsync(onOff);
        }

        public async Task<bool> SetWarnDebug(bool onOff) {
            return await this._controller.SetWarningAsync(onOff);
        }

        public MonitorBoxReading GetCurrentReading() {
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

        //public async Task<bool> GetMaintState() {
        //    return this._controller.Operations.LastReading.
        //}

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

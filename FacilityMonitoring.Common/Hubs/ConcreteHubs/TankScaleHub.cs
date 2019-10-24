using System.Collections.Generic;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Data.DTO;
using FacilityMonitoring.Common.ModbusServices.Controllers;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Hubs {
    public class TankScaleHub : Hub<ITankScaleHub> {
        private readonly ITankScaleController _controller;

        public TankScaleHub(ITankScaleController controller) {
            this._controller = controller;
        }

        public async Task GetLastReading() {
            await Clients.Caller.RecieveReadingCallBack(this._controller.Operations.LastReading.GetDataTransfer());
        }

        public IEnumerable<Tank> GetData() {
            return this._controller.Operations.LastReading.GetDataTransfer();
        }

        public async Task SetCalibrationMode(bool onOff) {
            var success=await this._controller.Operations.SetCalibrationModeAsync(onOff);
            await Clients.Caller.SetCalibrationModeCallBack(success);
        }

        public async Task GetCalibration(int tank) {
            var tankCal = this._controller.Operations.LastReading.GetTankCalibration(tank);
            if (tankCal != null) {
                await Clients.Caller.GetCalibrationCallBack(tankCal);
            } else {
                await Clients.Caller.RecieveErrorMessage("Error retrieving tank calibration");
            }
        }

        public async Task SetCalibration(AmmoniaCalibrationData data) {
            var success =await this._controller.Operations.SetCalibrationAsync(data);
            await Clients.Caller.SetCalibrationCallBack(success);
        }
    }

}

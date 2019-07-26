using System;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.Data_Layer;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FacilityMonitoring.MonitoringServer {
    public class MonitorHub:Hub<IFacilityAmmoniaReading> {
        private readonly Monitor _monitor;

        public MonitorHub(Monitor monitor) {
            this._monitor=monitor;
        }

        public async Task SendReadingToClients(AmmoniaTankView tankView) {
            await Clients.All.BroadcastReading(tankView);
        }

        public async Task SendAllTanksToClients(IEnumerable<AmmoniaTankView> tankViews) {
            await Clients.All.BroadcastAllTanks(tankViews);
        }

        public IEnumerable<AmmoniaTankView> GetAllTanks() {
            return this._monitor.GetAllTanks(); 
        }

    }
}

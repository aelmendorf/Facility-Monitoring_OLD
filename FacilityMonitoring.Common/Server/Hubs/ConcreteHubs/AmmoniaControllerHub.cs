using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {
    public class AmmoniaControllerHub : Hub<IAmmoniaControllerHub> {
        public async Task SendAmmoniaReading(string data) {
            await Clients.All.SendAmmoniaReading(data);
        }
    }

}

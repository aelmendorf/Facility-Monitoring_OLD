using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {
    public class GeneratorHub : Hub<IGeneratorHub> {
        public async Task SendGeneratorReading(string data) {
            await Clients.All.SendGeneratorReading(data);
        }
    }

}

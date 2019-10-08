using System.Threading.Tasks;
using FacilityMonitoring.Common.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {
    public class GeneratorHub : Hub<IGeneratorHub> {
        private readonly GeneratorsHubService _generatorsService;

        public GeneratorHub(GeneratorsHubService generatorsService) {
            this._generatorsService = generatorsService;
        }

        public async Task SendGeneratorReading(string data) {
            await Clients.All.SendGeneratorReading(data);
        }

        public async Task  GetGeneratorReading(int i) {
            if ((i-1) > this._generatorsService.Controllers.Count - 1  || (i-1)<0)
                throw new System.Exception("Error: Index Out Of Bounds");

            await Clients.All.RecieveMessage(this._generatorsService.Controllers[i].Data);
        }
    }

}

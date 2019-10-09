using System.Threading.Tasks;
using FacilityMonitoring.Common.Server.Services;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {
    public class GeneratorHub : Hub<IGeneratorHub> {
        private readonly IGeneratorCollectionController _generatorsService;

        public GeneratorHub(IGeneratorCollectionController generatorsService) {
            this._generatorsService = generatorsService;
        }

        public async Task SendGeneratorReading(int genId) {
            if ((genId - 1) > this._generatorsService.Operations.Count - 1 || (genId - 1) < 0) {
                await Clients.All.RecieveErrorMessage("Error: Index Out Of Bounds");
            } else {
                await Clients.All.SendGeneratorReading(this._generatorsService.Operations[genId-1].Data);
            }
        }

        public async Task  GetGeneratorReading(int i) {
            if ((i-1) > this._generatorsService.Operations.Count - 1  || (i - 1) < 0) {
                await Clients.All.RecieveErrorMessage("Error: Index Out Of Bounds");
            } else {
                await Clients.All.RecieveMessage(this._generatorsService.Operations[i-1].Data);
            }
        }
    }

}

using System.Collections.Generic;
using System.Threading.Tasks;
using FacilityMonitoring.Common.DataLayer;
using FacilityMonitoring.Common.Server.Services;
using FacilityMonitoring.Common.Services;
using Microsoft.AspNetCore.SignalR;

namespace FacilityMonitoring.Common.Server {
    public class GeneratorHub : Hub<IGeneratorHub> {
        private readonly IGeneratorCollectionController _generatorsService;

        public GeneratorHub(IGeneratorCollectionController generatorsService) {
            this._generatorsService = generatorsService;
        }

        public IEnumerable<GeneratorReadingDTO> GetAllGenerators() {
            return this._generatorsService.GetAllGenerators();
        }

        public async Task SendGeneratorReading(string genId) {
            var reading=this._generatorsService.GetLastReading(genId);
            if (reading != null) {
                await Clients.All.SendGeneratorReading(reading);
            } else {
                await Clients.All.RecieveErrorMessage("Error: Could Not Find Requested Generator");
            }
        }

        public async Task  GetGeneratorReading(string genId) {
            var reading = this._generatorsService.GetLastReading(genId);
            if (reading != null) {
                await Clients.All.SendGeneratorReading(reading);
            } else {
                await Clients.All.RecieveErrorMessage("Error: Could Not Find Requested Generator");
            }
        }
    }

}

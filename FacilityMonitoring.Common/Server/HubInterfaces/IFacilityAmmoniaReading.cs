using System.Collections.Generic;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Data_Layer;

namespace FacilityMonitoring.Common.Server {
    public interface IFacilityAmmoniaReading {
        Task BroadcastReading(AmmoniaTankView tankView);
        Task BroadcastAllTanks(IEnumerable<AmmoniaTankView> tankViews);
        IEnumerable<AmmoniaTankView> GetAllTanks();
    }
}

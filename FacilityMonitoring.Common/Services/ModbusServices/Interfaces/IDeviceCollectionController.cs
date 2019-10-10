using FacilityMonitoring.Common.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services {
    public interface IDeviceCollectionController {
        double ReadInterval { get; }
        Task StartAsync();
        void Start();
        void Stop();
        Task StopAsync();
    }

    public interface IGeneratorCollectionController:IDeviceCollectionController {
        ConcurrentDictionary<IGeneratorOperations,H2GenReading> Operations { get; }
        H2GenReading GetLastReading(string genId);
        void TimeHandler(object state);
    }
    public interface IAmmoniaCollectionController : IDeviceCollectionController {
        IAmmoniaOperations Operations { get; }
    }

    public interface IBoxCollectionController : IDeviceCollectionController {
        List<IGenericBoxOperations> Operations { get; }
    }
}

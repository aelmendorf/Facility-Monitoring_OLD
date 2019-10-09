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
        List<IGeneratorOperations> Operations { get; }
    }
    public interface IAmmoniaCollectionController : IDeviceCollectionController {
        IAmmoniaOperations Operations { get; }
    }

    public interface IBoxCollectionController : IDeviceCollectionController {
        List<IGenericBoxOperations> Operations { get; }
    }
}

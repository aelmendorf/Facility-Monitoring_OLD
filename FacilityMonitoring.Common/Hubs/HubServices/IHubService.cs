using Microsoft.Extensions.Hosting;
using System;

namespace FacilityMonitoring.Common.Hubs.HubServices {
    public interface IHubService : IHostedService, IDisposable {

    }
}

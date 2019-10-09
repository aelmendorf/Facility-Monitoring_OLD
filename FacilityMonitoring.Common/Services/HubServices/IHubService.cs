using FacilityMonitoring.Common.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Server.Services {
    //public interface IHubService:IHostedService, IDisposable {
    //    IDeviceOperations Controller { get; }
    //    void TimerHandler(object state);
    //}

    public interface IHubService : IHostedService, IDisposable {
        void TimerHandler(object state);
    }
}

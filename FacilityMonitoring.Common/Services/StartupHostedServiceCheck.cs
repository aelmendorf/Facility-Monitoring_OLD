using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Services {
    public class StartupHostedServiceCheck {

        private volatile bool _alertHandlerStarted=false;

        public bool AlertHandlerStarted { 
            get => this._alertHandlerStarted; 
            set => this._alertHandlerStarted = value;
        }
    }
}

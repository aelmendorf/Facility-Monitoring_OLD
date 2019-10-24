using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Hubs {
    public static class HubConstants {

        public static string HubUrl = "http://172.20.4.209:443/hubs/monitor";
        public static string GeneratorHubUrl = "http://172.20.4.209:443/hubs/generator";
        public static string AmmoniaHubUrl = "http://172.20.4.209:443/hubs/ammonia";
        public static string DeviceOverviewUrl = "http://172.20.4.209:443/hubs/overview";
        public static string GeneratorHubUrlLocal = "http://localhost:5001/hubs/generator";

        //public static string HubUrl => "https://localhost:5001/hubs/clock";
        public static class Events {

        }
    }
}

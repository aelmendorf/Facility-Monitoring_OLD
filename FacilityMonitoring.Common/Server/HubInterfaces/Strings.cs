﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Server {
    public static class Strings {

        public static string HubUrl = "https://localhost:5001/hubs/monitor";

        public static class Events {
            public static string ReadingSent => nameof(IMonitorBoxHub.SendMonitorBoxReading);
            //public static string AllReadingSent => nameof(IFacilityAmmoniaReading.BroadcastAllTanks);
            //public static string GetAllTanksSent => nameof(IFacilityAmmoniaReading.GetAllTanks);
        }
    }
}

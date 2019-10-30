using FacilityMonitoring.Common.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Services {
    public class MonitorBoxAlertCommand: IRequest<AlertServiceResponce> {
        public string Message { get; set; }
        public List<AnalogChannel> AnalogChannels { get; set; }
        public List<DigitalInputChannel> Registers { get; set; }
        public MonitorBoxReading Reading { get; set; }
    }
}

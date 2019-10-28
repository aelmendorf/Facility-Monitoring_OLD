using MediatR;
using System;

namespace FacilityMonitoring.Common.Services {
    public class AlertServiceCommand: IRequest<AlertServiceResponce> {
        public string Message { get; set; }
        public Type type;
    }
}

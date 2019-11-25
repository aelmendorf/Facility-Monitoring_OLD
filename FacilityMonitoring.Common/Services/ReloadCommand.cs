using FacilityMonitoring.Common.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Services {
    public interface IReloadCommand : IRequest<ReloadResponce> {

    }
}

using System;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Server {
#region IClock
    public interface IClock
    {
        Task ShowTime(DateTime currentTime);
    }
#endregion
}

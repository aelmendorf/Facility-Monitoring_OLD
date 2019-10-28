using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace FacilityMonitoring.Common.Services {
    public interface IEmailService {
        Task SendMessageAsync(string msg);
        void SendMessage(string msg);
    }
}

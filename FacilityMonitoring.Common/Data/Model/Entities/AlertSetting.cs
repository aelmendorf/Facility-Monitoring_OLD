using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Data.Entities {
    public enum NotificationType {
        EMAIL,
        WEBSITE,
        NONE
    }

    public enum AlertAction { 
        ALARM, 
        WARN, 
        SOFTWARN, 
        MAINTENANCE, 
        NOTHING 
    }

    public partial class AlertSetting {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Frequency { get; set; }
        public NotificationType Notification { get; set; }
        public AlertAction AlertAction { get; set; }
    }
}

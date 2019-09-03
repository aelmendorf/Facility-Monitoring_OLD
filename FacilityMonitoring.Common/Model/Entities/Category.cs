using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {
    public abstract class Category {
        public int Id { get; set; }
        public string Name { get; set; }       
    }

    public partial class SensorType : Category {
        public double ZeroValue { get; set; }
        public double ZeroCalibration { get; set; }

        public double MaxValue { get; set; }
        public double MaxCalibration { get; set; }

        public ICollection<AnalogChannel> AnalogChannels { get; set; }

        public SensorType() {
            this.AnalogChannels = new ObservableHashSet<AnalogChannel>();
        }
    }
}

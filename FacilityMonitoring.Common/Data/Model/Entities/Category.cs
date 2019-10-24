using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Data.Entities {

    public abstract class Category {
        public int Id { get; set; }
        public string Name { get; set; }       
    }

    public partial class SensorType : Category {
        public double ZeroPoint { get; set; }
        public double MaxPoint { get; set; }
        public string Units { get; set; }

        public ICollection<Register> Registers { get; set; }

        public SensorType() {
            this.Registers = new ObservableHashSet<Register>();
        }
    }
}

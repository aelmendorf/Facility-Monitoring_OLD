using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityMonitoring.Common.Model {

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

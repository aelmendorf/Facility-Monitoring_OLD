using System;

namespace FacilityMonitoring.Common.Data.Entities {

    //Inputs
    //High: Logic High=Tripped
    //Low:  Logic Low=Tripped

    //Outputs
    // High: Logic High=Turn on
    // Low:  Logic Low=Turn on

    public enum LogicType { HIGH=0, LOW }
    public enum OutputControl { HARDWARE,SOFTWARE}

    public abstract class Register {

        public int Id { get; set; }
        public string Name { get; set; }
        public int RegisterIndex { get; set; }
        public int RegisterLength { get; set; } //Added
        public bool Connected { get; set; }
        public bool Bypass { get; set; }
        public string PropertyMap { get; set; }
        public LogicType Logic { get; set; }
        public bool Display { get; set; }
        public DateTime? LastAlert { get; set; }

        public int DeviceId { get; set; }
        public virtual ModbusDevice Device { get; set; }

        public int? SensorTypeId { get; set; }
        public virtual SensorType SensorType { get; set; }
    }
}

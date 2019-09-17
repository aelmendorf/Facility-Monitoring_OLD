using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Hardware_Layer.Wrappers {
    public interface IModbusDeviceWrapper {
        ModbusDevice Device { get; set; }
        FacilityContext _context { get; set; }
        void Load();
        void Read();
        void ReadAndSave();
    }

    public class GeneratorWrapper : IModbusDeviceWrapper {
        public ModbusDevice Device { get; set; }
        public FacilityContext _context { get; set; }

        public void Load() {

        }

        public void Read() {

        }

        public void ReadAndSave() {

        }
    }
}

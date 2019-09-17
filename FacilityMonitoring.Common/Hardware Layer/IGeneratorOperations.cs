using System;
using System.Collections.Generic;
using System.Text;
using FacilityMonitoring.Common.Model;

namespace FacilityMonitoring.Common.Hardware {
    public interface IGeneratorOperations {
        H2Generator Device { get; set; }
        H2GenReading ReadAll();
    }
}

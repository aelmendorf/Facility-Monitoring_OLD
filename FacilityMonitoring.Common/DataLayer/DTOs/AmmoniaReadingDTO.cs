using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.DataLayer.DTOs {
    public class AmmoniaReadingDTO {
    
    }

    public class Tank {
        public string Identifier { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double DutyCycle { get; set; }
    }
}

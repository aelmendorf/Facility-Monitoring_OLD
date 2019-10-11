using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.DataLayer.DTOs {
    public class BoxReadingDTO {
        public string identifier { get; set; }
        public double H2DetectorGenRoom { get; set; }
        public double H2DetectorFront { get; set; }
    }
}

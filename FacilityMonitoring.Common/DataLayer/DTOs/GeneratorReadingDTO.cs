using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.DataLayer {
    public class GeneratorReadingDTO {
        public string Identifier { get; set; }
        public string TimeStamp { get; set; }
        public string OperationMode { get; set; }         //56
        public string SystemState { get; set; }           //57

        public double SystemPressure { get; set; }     //60
        public double ProductPressure { get; set; }    //61
        public double HydrogenFlow { get; set; }          //70


        public GeneratorReadingDTO(H2GenReading reading) {
            this.TimeStamp = reading.TimeStamp.ToString();
            this.Identifier = reading.Identifier;
            this.OperationMode = reading.OperationMode.ToString();
            this.SystemState = reading.SystemState.ToString();
            this.SystemPressure = reading.SystemPressure;
            this.ProductPressure = reading.ProductPressure;
            this.HydrogenFlow = reading.HydrogenFlow;
        }
    }
}

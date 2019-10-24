using FacilityMonitoring.Common.Converters;
using System;
using FacilityMonitoring.Common.Data.Entities;

namespace FacilityMonitoring.Common.Data.DTO {
    public class GeneratorReadingDTO {
        public string TimeStamp { get; set; }
        public string Identifier { get; set; }
        public string SystemMode { get; set; }
        public string OperationMode { get; set; }          
        public string SystemState { get; set; }
        public decimal SystemPressure { get; set; }     
        public decimal WaterTemperature { get; set; }
        public decimal HydrogenFlow { get; set; }          

        public GeneratorReadingDTO(H2GenReading reading) {
            this.TimeStamp = reading.TimeStamp.ToString();
            this.Identifier = reading.Identifier;
            this.SystemMode = reading.SystemMode.ToString();
            this.OperationMode = reading.OperationMode.ToString();
            this.SystemState = reading.SystemState.ToString();
            this.SystemPressure = Convert.ToDecimal(Math.Round(reading.SystemPressure * RegisterConverters.BAR_TO_PSI,2));
            this.HydrogenFlow = Convert.ToDecimal(Math.Round(reading.HydrogenFlow * RegisterConverters.KGHR_TO_SLM,2));
            this.WaterTemperature = Convert.ToDecimal(Math.Round(reading.HeatExchangerWaterTemp,2));
        }
    }
}

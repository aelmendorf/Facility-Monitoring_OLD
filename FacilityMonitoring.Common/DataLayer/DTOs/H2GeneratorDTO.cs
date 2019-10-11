using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.DataLayer {
    public class H2GeneratorDTO {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string DisplayName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int SlaveAddress { get; set; }
        public DeviceState State { get; set; }
        public string Status { get; set; }
        public bool BypassAll { get; set; }
        public double ReadInterval { get; set; }
        public double SaveInterval { get; set; }

        public OperationMode OperationMode { get; set; }
        public SystemState SystemState { get; set; }

        public List<Register> Registers { get; set; }

        public H2GeneratorDTO(H2Generator generator) {
            this.Id = generator.Id;
            this.Identifier = generator.Identifier;
            this.DisplayName = generator.Identifier;
        }

    }

    public class GeneratorRegisterDTO {

    }
}

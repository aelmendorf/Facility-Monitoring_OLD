using FacilityMonitoring.Common.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Services {
    public interface IAlertCommand:IRequest<AlertServiceResponce> {

    }

    public interface IMonitorBoxAlertCommand : IAlertCommand {
        List<Register> AlertRegisters { get; set; }
        List<Register> AllReg { get; set; }
        MonitorBox Device { get; set; }
    }

    public interface IGeneratorAlertCommand : IAlertCommand {
        H2GenReading Reading { get; set; }
        H2Generator Device { get; set; }
        List<GeneratorRegister> Registers { get; set; }
    }

    public interface ITankScaleAlertCommand : IAlertCommand {
        TankScaleReading Reading { get; set; }
        TankScale Device { get; set; }
    }

    public class MonitorBoxAlertCommand : IRequest<bool> {
        public MonitorBox Device { get; set; }
        public List<Register> AlertRegisters { get; set; }
        public List<Register> AllReg { get; set; }

        public MonitorBoxAlertCommand() {

        }

        public MonitorBoxAlertCommand(MonitorBox device,List<Register> alertReg,List<Register> statReg) {
            this.Device = device;
            this.AlertRegisters = alertReg;
            this.AllReg = statReg;
        }
    }

    public class GeneratorAlertCommand : IRequest<bool> {
        public H2GenReading Reading { get; set; }
        public H2Generator Device { get; set; }
        public List<GeneratorRegister> Registers { get; set; }
    }

    public class TankScaleAlertCommand : IRequest<bool> {
        public TankScaleReading Reading { get; set; }
        public TankScale Device { get; set; }
    }
}

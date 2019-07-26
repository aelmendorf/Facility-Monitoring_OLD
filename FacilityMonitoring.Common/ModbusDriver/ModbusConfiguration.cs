using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.ModbusDriver {

    public enum FunctionCode : ushort {
        ReadCoil = 1,
        ReadDiscreteInput = 2,
        ReadHoldingReg = 3,
        ReadInputReg = 4,
        WriteCoil = 5,
        WriteHoldingReg = 6,
        WriteMultipleCoils = 15,
        WriteMultipleReg = 16
    }

    public enum ModbusType {
        STRING,
        DOUBLE,
        LONG,
        BIT,
        SHORT
    }

}

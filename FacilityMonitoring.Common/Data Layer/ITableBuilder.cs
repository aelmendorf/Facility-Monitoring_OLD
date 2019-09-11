using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FacilityMonitoring.Common.Data {
    public interface ITableBuilder {
        DataTable BuildTable(ModbusDevice device);
    }

    public class GenericBoxTableBuilder : ITableBuilder {
        public DataTable BuildTable(ModbusDevice device) {
            if (device.GetType() == typeof(GenericMonitorBox)) {

                return null;
            } else {
                return null;
            }

        }
    }
}

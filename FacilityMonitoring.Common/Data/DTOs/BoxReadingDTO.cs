using FacilityMonitoring.Common.Data.Entities;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Data.DTO {
    public class Column {
        public string ColumnName { get; set; }
        public string Header { get; set; }
    }

    public class BoxReadingDTO {
        public IEnumerable<Column> Columns { get; set; }
        public MonitorBoxReading Row { get; set; }

        public BoxReadingDTO() {

        }
    }
}

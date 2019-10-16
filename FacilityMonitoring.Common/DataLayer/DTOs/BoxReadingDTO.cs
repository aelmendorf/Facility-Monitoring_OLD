using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.DataLayer {
    public class Column {
        public string ColumnName { get; set; }
        public string Header { get; set; }
    }

    public class BoxReadingDTO {
        public IEnumerable<Column> Columns { get; set; }
        public GenericBoxReading Row { get; set; }

        public BoxReadingDTO() {

        }
    }
}

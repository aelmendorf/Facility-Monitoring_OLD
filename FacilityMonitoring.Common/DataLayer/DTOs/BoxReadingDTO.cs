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
        public List<Column> Columns { get; set; }
        public List<GenericBoxReading> Rows { get; set; }

        public BoxReadingDTO() {
            this.Columns = new List<Column>();
            this.Rows = new List<GenericBoxReading>();
        }
    }
}

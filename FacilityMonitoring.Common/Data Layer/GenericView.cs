using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Data {

    public class Column {
        public string Name { get; set; }
        public int Index { get; set; }
        public Type Type;
    }

    public class Cell {
        public object value { get; set; }
    }

    public class Row {
        public IEnumerable<object> Cells { get; set; }
    }

    public class DataView {
        IEnumerable<Column> Columns { get; set; }
        IEnumerable<Row> Rows { get; set; }
    }
}

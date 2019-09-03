using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {
    public abstract class Mapping {
        public int Id { get; set; }

        public int GenericMonitorBoxId { get; set; }
        public virtual GenericMonitorBox GenericMonitorBox { get; set; }
    }



    public partial class AnalogMapping : Mapping {
        public ICollection<AnalogChannel> AnalogChannels { get; set; }

        public AnalogMapping() {
            this.AnalogChannels = new ObservableHashSet<AnalogChannel>();
        }
    }

    public partial class DigitalInputMapping : Mapping {
        public ICollection<DigitalChannel> DigitalChannels { get; set; }

        public DigitalInputMapping() {
            this.DigitalChannels = new ObservableHashSet<DigitalChannel>();
        }
    }

    //public partial class DigitalInputMapping : Mapping {
    //    public bool Chan1Connected { get; set; }
    //    public string Chan1Name { get; set; }

    //    public bool Chan2Connected { get; set; }
    //    public string Chan2Name { get; set; }

    //    public bool Chan3Connected { get; set; }
    //    public string Chan3Name { get; set; }

    //    public bool Chan4Connected { get; set; }
    //    public string Chan4Name { get; set; }

    //    public bool Chan5Connected { get; set; }
    //    public string Chan5Name { get; set; }

    //    public bool Chan6Connected { get; set; }
    //    public string Chan6Name { get; set; }

    //    public bool Chan7Connected { get; set; }
    //    public string Chan7Name { get; set; }

    //    public bool Chan8Connected { get; set; }
    //    public string Chan8Name { get; set; }

    //    public bool Chan9Connected { get; set; }
    //    public string Chan9Name { get; set; }

    //    public bool Chan10Connected { get; set; }
    //    public string Chan10Name { get; set; }

    //    public bool Chan11Connected { get; set; }
    //    public string Chan11Name { get; set; }

    //    public bool Chan12Connected { get; set; }
    //    public string Chan12Name { get; set; }

    //    public bool Chan13Connected { get; set; }
    //    public string Chan13Name { get; set; }

    //    public bool Chan14Connected { get; set; }
    //    public string Chan14Name { get; set; }

    //    public bool Chan15Connected { get; set; }
    //    public string Chan15Name { get; set; }

    //    public bool Chan16Connected { get; set; }
    //    public string Chan16Name { get; set; }

    //    public bool Chan17Connected { get; set; }
    //    public string Chan17Name { get; set; }

    //    public bool Chan18Connected { get; set; }
    //    public string Chan18Name { get; set; }

    //    public bool Chan19Connected { get; set; }
    //    public string Chan19Name { get; set; }

    //    public bool Chan20Connected { get; set; }
    //    public string Chan20Name { get; set; }

    //    public bool Chan21Connected { get; set; }
    //    public string Chan21Name { get; set; }

    //    public bool Chan22Connected { get; set; }
    //    public string Chan22Name { get; set; }

    //    public bool Chan23Connected { get; set; }
    //    public string Chan23Name { get; set; }

    //    public bool Chan24Connected { get; set; }
    //    public string Chan24Name { get; set; }

    //    public bool Chan25Connected { get; set; }
    //    public string Chan25Name { get; set; }

    //    public bool Chan26Connected { get; set; }
    //    public string Chan26Name { get; set; }

    //    public bool Chan27Connected { get; set; }
    //    public string Chan27Name { get; set; }

    //    public bool Chan28Connected { get; set; }
    //    public string Chan28Name { get; set; }

    //    public bool Chan29Connected { get; set; }
    //    public string Chan29Name { get; set; }

    //    public bool Chan30Connected { get; set; }
    //    public string Chan30Name { get; set; }

    //    public bool Chan31Connected { get; set; }
    //    public string Chan31Name { get; set; }

    //    public bool Chan32Connected { get; set; }
    //    public string Chan32Name { get; set; }

    //    public bool Chan33Connected { get; set; }
    //    public string Chan33Name { get; set; }

    //    public bool Chan34Connected { get; set; }
    //    public string Chan34Name { get; set; }

    //    public bool Chan35Connected { get; set; }
    //    public string Chan35Name { get; set; }

    //    public bool Chan36Connected { get; set; }
    //    public string Chan36Name { get; set; }

    //    public bool Chan37Connected { get; set; }
    //    public string Chan37Name { get; set; }

    //    public bool Chan38Connected { get; set; }
    //    public string Chan38Name { get; set; }
    //}

    //public partial class DigitalOutputMapping:Mapping {
    //    public bool Chan1Connected { get; set; }
    //    public string Chan1Name { get; set; }

    //    public bool Chan2Connected { get; set; }
    //    public string Chan2Name { get; set; }

    //    public bool Chan3Connected { get; set; }
    //    public string Chan3Name { get; set; }

    //    public bool Chan4Connected { get; set; }
    //    public string Chan4Name { get; set; }

    //    public bool Chan5Connected { get; set; }
    //    public string Chan5Name { get; set; }

    //    public bool Chan6Connected { get; set; }
    //    public string Chan6Name { get; set; }

    //    public bool Chan7Connected { get; set; }
    //    public string Chan7Name { get; set; }

    //    public bool Chan8Connected { get; set; }
    //    public string Chan8Name { get; set; }

    //    public bool Chan9Connected { get; set; }
    //    public string Chan9Name { get; set; }

    //    public bool Chan10Connected { get; set; }
    //    public string Chan10Name { get; set; }
    //}

    //public partial class AnalogMapping : Mapping {
    //    public int AnalogMappingId { get; set; }
    //    public virtual AnalogCalibration AnalogCalibration { get; set; }

    //    public bool Chan1Connected { get; set; }
    //    public string Chan1Name { get; set; }
    //    public AnalogSensorType Chan1Type { get; set; }

    //    public bool Chan2Connected { get; set; }
    //    public string Chan2Name { get; set; }
    //    public AnalogSensorType Chan2Type { get; set; }

    //    public bool Chan3Connected { get; set; }
    //    public string Chan3Name { get; set; }
    //    public AnalogSensorType Chan3Type { get; set; }

    //    public bool Chan4Connected { get; set; }
    //    public string Chan4Name { get; set; }
    //    public AnalogSensorType Chan4Type { get; set; }

    //    public bool Chan5Connected { get; set; }
    //    public string Chan5Name { get; set; }
    //    public AnalogSensorType Chan5Type { get; set; }

    //    public bool Chan6Connected { get; set; }
    //    public string Chan6Name { get; set; }
    //    public AnalogSensorType Chan6Type { get; set; }

    //    public bool Chan7Connected { get; set; }
    //    public string Chan7Name { get; set; }
    //    public AnalogSensorType Chan7Type { get; set; }

    //    public bool Chan8Connected { get; set; }
    //    public string Chan8Name { get; set; }
    //    public AnalogSensorType Chan8Type { get; set; }

    //    public bool Chan9Connected { get; set; }
    //    public string Chan9Name { get; set; }
    //    public AnalogSensorType Chan9Type { get; set; }

    //    public bool Chan10Connected { get; set; }
    //    public string Chan10Name { get; set; }
    //    public AnalogSensorType Chan10Type { get; set; }

    //    public bool Chan11Connected { get; set; }
    //    public string Chan11Name { get; set; }
    //    public AnalogSensorType Chan11Type { get; set; }

    //    public bool Chan12Connected { get; set; }
    //    public string Chan12Name { get; set; }
    //    public AnalogSensorType Chan12Type { get; set; }

    //    public bool Chan13Connected { get; set; }
    //    public string Chan13Name { get; set; }
    //    public AnalogSensorType Chan13Type { get; set; }

    //    public bool Chan14Connected { get; set; }
    //    public string Chan14Name { get; set; }
    //    public AnalogSensorType Chan14Type { get; set; }

    //    public bool Chan15Connected { get; set; }
    //    public string Chan15Name { get; set; }
    //    public AnalogSensorType Chan15Type { get; set; }

    //    public bool Chan16Connected { get; set; }
    //    public string Chan16Name { get; set; }
    //    public AnalogSensorType Chan16Type { get; set; }
    //}
}

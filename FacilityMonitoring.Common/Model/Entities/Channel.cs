﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Model {
    //High: High=true
    //Low:  Low=true
    public enum LogicType { HIGH, LOW }

    public abstract class Channel {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChannelNumber { get; set; }
        public bool Connected { get; set; }
        public bool Bypass { get; set; }
        public string PropertyMap { get; set; }
        public LogicType Logic { get; set; }

        public int GenericMonitorBoxId { get; set; }
        public virtual GenericMonitorBox GenericMonitorBox { get; set; }

    }

    public partial class AnalogChannel:Channel  {
        public int? SensorTypeId { get; set; }
        public virtual SensorType SensorType { get; set; }

        public double Slope { get; set; }
        public double Offset { get; set; }
        public double Resistance { get; set; }

        public double Alarm1SetPoint { get; set; }
        public double Alarm2SetPoint { get; set; }
        public double Alarm3SetPoint { get; set; }

        public double ValueDivisor { get; set; }

        public AnalogChannel(string name, int chnum, bool connected, string pName){
            this.Name = name;
            this.ChannelNumber = chnum;
            this.Connected = connected;
            this.PropertyMap = pName;
            this.Bypass = false;
            this.Logic = LogicType.HIGH;
        }

        public AnalogChannel() {

        }

    }

    public partial class DigitalInputChannel:Channel  {
        

        public DigitalInputChannel(string name, int chnum, bool connected, string pname, LogicType ltype) {
            this.Logic = ltype;
            this.Name = name;
            this.ChannelNumber = chnum;
            this.Connected = connected;
            this.PropertyMap = pname;
            this.Bypass = false;
        }

        public DigitalInputChannel() {

        }
    }

    public partial class DigitalOutputChannel:Channel {
        public LogicType Logic { get; set; }

        public DigitalOutputChannel(string name, int chnum, bool connected, string pname, LogicType ltype) {
            this.Logic = ltype;
            this.Name = name;
            this.ChannelNumber = chnum;
            this.Connected = connected;
            this.PropertyMap = pname;
            this.Bypass = false;
        }

        public DigitalOutputChannel() {

        }
    }
}

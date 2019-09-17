using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Hardware;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace FacilityMonitoring.Common.Server {
    public class ModbusServer {
        public Timer _readTimer;
        public FacilityContext _context;
        public List<ModbusDevice> Devices;
        private object _lock = new object();

        public ModbusServer() {
            this._context = new FacilityContext();
            this._readTimer = new Timer(5000);
            this._readTimer.Elapsed += new ElapsedEventHandler(this.TimerEvent);
        }

        private void Setup() {

        }

        private async void TimerEvent(object sender, ElapsedEventArgs e) {

        }


    }
}

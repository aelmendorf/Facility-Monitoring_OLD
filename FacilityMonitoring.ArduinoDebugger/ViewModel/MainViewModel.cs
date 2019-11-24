using DevExpress.Mvvm;
using FacilityMonitoring.ArduinoDebugger.Common;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FacilityMonitoring.Common.ModbusServices.Operations;
using System.Timers;
using System.Threading.Tasks;

namespace FacilityMonitoring.ArduinoDebugger.ViewModel {
    public class MainViewModel:ViewModelBase {
        private ModbusOperations _modbus;
        protected IDispatcherService DispatcherService { get => this.GetService<IDispatcherService>(); }
        private Timer _timer;

        public ObservableCollection<AnalogModel> AnalogModels { get; set; }

        public AsyncCommand Start { get; private set; }
        public AsyncCommand Stop { get; private set; }

        public MainViewModel() {
            this.Start = new AsyncCommand(this.StartHandler);
            this.Stop = new AsyncCommand(this.StopHandler);
            this._modbus = new ModbusOperations("172.20.4....", 502);
        }

        public async Task StartHandler() {
            await Task.Run(() => {
                this._timer = new Timer(500);
                this._timer.Elapsed += this._timer_Elapsed;
                this._timer.AutoReset = true;
                this._timer.Start();
            });
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            var data = await this._modbus.ReadRegistersAndCoilsAsync(0, 4, 0, 4);
            this.DispatcherService.BeginInvoke(() => {
                //update UI
            });
        }

        public async Task StopHandler() {
            await Task.Run(() => {
                this._timer.Stop();
            });
        }
    }
}

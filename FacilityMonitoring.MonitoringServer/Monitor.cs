using System;
using System.Threading;
using System.Threading.Tasks;
using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Server;
using FacilityMonitoring.Common.Data_Layer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Modbus.Device;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using ModbusDevice = FacilityMonitoring.Common.Model.ModbusDevice;

namespace FacilityMonitoring.MonitoringServer {
    public class Monitor : BackgroundService {
        private readonly ILogger<Monitor> _logger;
        private readonly IHubContext<MonitorHub, IFacilityAmmoniaReading> _hubContext;
        //private readonly FacilityContext _context;
        private List<AmmoniaTankView> _dataViews;

        public Monitor(ILogger<Monitor> logger,IHubContext<MonitorHub, IFacilityAmmoniaReading> hubContext) {
            this._logger = logger;
            this._hubContext = hubContext;


            this._dataViews = new List<AmmoniaTankView>(){
                    new AmmoniaTankView("Tank 1","Online",Convert.ToDecimal(0.00),Convert.ToDecimal(0.00),false,false),
                    new AmmoniaTankView("Tank 2","Online",Convert.ToDecimal(0.00),Convert.ToDecimal(0.00),false,false),
                    new AmmoniaTankView("Tank 3","Online",Convert.ToDecimal(0.00),Convert.ToDecimal(0.00),false,false),
                    new AmmoniaTankView("Tank 4","Online",Convert.ToDecimal(0.00),Convert.ToDecimal(0.00),false,false)
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                _logger.LogInformation($"Monitor is running at: {DateTime.Now}");
                await this.Broadcast();
                await Task.Delay(3000);
            }
        }

        public IEnumerable<AmmoniaTankView> GetAllTanks() {
            return this._dataViews;
        }

        private async Task Broadcast() {
            ushort[] regData;
            bool[] coilData;
            using(FacilityContext context=new FacilityContext()) {
                ModbusDevice device =context.ModbusDevices.FirstOrDefault(x => x.Identifier == "NH3_Box");


                if (device != null) {
                    using (TcpClient client = new TcpClient(device.IpAddress, device.Port)) {
                        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                        regData = await master.ReadHoldingRegistersAsync(0, 70);
                        coilData = await master.ReadCoilsAsync(0, 10);
                        client.Close();
                    }
                    AmmoniaBoxReading reading = new AmmoniaBoxReading(DateTime.Now, "Tank 2 Loaded", device);
                    reading.Tank1Weight = BitConverter.ToInt32(BitConverter.GetBytes(regData[1]).Concat(BitConverter.GetBytes(regData[0])).ToArray(), 0);
                    reading.Tank2Weight = BitConverter.ToInt32(BitConverter.GetBytes(regData[3]).Concat(BitConverter.GetBytes(regData[2])).ToArray(), 0);
                    reading.Tank3Weight = BitConverter.ToInt32(BitConverter.GetBytes(regData[5]).Concat(BitConverter.GetBytes(regData[4])).ToArray(), 0);
                    reading.Tank4Weight = BitConverter.ToInt32(BitConverter.GetBytes(regData[7]).Concat(BitConverter.GetBytes(regData[6])).ToArray(), 0);

                    reading.Tank1Tare = (int)regData[56];
                    reading.Tank2Tare = (int)regData[57];
                    reading.Tank3Tare = (int)regData[58];
                    reading.Tank4Tare = (int)regData[59];

                    reading.Tank1Temperature = (int)regData[60];
                    reading.Tank2Temperature = (int)regData[61];
                    reading.Tank3Temperature = (int)regData[62];
                    reading.Tank4Temperature = (int)regData[63];

                    reading.Heater1DutyCycle = (int)regData[66];
                    reading.Heater2DutyCycle = (int)regData[67];
                    reading.Heater3DutyCycle = (int)regData[68];
                    reading.Heater4DutyCycle = (int)regData[69];

                    reading.Tank1Warning = coilData[2];
                    reading.Tank2Warning = coilData[3];
                    reading.Tank3Warning = coilData[4];
                    reading.Tank4Warning = coilData[5];
                    reading.Tank1Alarm = coilData[6];
                    reading.Tank2Alarm = coilData[7];
                    reading.Tank3Alarm = coilData[8];
                    reading.Tank4Alarm = coilData[9];


                    this._dataViews[0].Weight = Convert.ToDecimal(reading.Tank1Weight);
                    this._dataViews[0].Temperature = Convert.ToDecimal(reading.Tank1Temperature);
                    this._dataViews[0].Status = "Online";
                    this._dataViews[0].Alarm = (reading.Tank1Alarm) ? "Ammonia Is Below Limit" : "Okay";
                    this._dataViews[0].Warning = (reading.Tank1Alarm) ? "Ammonia Is Getting Low" : "Okay";

                    this._dataViews[1].Weight = Convert.ToDecimal(reading.Tank2Weight);
                    this._dataViews[1].Temperature = Convert.ToDecimal(reading.Tank2Temperature);
                    this._dataViews[1].Status = "Online";
                    this._dataViews[1].Alarm = (reading.Tank2Alarm) ? "Ammonia Is Below Limit" : "Okay";
                    this._dataViews[1].Warning = (reading.Tank2Alarm) ? "Ammonia Is Getting Low" : "Okay";

                    this._dataViews[2].Weight = Convert.ToDecimal(reading.Tank3Weight);
                    this._dataViews[2].Temperature = Convert.ToDecimal(reading.Tank3Temperature);
                    this._dataViews[2].Status = "Online";
                    this._dataViews[2].Alarm = (reading.Tank3Alarm) ? "Ammonia Is Below Limit" : "Okay";
                    this._dataViews[2].Warning = (reading.Tank3Alarm) ? "Ammonia Is Getting Low" : "Okay";

                    this._dataViews[3].Weight = Convert.ToDecimal(reading.Tank4Weight);
                    this._dataViews[3].Temperature = Convert.ToDecimal(reading.Tank4Temperature);
                    this._dataViews[3].Status = "Online";
                    this._dataViews[3].Alarm = (reading.Tank4Alarm) ? "Ammonia Is Below Limit" : "Okay";
                    this._dataViews[3].Warning = (reading.Tank4Alarm) ? "Ammonia Is Getting Low" : "Okay";



                    await this._hubContext.Clients.All.BroadcastAllTanks(this._dataViews);
                }
            }
        }
    }
}

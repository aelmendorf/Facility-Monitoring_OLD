using System;
using System.Linq;
using System.Net.Sockets;
using System.Timers;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Modbus.Device;
using ModbusDevice = FacilityMonitoring.Common.Model.ModbusDevice;


namespace FacilityMonitoring.ConsoleTesting {
    class Program {
        static void Main(string[] args) {
            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Interval = 3000;
            timer.Start();
            Console.WriteLine("Started!");
            Console.WriteLine("Press Any Key To Quit");
            Console.ReadKey();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            using (FacilityContext context = new FacilityContext()) {
                var device = context.ModbusDevices.Include(x => x.Readings).FirstOrDefault(x => x.Identifier == "NH3_Box");
                ushort[] regData;
                bool[] coilData;
                using (TcpClient client = new TcpClient(device.IpAddress, device.Port)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    regData = master.ReadHoldingRegisters(0, 70);
                    coilData = master.ReadCoils(0, 10);
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

                device.Readings.Add(reading);
                context.Readings.Add(reading);
                context.SaveChanges();
                Console.WriteLine("Measure: {0}", DateTime.Now);
                

            }
        }

        public static void CreateDevices() {
            using (FacilityContext context = new FacilityContext()) {
                ModbusDevice gen1 = new ModbusDevice("H2-1", "H2-1", "172.21.100.25", 502, 1, "Offline");
                ModbusDevice gen2 = new ModbusDevice("H2-2", "H2-2", "172.21.100.26", 502, 1, "Offline");
                ModbusDevice gen3 = new ModbusDevice("H2-3", "H2-3", "172.21.100.27", 502, 1, "Offline");
                ModbusDevice box = new ModbusDevice("NH3_Box", "Ammonia Controller", "172.21.100.29", 502, 0, "Offline");

                context.ModbusDevices.Add(gen1);
                context.ModbusDevices.Add(gen2);
                context.ModbusDevices.Add(gen3);
                context.ModbusDevices.Add(box);
                context.SaveChanges();
                Console.WriteLine("Should be saved");
                Console.ReadKey();
            }
        }
    }
}

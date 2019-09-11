using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Timers;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Console_Table;
using System.Collections.Generic;
using System.Text;
using FacilityMonitoring.Common.Hardware;

namespace FacilityMonitoring.ConsoleTesting
{
    class Program {

        static void Main(string[] args) {
            //TestWarning(true);
            //TestWarning(false);
            TestAlarm(true);
            TestAlarm(false);
            //TestMaintenance(true);
            //TestMaintenance(false);
        }

        public static void TestWarning(bool on_off) {
            using (var context = new FacilityContext()) {
                var device = context.ModbusDevices
                    .OfType<GenericMonitorBox>()
                    .AsNoTracking()
                    .Include(e => e.Registers)
                        .ThenInclude(e => e.SensorType)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(device);
                    if (!operations.SetWarning(on_off)) {
                        Console.WriteLine("Done! Press any key to exit");
                    } else {
                        Console.WriteLine("Error Send Failed");
                    }
                } else {
                    Console.WriteLine("Error: Device Not Found");
                }
            }
            Console.ReadKey();
        }

        public static void TestMaintenance(bool on_off) {
            using (var context = new FacilityContext()) {
                var device = context.ModbusDevices
                    .OfType<GenericMonitorBox>()
                    .AsNoTracking()
                    .Include(e => e.Registers)
                        .ThenInclude(e => e.SensorType)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(device);
                    if (!operations.SetMaintenance(on_off)) {
                        Console.WriteLine("Done! Press any key to exit");
                    } else {
                        Console.WriteLine("Error Send Failed");
                    }
                } else {
                    Console.WriteLine("Error: Device Not Found");
                }
            }
            Console.ReadKey();
        }

        public static void TestAlarm(bool on_off) {
            using (var context = new FacilityContext()) {
                var device = context.ModbusDevices
                    .OfType<GenericMonitorBox>()
                    .AsNoTracking()
                    .Include(e => e.Registers)
                        .ThenInclude(e => e.SensorType)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(device);
                    if (!operations.SetAlarm(on_off)) {
                        Console.WriteLine("Done! Press any key to exit");
                    } else {
                        Console.WriteLine("Error Send Failed");
                    }
                } else {
                    Console.WriteLine("Error: Device Not Found");
                }
            }
            Console.ReadKey();
        }

        public static void TestRead() {
            using (var context = new FacilityContext()) {
                var device = context.ModbusDevices
                    .OfType<GenericMonitorBox>()
                    .AsNoTracking()
                    .Include(e => e.Registers)
                        .ThenInclude(e => e.SensorType)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(device);
                    var reading = (GenericBoxReading)operations.ReadAll();
                    if (reading != null) {
                        foreach (var register in device.Registers.OfType<AnalogChannel>().OrderBy(e => e.RegisterIndex)) {
                            Console.WriteLine("{0}: {1}", register.Name, Convert.ToDouble(reading[register.PropertyMap]));
                        }
                        device.Readings.Add(reading);
                        context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                        context.Readings.Add(reading);
                        context.SaveChanges();
                        Console.WriteLine("Done! Press any key to exit");
                    } else {
                        Console.WriteLine("Error Reading Failed");
                    }
                } else {
                    Console.WriteLine("Error: Device Not Found");
                }
            }
            Console.ReadKey();
        }
    }
}

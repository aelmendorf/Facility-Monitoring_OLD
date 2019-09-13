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
using FacilityMonitoring.Common.Harware;
using System.Reflection;
using FacilityMonitoring.Common.Data;

namespace FacilityMonitoring.ConsoleTesting
{
    class Program {

        static void Main(string[] args) {
            //TestWarning(true);
            //TestWarning(false);
            //TestAlarm(true);
            //TestAlarm(false);
            //TestMaintenance(true);
            //TestMaintenance(false);
            //TestRead();
            //ImportModbus();
            //CreateAmmoniaController();
            //CreateAmmoniaController();
            //TestAmmoniaRead();
            //TestSetCal(true);
            //TestGetCal();
            //TestSendCal();
        }

        public static void CreateAmmoniaController() {
            using var context = new FacilityContext();
            AmmoniaController controller = new AmmoniaController();
            controller.Identifier = "AmmoniaController";
            controller.IpAddress = "172.21.100.29";
            controller.Port = 502;
            controller.RegisterBaseAddress = 0;
            controller.ReadRegisterLength = 70;
            controller.CoilBaseAddress = 0;
            controller.ReadCoilLength = 10;
            controller.DataForInputAddr = 0;
            controller.CalModeAddr = 1;
            controller.CalInputBaseAddr = 70;
            controller.CalInputLength = 12;
            controller.SlaveAddress = 0;
            controller.State = DeviceState.OKAY;
            context.ModbusDevices.Add(controller);
            context.SaveChanges();
            Console.WriteLine("Should be done");
            Console.ReadKey();
        }

        public static void TestSetCal(bool on_off) {
            using var context = new FacilityContext();
            var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
            if (controller != null) {
                AmmoniaControllerOperations operations = new AmmoniaControllerOperations(controller);
                if (operations.SetCalibrationMode(on_off)) {
                    Console.WriteLine("Done.  Press Any Key To Turn Off");
                    Console.ReadKey();
                    if (operations.SetCalibrationMode(!on_off)) {
                        Console.WriteLine("Done!");
                    } else {
                        Console.WriteLine("Error: Cal Mode Failed");
                    }
                } else {
                    Console.WriteLine("Error: Cal Mode Failed");
                }
            }
            Console.ReadKey();
        }

        public static void TestGetCal() {
            using var context = new FacilityContext();
            var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
            if (controller != null) {
                AmmoniaControllerOperations operations = new AmmoniaControllerOperations(controller);
                var calibration = operations.ReadTankCalibration(1);
                ConsoleTable table = new ConsoleTable();
                PropertyInfo[] properties = typeof(AmmoniaCalibrationData).GetProperties();
                List<object> row = new List<object>();
                foreach(var property in properties) {
                    table.Columns.Add(property.Name);
                    row.Add(property.GetValue(calibration));
                }
                table.AddRow(row.ToArray());
                Console.WriteLine(table.ToMinimalString());
            }
            Console.ReadKey();
        }

        public static void TestSendCal() {
            using var context = new FacilityContext();
            var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
            if (controller != null) {
                AmmoniaControllerOperations operations = new AmmoniaControllerOperations(controller);
                var cal = operations.ReadTankCalibration(2);
                if (cal != null) {
                    cal.ActualNonZero = 30;
                    if(operations.SetCalibration(cal, 3)) {
                        Console.WriteLine("Done");
                    } else {
                        Console.WriteLine("Error:  Send Failed");
                    }
                } else {
                    Console.WriteLine("Error: Retrieve Cal Failed");
                }
            } else {
                Console.WriteLine("Error: Controller Not Found");
            }
            Console.ReadKey();
        }

        public static void TestAmmoniaRead() {
            using var context = new FacilityContext();
            var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e=>e.Readings).FirstOrDefault(e=>e.Identifier== "AmmoniaController");
            if (controller != null) {
                AmmoniaControllerOperations operations = new AmmoniaControllerOperations(controller);
                var reading = operations.ReadAll();
                if (reading != null) {
                    context.AmmoniaControllerReadings.Add(reading);
                    context.SaveChanges();
                    PropertyInfo[] properties = typeof(AmmoniaControllerReading).GetProperties();
                    ConsoleTable table = new ConsoleTable();
                    //table.Columns.Add(properties.Select(e => e.Name));
                    List<object> row = new List<object>();
                    foreach(var property in properties) {
                        table.Columns.Add(property.Name);
                        row.Add(property.GetValue(reading));
                    }
                    table.AddRow(row.ToArray());
                    Console.WriteLine(table.ToMinimalString());
                } else {
                    Console.WriteLine("Error: Read Failed");
                }
            } else {
                Console.WriteLine("Error: Controller not found!");
            }
            Console.ReadKey();
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
                        context.GenericBoxReadings.Add(reading);
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

        private static void ImportModbus() {
            using (FacilityContext context = new FacilityContext()) {
                GenericMonitorBox monitorBox = new GenericMonitorBox();
                monitorBox.IpAddress = "172.21.100.30";
                monitorBox.Port = 502;
                monitorBox.Identifier = "GasBay";
                monitorBox.SlaveAddress = 0;
                monitorBox.Status = "Okay";
                monitorBox.AnalogChannelCount = 16;
                monitorBox.DigitalInputChannelCount = 39;
                monitorBox.DigitalOutputChannelCount = 10;
                monitorBox.ModbusComAddr = 39;
                monitorBox.SoftwareMaintAddr = 40;
                monitorBox.WarningAddr = 41;
                monitorBox.AlarmAddr = 42;
                context.ModbusDevices.Add(monitorBox);
                context.SaveChanges();
                if (ImportModbusSettings.ImportSensorType(monitorBox, context)) {
                    Console.WriteLine("Success: Sensor Types Imported");
                } else {
                    Console.WriteLine("Error: Sensor Import Failed");
                }

                if (ImportModbusSettings.ImportAnalog(monitorBox, context)) {
                    Console.WriteLine("Success: Analog Channels Imported");
                } else {
                    Console.WriteLine("Error: Analog Import Failed");
                }

                if (ImportModbusSettings.ImportDigital(monitorBox, context)) {
                    Console.WriteLine("Success: Digital Channels Imported");
                } else {
                    Console.WriteLine("Error: Digital Import Failed");
                }

                if (ImportModbusSettings.ImportOutput(monitorBox, context)) {
                    Console.WriteLine("Success: Output Channels Imported");
                } else {
                    Console.WriteLine("Error: Output Import Failed");
                }
                Console.WriteLine();
                Console.WriteLine("Done, Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}

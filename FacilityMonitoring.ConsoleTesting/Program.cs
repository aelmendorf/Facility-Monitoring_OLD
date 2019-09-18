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
using System.Reflection;
using FacilityMonitoring.Common.Data;
using System.IO;

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
            TestAmmoniaRead();
            //TestSetCal(true);
            //TestGetCal();
            //TestSendCal();

            //ExportH2ReadingParam();
            //ImportModbusH2("Generator 1", "172.21.100.25", 1);
            //ImportModbusH2("Generator 2", "172.21.100.26", 1);
            //ImportModbusH2("Generator 3", "172.21.100.27", 1);

            //TestGeneratorRead("Generator 1");
            //TestGeneratorRead("Generator 2");
            //TestGeneratorRead("Generator 3");




        }

        public static void ExportH2ReadingParam() {
            PropertyInfo[] properties = typeof(H2GenReading).GetProperties();
            StringBuilder builder = new StringBuilder();
            foreach(var property in properties) {
                builder.AppendFormat("{0}\t{1}", property.Name, property.PropertyType.Name).AppendLine();
            }
            File.WriteAllText(@"D:\Software Development\Monitoring\ImportFiles\H2GenParam.txt", builder.ToString());
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

        //public static void TestSetCal(bool on_off) {
        //    using var context = new FacilityContext();
        //    var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
        //    if (controller != null) {
        //        AmmoniaControllerOperations operations = new AmmoniaControllerOperations(controller);
        //        if (operations.SetCalibrationMode(on_off)) {
        //            Console.WriteLine("Done.  Press Any Key To Turn Off");
        //            Console.ReadKey();
        //            if (operations.SetCalibrationMode(!on_off)) {
        //                Console.WriteLine("Done!");
        //            } else {
        //                Console.WriteLine("Error: Cal Mode Failed");
        //            }
        //        } else {
        //            Console.WriteLine("Error: Cal Mode Failed");
        //        }
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestGetCal() {
        //    using var context = new FacilityContext();
        //    var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
        //    if (controller != null) {
        //        AmmoniaControllerOperations operations = new AmmoniaControllerOperations(context, controller);
        //        var calibration = operations.ReadTankCalibration(1);
        //        ConsoleTable table = new ConsoleTable();
        //        PropertyInfo[] properties = typeof(AmmoniaCalibrationData).GetProperties();
        //        List<object> row = new List<object>();
        //        foreach (var property in properties) {
        //            table.Columns.Add(property.Name);
        //            row.Add(property.GetValue(calibration));
        //        }
        //        table.AddRow(row.ToArray());
        //        Console.WriteLine(table.ToMinimalString());
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestSendCal() {
        //    using var context = new FacilityContext();
        //    var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e => e.Readings).FirstOrDefault(e => e.Identifier == "AmmoniaController");
        //    if (controller != null) {
        //        AmmoniaControllerOperations operations = new AmmoniaControllerOperations(controller);
        //        var cal = operations.Device.
        //        if (cal != null) {
        //            cal.ActualNonZero = 30;
        //            if (operations.SetCalibration(cal, 3)) {
        //                Console.WriteLine("Done");
        //            } else {
        //                Console.WriteLine("Error:  Send Failed");
        //            }
        //        } else {
        //            Console.WriteLine("Error: Retrieve Cal Failed");
        //        }
        //    } else {
        //        Console.WriteLine("Error: Controller Not Found");
        //    }
        //    Console.ReadKey();
        //}

        public static void TestGeneratorRead(string generator) {
            using var context = new FacilityContext();
            var controller = context.ModbusDevices.OfType<H2Generator>().Include(e => e.H2Readings).Include(e=>e.Registers).FirstOrDefault(e => e.Identifier == generator);
            if (controller != null) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                GeneratorOperations operations = new GeneratorOperations(context,controller);
                if (operations.Read()) {
                    stopwatch.Stop();
                    Console.WriteLine("{0} done! Elapsed Time: {1}",generator,stopwatch.ElapsedMilliseconds);
                } else {
                    Console.WriteLine("Error: Read Failed");
                }
            } else {
                Console.WriteLine("Error: Controller not found!");
            }
            Console.ReadKey();
        }

        public static void TestAmmoniaRead() {
            using var context = new FacilityContext();
            var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e=>e.Readings).FirstOrDefault(e=>e.Identifier== "AmmoniaController");
            if (controller != null) {
                AmmoniaControllerOperations operations = new AmmoniaControllerOperations(context,controller);
                //var reading = operations.ReadAll();
                if (operations.Read()) {
                    Console.WriteLine();
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
                    .Include(e => e.BoxReadings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(context,device);
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
                    .Include(e => e.BoxReadings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(context,device);
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
                    .Include(e => e.BoxReadings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(context,device);
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
                    .Include(e => e.BoxReadings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    MonitorBoxOperations operations = new MonitorBoxOperations(context, device);
                    if (operations.Read()) {
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

        //private static void ImportModbusGeneric() {
        //    using (FacilityContext context = new FacilityContext()) {
        //        GenericMonitorBox monitorBox = new GenericMonitorBox();
        //        monitorBox.IpAddress = "172.21.100.30";
        //        monitorBox.Port = 502;
        //        monitorBox.Identifier = "GasBay";
        //        monitorBox.SlaveAddress = 0;
        //        monitorBox.Status = "Okay";
        //        monitorBox.AnalogChannelCount = 16;
        //        monitorBox.DigitalInputChannelCount = 39;
        //        monitorBox.DigitalOutputChannelCount = 10;
        //        monitorBox.ModbusComAddr = 39;
        //        monitorBox.SoftwareMaintAddr = 40;
        //        monitorBox.WarningAddr = 41;
        //        monitorBox.AlarmAddr = 42;
        //        context.ModbusDevices.Add(monitorBox);
        //        context.SaveChanges();
        //        if (ImportModbusSettings.ImportSensorType(monitorBox, context)) {
        //            Console.WriteLine("Success: Sensor Types Imported");
        //        } else {
        //            Console.WriteLine("Error: Sensor Import Failed");
        //        }

        //        if (ImportModbusSettings.ImportAnalog(monitorBox, context)) {
        //            Console.WriteLine("Success: Analog Channels Imported");
        //        } else {
        //            Console.WriteLine("Error: Analog Import Failed");
        //        }

        //        if (ImportModbusSettings.ImportDigital(monitorBox, context)) {
        //            Console.WriteLine("Success: Digital Channels Imported");
        //        } else {
        //            Console.WriteLine("Error: Digital Import Failed");
        //        }

        //        if (ImportModbusSettings.ImportOutput(monitorBox, context)) {
        //            Console.WriteLine("Success: Output Channels Imported");
        //        } else {
        //            Console.WriteLine("Error: Output Import Failed");
        //        }
        //        Console.WriteLine();
        //        Console.WriteLine("Done, Press any key to exit");
        //        Console.ReadKey();
        //    }
        //}

        //private static void ImportModbusH2(string name,string IpAddress,int slave) {
        //    using (FacilityContext context = new FacilityContext()) {
        //        H2Generator monitorBox = new H2Generator();
        //        monitorBox.Identifier = name;
        //        monitorBox.IpAddress =IpAddress;
        //        monitorBox.Port = 502;
        //        monitorBox.SlaveAddress = slave;
        //        monitorBox.Status = "Okay";
        //        context.ModbusDevices.Add(monitorBox);
        //        context.SaveChanges();
        //        if (ImportModbusSettings.ImportGeneratorRegisters(monitorBox, context)) {
        //            Console.WriteLine("Success: H2 Registers Imported");
        //        } else {
        //            Console.WriteLine("Error: Import Failed");
        //        }

        //        Console.WriteLine();
        //        Console.WriteLine("Done, Press any key to exit");
        //        Console.ReadKey();
        //    }
        //}
    }
}

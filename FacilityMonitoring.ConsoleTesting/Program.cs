using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Reflection;
using System.IO;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using FacilityMonitoring.Common.Services;

namespace FacilityMonitoring.ConsoleTesting {
    class Program {

        static void Main(string[] args) {
            //BuildAlertDef();
            //TestingNewLinq();
            //TestWarning(true);
            //TestWarning(false);
            //TestAlarm(true);
            //TestAlarm(false);
            //TestMaintenance(true);
            //TestMaintenance(false);
            //TestRead();

            //CreateAmmoniaController();
            //ImportModbusGeneric();
            //TestAmmoniaRead();
            //TestSetCal(true);
            //TestGetCal();
            //TestSendCal();

            //ExportH2ReadingParam();
            //ExportNH3ReadingParam();
            //ExportH2SystemErrorParam();
            //ImportModbusH2("Generator 1", "172.21.100.25", 1);
            //ImportModbusH2("Generator 2", "172.21.100.26", 1);
            //ImportModbusH2("Generator 3", "172.21.100.27", 1);

            //TestGeneratorRead("Generator 1");
            //TestGeneratorRead("Generator 2");
            //TestGeneratorRead("Generator 3");

            //AddSensor();
            EmailService emailer = new EmailService();
            MessageBuilder builder = new MessageBuilder();

            builder.StartMessage();
            builder.AppendAlert("Test Analog Channel", "654.25", "ALARM3");
            builder.AppendAlert("Test Analog Channel 2", "255.25", "ALARM3");
            builder.AppendAlert("Test Analog Channel 3", "456.25", "ALARM3");
            builder.AppendAlert("Test Digital Channel 1", "Alarm", "Tripped");
            builder.AppendAlert("Test Digital Channel 2", "Alarm", "Tripped");
            builder.AppendAlert("Test Digital Channel 3", "Alarm", "Tripped");

            builder.AppendStatus("Test Analog Channel", "654.25");
            builder.AppendStatus("Test Analog Channel 2", "255.25");
            builder.AppendStatus("Test Analog Channel 3", "456.25");
            builder.AppendStatus("Test Digital Channel 1", "Alarm");
            builder.AppendStatus("Test Digital Channel 2", "Alarm");
            builder.AppendStatus("Test Digital Channel 3", "Alarm");

            emailer.SendMessage(builder.FinishMessage());

            Console.WriteLine("Should be done!");
            Console.ReadKey();
        }

        public static void Test<T>() {
            Console.WriteLine(typeof(T));
            Console.ReadKey();
        }

        public static void AddSensor() {
            using FacilityContext context = new FacilityContext();
            SensorType sensor = new SensorType();
            sensor.Name = "H2 Detector-LEL";
            sensor.ZeroPoint = 0;
            sensor.MaxPoint = 100;
            sensor.Units = "LEL%";
            context.Categories.Add(sensor);
            context.SaveChanges();

            Console.WriteLine("Should be done!");
            Console.ReadKey();

        }

        public static void BuildAlertDef() {
            using FacilityContext context = new FacilityContext();
            AlertSetting alarm = new AlertSetting();
            alarm.Name = "Alarm";
            alarm.Frequency = 1.0;
            alarm.Notification = NotificationType.EMAIL;
            alarm.AlertAction = AlertAction.ALARM;

            AlertSetting warn = new AlertSetting();
            warn.Name = "Warning";
            warn.Frequency = 4;
            warn.Notification = NotificationType.EMAIL;
            warn.AlertAction = AlertAction.WARN;

            AlertSetting SoftWarning = new AlertSetting();
            SoftWarning.Name = "Soft Warning";
            SoftWarning.Frequency = 24;
            SoftWarning.Notification = NotificationType.EMAIL;
            SoftWarning.AlertAction = AlertAction.SOFTWARN;

            AlertSetting Maintenance = new AlertSetting();
            Maintenance.Name = "Maintenance";
            Maintenance.Frequency = 0;
            Maintenance.AlertAction = AlertAction.MAINTENANCE;
            Maintenance.Notification = NotificationType.WEBSITE;

            AlertSetting none = new AlertSetting();
            none.Name = "Nothing";
            none.Frequency = 0;
            none.Notification = NotificationType.NONE;
            none.AlertAction = AlertAction.NOTHING;

            context.AlertSettings.Add(alarm);
            context.AlertSettings.Add(warn);
            context.AlertSettings.Add(Maintenance);
            context.AlertSettings.Add(SoftWarning);
            context.AlertSettings.Add(none);
            context.SaveChanges();
            Console.WriteLine("Should be done");
            Console.ReadKey();

        }

        //public static void TestEmail() {

        //}

        //public static void TestingNewLinq() {
        //    using FacilityContext context = new FacilityContext();

        //    var box =context.ModbusDevices.OfType<GenericMonitorBox>().Include(device => device.Registers).ThenInclude(register=>register.SensorType).SingleOrDefault(e => e.Id == 1);
        //    Console.WriteLine("Device: {0}", box.Identifier);
        //    foreach(var register in box.Registers) {
        //        if (register.SensorType != null) {
        //            Console.WriteLine("Register: {0}  Sensor: {1}", register.Name, register.SensorType.Name);
        //        } else {
        //            Console.WriteLine("Register: {0}  Sensor: NULL", register.Name);
        //        }

        //    }
        //    Console.ReadKey();
        //}

        //public static void ExportH2ReadingParam() {
        //    PropertyInfo[] properties = typeof(H2GenReading).GetProperties();
        //    StringBuilder builder = new StringBuilder();
        //    foreach(var property in properties) {
        //        builder.AppendFormat("{0}\t{1}", property.Name, property.PropertyType.Name).AppendLine();
        //    }
        //    File.WriteAllText(@"D:\Software Development\Monitoring\ImportFiles\H2GenParam.txt", builder.ToString());
        //}

        //public static void ExportNH3ReadingParam() {

        //    PropertyInfo[] properties = typeof(AmmoniaControllerReading).GetProperties();
        //    StringBuilder builder = new StringBuilder();
        //    foreach (var property in properties) {
        //        builder.AppendFormat("{0}\t{1}", property.Name, property.PropertyType.Name).AppendLine();
        //    }
        //    File.WriteAllText(@"D:\Software Development\Monitoring\ImportFiles\NH3GenParam.txt", builder.ToString());
        //}

        //public static void ExportH2SystemErrorParam() {
        //    PropertyInfo[] properties = typeof(GeneratorSystemError).GetProperties();
        //    StringBuilder builder = new StringBuilder();
        //    foreach (var property in properties) {
        //        builder.AppendFormat("{0}\t{1}", property.Name, property.PropertyType.Name).AppendLine();
        //    }
        //    File.WriteAllText(@"D:\Software Development\Monitoring\ImportFiles\H2SystemErrorParam.txt", builder.ToString());
        //}

        public static void CreateAmmoniaController() {
            using var context = new FacilityContext();
            TankScale controller = new TankScale();
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
            controller.ReadInterval = 1;
            controller.SaveInterval = 300;
            controller.AlarmSetPoint = 100;
            controller.WarningSetPoint = 150;
            controller.Tank1AlertEnabled = true;
            controller.Tank2AlertEnabled = true;
            controller.Tank3AlertEnabled = false;
            controller.Tank4AlertEnabled = false;
            controller.ActiveTank = 1;
            context.ModbusDevices.Add(controller);
            context.SaveChanges();
            Console.WriteLine("Should be done");
            Console.ReadKey();
        }

        private static void ImportModbusGeneric() {
            using (FacilityContext context = new FacilityContext()) {
                MonitorBox monitorBox = new MonitorBox();
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
                monitorBox.ReadInterval = 2;
                monitorBox.SaveInterval = 300;
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

        private static void ImportModbusH2(string name, string IpAddress, int slave) {
            using (FacilityContext context = new FacilityContext()) {
                H2Generator monitorBox = new H2Generator();
                monitorBox.Identifier = name;
                monitorBox.IpAddress = IpAddress;
                monitorBox.Port = 502;
                monitorBox.SlaveAddress = slave;
                monitorBox.Status = "Okay";
                monitorBox.ReadInterval = 10;
                monitorBox.SaveInterval = 30;
                context.ModbusDevices.Add(monitorBox);
                context.SaveChanges();
                if (ImportModbusSettings.ImportGeneratorRegisters(monitorBox, context)) {
                    Console.WriteLine("Success: H2 Registers Imported");
                } else {
                    Console.WriteLine("Error: Import Failed");
                }

                Console.WriteLine();
                Console.WriteLine("Done, Press any key to exit");
                Console.ReadKey();
            }
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

        //public static void TestGeneratorRead(string generator) {
        //    using var context = new FacilityContext();
        //    var controller = context.ModbusDevices.OfType<H2Generator>().Include(e => e.H2Readings).Include(e=>e.Registers).FirstOrDefault(e => e.Identifier == generator);
        //    if (controller != null) {
        //        Stopwatch stopwatch = new Stopwatch();
        //        stopwatch.Start();
        //        GeneratorOperations operations = new GeneratorOperations(context,controller);
        //        if (operations.Read()) {
        //            stopwatch.Stop();
        //            Console.WriteLine("{0} done! Elapsed Time: {1}",generator,stopwatch.ElapsedMilliseconds);
        //        } else {
        //            Console.WriteLine("Error: Read Failed");
        //        }
        //    } else {
        //        Console.WriteLine("Error: Controller not found!");
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestAmmoniaRead() {
        //    using var context = new FacilityContext();
        //    var controller = context.ModbusDevices.OfType<AmmoniaController>().Include(e=>e.Readings).FirstOrDefault(e=>e.Identifier== "AmmoniaController");
        //    if (controller != null) {
        //        AmmoniaControllerOperations operations = new AmmoniaControllerOperations(context,controller);
        //        //var reading = operations.ReadAll();
        //        if (operations.Read()) {
        //            Console.WriteLine();
        //        } else {
        //            Console.WriteLine("Error: Read Failed");
        //        }
        //    } else {
        //        Console.WriteLine("Error: Controller not found!");
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestWarning(bool on_off) {
        //    using (var context = new FacilityContext()) {
        //        var device = context.ModbusDevices
        //            .OfType<GenericMonitorBox>()
        //            .AsNoTracking()
        //            .Include(e => e.Registers)
        //                .ThenInclude(e => e.SensorType)
        //            .Include(e => e.BoxReadings)
        //            .FirstOrDefault(e => e.Identifier == "GasBay");
        //        if (device != null) {
        //            MonitorBoxOperations operations = new MonitorBoxOperations(context,device);
        //            if (!operations.SetWarning(on_off)) {
        //                Console.WriteLine("Done! Press any key to exit");
        //            } else {
        //                Console.WriteLine("Error Send Failed");
        //            }
        //        } else {
        //            Console.WriteLine("Error: Device Not Found");
        //        }
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestMaintenance(bool on_off) {
        //    using (var context = new FacilityContext()) {
        //        var device = context.ModbusDevices
        //            .OfType<GenericMonitorBox>()
        //            .AsNoTracking()
        //            .Include(e => e.Registers)
        //                .ThenInclude(e => e.SensorType)
        //            .Include(e => e.BoxReadings)
        //            .FirstOrDefault(e => e.Identifier == "GasBay");
        //        if (device != null) {
        //            MonitorBoxOperations operations = new MonitorBoxOperations(context,device);
        //            if (!operations.SetMaintenance(on_off)) {
        //                Console.WriteLine("Done! Press any key to exit");
        //            } else {
        //                Console.WriteLine("Error Send Failed");
        //            }
        //        } else {
        //            Console.WriteLine("Error: Device Not Found");
        //        }
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestAlarm(bool on_off) {
        //    using (var context = new FacilityContext()) {
        //        var device = context.ModbusDevices
        //            .OfType<GenericMonitorBox>()
        //            .AsNoTracking()
        //            .Include(e => e.Registers)
        //                .ThenInclude(e => e.SensorType)
        //            .Include(e => e.BoxReadings)
        //            .FirstOrDefault(e => e.Identifier == "GasBay");
        //        if (device != null) {
        //            MonitorBoxOperations operations = new MonitorBoxOperations(context,device);
        //            if (!operations.SetAlarm(on_off)) {
        //                Console.WriteLine("Done! Press any key to exit");
        //            } else {
        //                Console.WriteLine("Error Send Failed");
        //            }
        //        } else {
        //            Console.WriteLine("Error: Device Not Found");
        //        }
        //    }
        //    Console.ReadKey();
        //}

        //public static void TestRead() {
        //    using (var context = new FacilityContext()) {
        //        var device = context.ModbusDevices
        //            .OfType<GenericMonitorBox>()
        //            .AsNoTracking()
        //            .Include(e => e.Registers)
        //                .ThenInclude(e => e.SensorType)
        //            .Include(e => e.BoxReadings)
        //            .FirstOrDefault(e => e.Identifier == "GasBay");
        //        if (device != null) {
        //            MonitorBoxOperations operations = new MonitorBoxOperations(context, device);
        //            if (operations.Read()) {
        //                Console.WriteLine("Done! Press any key to exit");
        //            } else {
        //                Console.WriteLine("Error Reading Failed");
        //            }
        //        } else {
        //            Console.WriteLine("Error: Device Not Found");
        //        }
        //    }
        //    Console.ReadKey();
        //}


    }
}

﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Timers;
using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Modbus.Device;
using Console_Table;
using ModbusDevice = FacilityMonitoring.Common.Model.ModbusDevice;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.ConsoleTesting
{
    class Program {
        static string[] AnalogIdentifiers = {"H2 Detector 1","NH3 Detector","O2 Detector","Bad Channel","N2 Dew Point","H2 Detector",
                                            "Bad Channel" , "Bad Channel","Not Connected","Not Connected","Not Connected","Not Connected" ,"Not Connected","Not Connected" ,
                                            "Not Connected","Not Connected"};
        static double[] SlopeValues = {1.00476,1.00577,1.004007,1.00488,1.00448,1.00454,.99849,0.000,1.00448,1.00455,1.00450,1.00451,1.00456,1.00414,1.00456,0};
        static double[] OffsetValues = { 0.00998,0.00920,.008976,0.00919,0.00744,0.00791,-.00462,0.0000,0.00787,0.00763,0.00761,0.00761,0.00761,0.00833,0.00782,0.0000};
        static double[] RValues = {250.81,250.474,246.2776,210.223,250.58,240.204,332.018,0.000,250.902,250.918,250.684,250.808,251.002,250.576,250.821,0.000};
        static double[] MinValues = {4,4,4,0,4,4.152,0,0,0,0,0,0,0,0,0,0 };
        static double[] MaxValues = { 20, 20, 20, 0, 20, 20.868, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm1SetPoints = { 300, 10, 0, 0,  0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm2SetPoints = { 500, 25, 23,0, -118, 500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm3SetPoints = { 1000, 50, 25, 0, -118, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static void Main(string[] args) {
            //TestingFacilityModel();
            ////ViewModelTesting();
            //TestingCategories();
            //TestingCategories2();
            //TestingAnalogRead();
            //TestingAddChannels();
            //DisplayMeasurments();
            //TestingProgram();
            ReadAndDisplay2();
            //SendMaintNew();
        }

        private static void SendMaintNew() {
            if (CheckConnection("172.21.100.30", 500)) {
                using (var context = new FacilityContext()) {
                    var box = context.ModbusDevices.OfType<GenericMonitorBox>()
                        .Include(e => e.Channels)
                        .Include(e => e.Readings)
                        .FirstOrDefault(e => e.Identifier == "GasBay");
                    if (box != null) {
                        ushort[] regData = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
                        using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                            bool[] com = { true,true,false,false};
                            ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                            master.WriteMultipleCoils((ushort)box.ModbusComAddr, com);
                            while (true) {
                                var check = master.ReadCoils((ushort)box.ModbusComAddr, 1);
                                if (!check[0]) {
                                    break;
                                }
                            }
                            client.Close();
                        }
                        Console.WriteLine("Press any key to continue");
                    } else {
                        Console.WriteLine("Error: Box not found");
                    }

                }
            } else {
                Console.WriteLine("Connection Failed");
            }
            Console.ReadKey();
        }

        private static void ReadAndDisplay2() {
            using (var context = new FacilityContext()) {
                var box = context.ModbusDevices.OfType<GenericMonitorBox>()
                    .Include(e => e.Channels)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (box != null) {
                    while (true) { 
                        if (CheckConnection(box.IpAddress, 100)) {
                            ushort[] regData;
                            bool[] coilData;
                            using (TcpClient client = new TcpClient(box.IpAddress, 502)) {
                                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                                regData = master.ReadHoldingRegisters(0, (ushort)box.AnalogChannelCount);
                                coilData = master.ReadCoils(0, (ushort)box.DigitalInputChannelCount);
                                client.Close();
                            }

                            GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", box);
                            foreach (var channel in box.Channels.OfType<AnalogChannel>().OrderBy(e => e.ChannelNumber)) {
                                double x = regData[channel.ChannelNumber - 1];
                                x = (x / 1000);
                                double y = channel.Slope * x + channel.Offset;
                                double current = (channel.Resistance != 0) ? (y / channel.Resistance) * 1000 : 0.00;
                                reading[channel.PropertyMap] = current;
                            }

                            foreach (var channel in box.Channels.OfType<DigitalInputChannel>().OrderBy(e => e.ChannelNumber)) {
                                reading[channel.PropertyMap] = coilData[channel.ChannelNumber - 1];
                            }

                            box.Readings.Add(reading);
                            context.Readings.Add(reading);
                            context.SaveChanges();

                            var analog = context.Channels.OfType<AnalogChannel>().Include(e => e.SensorType).Where(e => e.Connected).OrderBy(e => e.ChannelNumber);
                            var digital = context.Channels.OfType<DigitalInputChannel>().Where(e => e.Connected).OrderBy(e => e.ChannelNumber);

                            ConsoleTable analogTable = new ConsoleTable();
                            ConsoleTable digitalTable = new ConsoleTable();

                            analogTable.AddColumn(analog.Select(e => e.Name).ToList());
                            digitalTable.AddColumn(digital.Select(e => e.Name).ToList());
                            List<object> tempAnalog = new List<object>();
                            List<object> tempDigital = new List<object>();
                            foreach (var read in box.Readings.OfType<GenericBoxReading>()) {
                                foreach (var channel in analog) {
                                    double value = Convert.ToDouble(read[channel.PropertyMap]);
                                    var calc = channel.GetEquationParameters();
                                    StringBuilder builder = new StringBuilder();
                                    builder.AppendFormat("({0},{1})", Math.Round(value, 3), Math.Round((value * calc.Item1 + calc.Item2), 3));
                                    tempAnalog.Add(builder.ToString());
                                }

                                foreach (var channel in digital) {
                                    tempDigital.Add(reading[channel.PropertyMap]);
                                }

                                analogTable.AddRow(tempAnalog.ToArray());
                                digitalTable.AddRow(tempDigital.ToArray());

                                tempAnalog.Clear();
                                tempDigital.Clear();
                            }
                            Console.WriteLine("Analog Channels");
                            Console.WriteLine(analogTable.ToMinimalString());
                            Console.WriteLine();
                            Console.WriteLine("Digital Channels");
                            Console.WriteLine(digitalTable.ToMinimalString());
                            Console.WriteLine();
                            Console.WriteLine("Press A to continue or E to Exit");
                            var key = Console.ReadKey().Key;
                            if (key == ConsoleKey.E) {
                                break;
                            } else {
                                Console.Clear();
                            }

                        } else {
                            Console.WriteLine("Connection Failed");
                        }
                    }
                } else {
                    Console.WriteLine("Error: Could not find device");
                    Console.ReadKey();
                }
            }
        }

        private static void DisplayMeasurments() {
            using(FacilityContext context=new FacilityContext()) {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                var box = context.ModbusDevices.OfType<GenericMonitorBox>()
                    .Include(e => e.Channels)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");

                if (box != null) {
                    var analog=context.Channels.OfType<AnalogChannel>().Include(e=>e.SensorType).Where(e => e.Connected).OrderBy(e=>e.ChannelNumber);
                    var digital= context.Channels.OfType<DigitalInputChannel>().Where(e => e.Connected).OrderBy(e => e.ChannelNumber);

                    ConsoleTable analogTable = new ConsoleTable();
                    ConsoleTable digitalTable = new ConsoleTable();

                    analogTable.AddColumn(analog.Select(e => e.Name).ToList());
                    digitalTable.AddColumn(digital.Select(e => e.Name).ToList());
                    List<object> tempAnalog = new List<object>();
                    List<object> tempDigital = new List<object>();
                    foreach (var reading in box.Readings.OfType<GenericBoxReading>()) {
                        foreach (var channel in analog) {
                            double value=Convert.ToDouble(reading[channel.PropertyMap]);
                            var calc = channel.GetEquationParameters();
                            StringBuilder builder = new StringBuilder();
                            builder.AppendFormat("({0},{1})", Math.Round(value,3), Math.Round((value * calc.Item1 + calc.Item2),3));
                            tempAnalog.Add(builder.ToString());
                        }

                        foreach(var channel in digital) {
                            tempDigital.Add(reading[channel.PropertyMap]);
                        }

                        analogTable.AddRow(tempAnalog.ToArray());
                        digitalTable.AddRow(tempDigital.ToArray());

                        tempAnalog.Clear();
                        tempDigital.Clear();
                    }
                    Console.WriteLine("Analog Channels");
                    Console.WriteLine(analogTable.ToMinimalString());
                    Console.WriteLine();
                    Console.WriteLine("Digital Channels");
                    Console.WriteLine(digitalTable.ToMinimalString());
                    timer.Stop();
                    Console.WriteLine();
                    Console.WriteLine("Elapsed Time(ms): {0}",timer.ElapsedMilliseconds);
                    Console.ReadKey();
                } else {
                    Console.WriteLine("Error: device not found");
                }
            }
        }

        private static void CalibrateAnalog() {
            using (var context = new FacilityContext()) {
                var box = context.ModbusDevices.OfType<GenericMonitorBox>()
                    .Include(e => e.Channels)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (box != null) {
                    var analog = context.Channels.OfType<AnalogChannel>()
                        .Include(e => e.SensorType)
                        .Where(e => e.Connected && e.GenericMonitorBoxId==box.Id)
                        .OrderBy(e => e.ChannelNumber).FirstOrDefault(e=>e.Name== "H2 Detector");



                } else {
                    Console.WriteLine("Error: Box Not Found");
                }
            }
        }

        private static void TestingAnalogRead() {
            using (var context = new FacilityContext()) {
                var box = context.ModbusDevices.OfType<GenericMonitorBox>()
                    .Include(e => e.Channels)
                    .Include(e => e.Readings)
                    .FirstOrDefault(e => e.Identifier == "GasBay");
                if (box != null) {
                    if (CheckConnection(box.IpAddress, 100)) {
                        ushort[] regData;
                        bool[] coilData;
                        using (TcpClient client = new TcpClient(box.IpAddress, 502)) {
                            ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                            regData = master.ReadHoldingRegisters(0, (ushort)box.AnalogChannelCount);
                            coilData = master.ReadCoils(0, (ushort)box.DigitalInputChannelCount);
                            client.Close();
                        }

                        GenericBoxReading reading = new GenericBoxReading(DateTime.Now, "", box);
                        foreach (var channel in box.Channels.OfType<AnalogChannel>().OrderBy(e=>e.ChannelNumber)) {
                            double x = regData[channel.ChannelNumber-1];
                            x = (x / 1000);
                            double y = channel.Slope * x + channel.Offset;
                            double current = (channel.Resistance != 0) ? (y / channel.Resistance) * 1000 : 0.00;
                            reading[channel.PropertyMap] = current;
                        }

                        foreach(var channel in box.Channels.OfType<DigitalInputChannel>().OrderBy(e => e.ChannelNumber)) {
                            reading[channel.PropertyMap] = coilData[channel.ChannelNumber-1];
                        }

                        box.Readings.Add(reading);
                        context.Readings.Add(reading);
                        context.SaveChanges();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    } else {
                        Console.WriteLine("Connection Failed");
                    }
                } else {
                    Console.WriteLine("Error: Could not find device");
                    Console.ReadKey();
                }
            }
        }

        private static void TestingCategories2() {
            using(var context=new FacilityContext()) {
                var h2Detect= context.Categories.OfType<SensorType>().FirstOrDefault(e => e.Name == "H2 Detector");
                var NH3Detect= context.Categories.OfType<SensorType>().FirstOrDefault(e => e.Name == "NH3 Detector");
                var O2Detect= context.Categories.OfType<SensorType>().FirstOrDefault(e => e.Name == "O2 Detector");
                var N2Detect= context.Categories.OfType<SensorType>().FirstOrDefault(e => e.Name == "N2 Dewpoint Detector");

                var h2_1 = context.Channels.OfType<AnalogChannel>().Include(e => e.SensorType).FirstOrDefault(e => e.Name == "H2 Detector 1");
                if (h2_1 != null) {
                    h2_1.Alarm1SetPoint = 50;
                    h2_1.Alarm2SetPoint = 500;
                    h2_1.Alarm3SetPoint = 1000;
                    h2_1.SensorType = h2Detect;
                    h2Detect.AnalogChannels.Add(h2_1);
                    context.SaveChanges();
                    Console.WriteLine("H2-1 Done");
                } else {
                    Console.WriteLine("Could not Find H2 Detector 1");
                }

                var Nh3 = context.Channels.OfType<AnalogChannel>().Include(e => e.SensorType).FirstOrDefault(e => e.Name == "NH3 Detector");
                if (Nh3 != null) {
                    Nh3.Alarm1SetPoint = 50;
                    Nh3.Alarm2SetPoint = 500;
                    Nh3.Alarm3SetPoint = 1000;
                    Nh3.SensorType = NH3Detect;
                    NH3Detect.AnalogChannels.Add(Nh3);
                    context.SaveChanges();
                    Console.WriteLine("Nh3 Done");
                } else {
                    Console.WriteLine("Could not Find H2 Detector 1");
                }

                var o2 = context.Channels.OfType<AnalogChannel>().Include(e => e.SensorType).FirstOrDefault(e => e.Name == "O2 Detector");
                if (o2 != null) {
                    o2.Alarm1SetPoint = 50;
                    o2.Alarm2SetPoint = 500;
                    o2.Alarm3SetPoint = 1000;
                    o2.SensorType = O2Detect;
                    O2Detect.AnalogChannels.Add(o2);
                    context.SaveChanges();
                    Console.WriteLine("o2 Done");
                } else {
                    Console.WriteLine("Could not Find H2 Detector 1");
                }

                var n2 = context.Channels.OfType<AnalogChannel>().Include(e => e.SensorType).FirstOrDefault(e => e.Name == "N2 Dew Point");
                if (n2 != null) {
                    n2.Alarm1SetPoint = 50;
                    n2.Alarm2SetPoint = 500;
                    n2.Alarm3SetPoint = 1000;
                    n2.SensorType = N2Detect;
                    N2Detect.AnalogChannels.Add(n2);
                    context.SaveChanges();
                    Console.WriteLine("n2 Done");
                } else {
                    Console.WriteLine("Could not Find H2 Detector 1");
                }

                var h2_2 = context.Channels.OfType<AnalogChannel>().Include(e => e.SensorType).FirstOrDefault(e => e.Name == "H2 Detector");
                if (h2_2 != null) {
                    h2_2.Alarm1SetPoint = 50;
                    h2_2.Alarm2SetPoint = 500;
                    h2_2.Alarm3SetPoint = 1000;
                    h2_2.SensorType = h2Detect;
                    h2Detect.AnalogChannels.Add(h2_2);
                    context.SaveChanges();
                    Console.WriteLine("H2-2 Done");
                } else {
                    Console.WriteLine("Could not Find H2 Detector 1");
                }
            }
        }

        private static void TestingCategories() {
            using (var context = new FacilityContext()) {
                SensorType h2 = new SensorType();
                h2.ZeroPoint = 0;
                h2.MaxPoint = 1000;
                h2.Name = "H2 Detector";

                SensorType o2 = new SensorType();
                o2.ZeroPoint = 0;
                o2.MaxPoint = 26.66;
                o2.Name = "O2 Detector";

                SensorType NH3 = new SensorType();
                NH3.ZeroPoint = 0;
                NH3.MaxPoint = 80.06;
                NH3.Name = "NH3 Detector";

                SensorType N2 = new SensorType();
                N2.ZeroPoint = -120;
                N2.MaxPoint = -40;
                N2.Name = "N2 Dewpoint Detector";

                context.Categories.Add(N2);
                context.Categories.Add(NH3);
                context.Categories.Add(o2);
                context.Categories.Add(h2);
                context.SaveChanges();
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        private static void ViewModelTesting() {
            using (var context = new FacilityContext()) {
                var device = context.ModbusDevices.OfType<GenericMonitorBox>().Include(e => e.Readings).Include(e => e.Channels).FirstOrDefault(e => e.Identifier == "GasBay");
                if (device != null) {
                    Console.WriteLine("Device Found: {0}", device.Identifier);
                    Console.WriteLine("Displaying Analog Channels");
                    device.Channels.OfType<AnalogChannel>().ToList().ForEach(x => {
                        Console.WriteLine("Name: {0}", x.Name);
                    });
                    Console.WriteLine("Should be done");
                } else {
                    Console.WriteLine("Error: Device Not Found");
                }
            }
            Console.ReadKey();
        }

        private static void TestingFacilityModel() {
            using (FacilityContext context = new FacilityContext()) {
                GenericMonitorBox monitorBox = new GenericMonitorBox();
                monitorBox.IpAddress = "172.21.100.30";
                monitorBox.Port = 0;
                monitorBox.Identifier = "GasBay";
                monitorBox.SlaveAddress = 0;
                monitorBox.Status = "Okay";
                monitorBox.AnalogChannelCount = 16;
                monitorBox.DigitalInputChannelCount = 38;
                monitorBox.DigitalOutputChannelCount = 10;
                monitorBox.ModbusComAddr = 39;
                monitorBox.SoftwareMaintAddr = 40;
                monitorBox.WarningAddr = 41;
                monitorBox.AlarmAddr = 42;
                context.ModbusDevices.Add(monitorBox);

                for (int i = 0; i < SlopeValues.Length; i++) {
                    bool connected;
                    if(i<3 || i==4 || i==5) {
                        connected = true;
                    } else {
                        connected = false;
                    }
                    AnalogChannel channel = new AnalogChannel(AnalogIdentifiers[i], i + 1, connected , "AnalogCh" + (i + 1));
                    channel.Slope = SlopeValues[i];
                    channel.Offset = OffsetValues[i];
                    channel.Resistance = RValues[i];
                    channel.GenericMonitorBox = monitorBox;
                    channel.ZeroValue = MinValues[i];
                    channel.MaxValue = MaxValues[i];
                    channel.Alarm1SetPoint = Alarm1SetPoints[i];
                    channel.Alarm2SetPoint = Alarm2SetPoints[i];
                    channel.Alarm3SetPoint = Alarm3SetPoints[i];
                    monitorBox.Channels.Add(channel);
                    context.Channels.Add(channel);
                }

                for(int i = 1; i < 39; i++) {
                    LogicType type = (i <= 22) ? LogicType.LOW : LogicType.HIGH;
                    DigitalInputChannel channel;
                    switch (i) {
                        case 7: {
                            channel = new DigitalInputChannel("DI Water Gauge", i, true, "DigitalCh"+i, type);
                            break;
                        }
                        case 8: {
                            channel = new DigitalInputChannel("Ammonia Gauge 2", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        case 9: {
                            channel = new DigitalInputChannel("Ammonia Gauge 1", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        case 10: {
                            channel = new DigitalInputChannel("Hydrogen Gauge", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        case 22: {
                            channel = new DigitalInputChannel("Maint. Key Switch", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        case 23: {
                            channel = new DigitalInputChannel("Power Supply 1", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        case 24: {
                            channel = new DigitalInputChannel("Power Supply 2", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        case 25: {
                            channel = new DigitalInputChannel("Nitrogen Gauge", i, true, "DigitalCh" + i, type);
                            break;
                        }
                        default: {
                            channel = new DigitalInputChannel("None", i, false, "DigitalCh" + i, type);
                            break;
                        }
                    }
                    monitorBox.Channels.Add(channel);
                    context.Channels.Add(channel);
                }

                for (int i = 1; i < 11; i++) {
                    DigitalOutputChannel channel;
                    LogicType type = (i >= 7) ? LogicType.LOW : LogicType.HIGH;
                    switch (i) {
                        case 1: {
                            channel = new DigitalOutputChannel("Bad Channel", i, true, "OutputCh" + i, type);
                            break;
                        }
                        case 2: {
                            channel = new DigitalOutputChannel("Bad Channel", i, true, "OutputCh" + i, type);
                            break;
                        }
                        case 7: {
                            channel = new DigitalOutputChannel("Pump Emg. On/Off", i, true, "OutputCh" + i, type);
                            break;
                        }
                        case 8: {
                            channel = new DigitalOutputChannel("Green/Okay", i, true, "OutputCh" + i, type);
                            break;
                        }
                        case 9: {
                            channel = new DigitalOutputChannel("Yellow/Warning", i, true, "OutputCh" + i, type);
                            break;
                        }
                        case 10: {
                            channel = new DigitalOutputChannel("Red/Alarm", i, true, "OutputCh" + i, type);
                            break;
                        }
                        default: {
                            channel = new DigitalOutputChannel("None", i, false, "OutputCh" + i, type);
                            break;
                        }
                    }
                    monitorBox.Channels.Add(channel);
                    context.Channels.Add(channel);
                }
                context.SaveChanges();
            }
            Console.WriteLine("Should be done");
            Console.Read();
        }

        private static void TestingProgram() {
            if (CheckConnection("172.21.100.30", 500)) {
                while (true) {
                    bool exit = false;
                    var key = DisplayMenu();
                    switch (key) {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1: {
                            PutInMaintenceMode();
                            break;
                        }

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2: {
                            RaiseAlarm();
                            break;
                        }

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3: {
                            RaiseWarning();
                            break;
                        }

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4: {
                            ReadAnalog();
                            break;
                        }

                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5: {
                            ReadDigitalPullup();
                            break;
                        }

                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6: {
                            ReadDigital24v();
                            break;
                        }

                        case ConsoleKey.D7:
                        case ConsoleKey.NumPad7: {
                            exit = true;
                            break;
                        }

                        default: {
                            Console.WriteLine("Invalid Selection, Please Try Again");
                            break;
                        }
                    }
                    if (exit) {
                        Console.WriteLine("Thank you Come Again!");
                        break;
                    }
                }
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static ConsoleKey DisplayMenu() {
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: Put In Maintence Mode");
            Console.WriteLine("2: Raise Alarm");
            Console.WriteLine("3: Raise Warning");
            Console.WriteLine("4: Read Analog");
            Console.WriteLine("5: Read Digital Pull-ups");
            Console.WriteLine("6: Read Digital 24v");
            Console.WriteLine("7: Exit");
            var responce = Console.ReadKey();
            return responce.Key;
        }

        private static void RaiseAlarm() {
            Console.Clear();
            Console.WriteLine("Raising Alarm Please Wait....");
            if (CheckConnection("172.21.100.30", 500)) {
                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    bool[] com = { true, false, false, false };
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    com[1] = false;
                    com[2] = false;
                    com[3] = true;
                    master.WriteMultipleCoils(39, com);
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    bool success=false;
                    while (true) {
                        var check=master.ReadCoils(39, 1);
                        if (!check[0]) {
                            Console.WriteLine("Success, Waiting 2sec then turning off Alarm");
                            success = true;
                            break;
                        }
                    }
                    if (success) {
                        System.Threading.Thread.Sleep(5000);
                        com[1] = false;
                        com[2] = false;
                        com[3] = false;
                        master.WriteMultipleCoils(39, com);
                    }
                    client.Close();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void RaiseWarning() {
            Console.Clear();
            Console.WriteLine("Raising Warning Signal Please Wait....");
            if (CheckConnection("172.21.100.30", 500)) {
                bool[] com = { true, false, false, false };

                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    com[1] = false;
                    com[2] = true;
                    com[3] = false;
                    master.WriteMultipleCoils(39, com);
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    bool success = false;
                    while (true) {
                        var check = master.ReadCoils(39, 1);
                        if (!check[0]) {
                            Console.WriteLine("Success, Waiting 5 sec then turning off Warning");
                            success = true;
                            break;
                        }
                    }
                    if (success) {
                        System.Threading.Thread.Sleep(5000);
                        com[1] = false;
                        com[2] = false;
                        com[3] = false;
                        master.WriteMultipleCoils(39, com);
                    }
                    client.Close();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ReadAnalog() {
            Console.Clear();
            Console.WriteLine("Retrieving Values, Please Wait");
            if (CheckConnection("172.21.100.30", 100)) {
                ushort[] regData;
                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    regData = master.ReadHoldingRegisters(0, 16);
                    client.Close();
                }
                Console.WriteLine();
                Console.WriteLine("Analog Values");
                for (int i = 0; i < regData.Length; i++) {
                    double x = regData[i];
                    x = (x / 1000);
                    double y = SlopeValues[i] * x + OffsetValues[i];
                    double current = (RValues[i] != 0) ? (y / RValues[i]) * 1000 : 0.00;
                    Console.WriteLine(" A{0}: Voltage: {1} Current: {2}", i, Math.Round(y,3), Math.Round(current,3));
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ReadDigitalPullup() {
            Console.Clear();
            Console.WriteLine("Retrieving Values, Please Wait");
            if (CheckConnection("172.21.100.30", 100)) {
                bool[] coilData;
                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    coilData = master.ReadCoils(0, 48);
                    client.Close();
                }
                Console.WriteLine("Digitals Pull-up: ");
                for (int i = 0; i < 22; i++) {
                    Console.WriteLine(" D{0}: {1}", i+1, coilData[i]);
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ReadDigital24v() {
            Console.Clear();
            Console.WriteLine("Retrieving Values, Please Wait");
            if (CheckConnection("172.21.100.30", 100)) {
                bool[] coilData;
                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    coilData = master.ReadCoils(0, 48);
                    client.Close();
                }
                Console.WriteLine();
                Console.WriteLine("Digitals 24Volt");
                for (int i = 22; i < 38; i++) {
                    Console.WriteLine(" D{0}: {1}", i-22, coilData[i]);
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void PutInMaintenceMode() {
            Console.Clear();
            Console.WriteLine("Switching To Maintence Mode Please Wait....");
            if (CheckConnection("172.21.100.30", 500)) {
                ushort[] regData = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    bool[] com = { true, false, false, false };
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    com[1] = true;
                    com[2] = false;
                    com[3] = false;
                    master.WriteMultipleCoils(39, com);
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    bool success = false;
                    while (true) {
                        var check = master.ReadCoils(39, 1);
                        if (!check[0]) {
                            Console.WriteLine("Success, Waiting 5 sec then turning off Warning");
                            success = true;
                            break;
                        }
                    }
                    if (success) {
                        System.Threading.Thread.Sleep(5000);
                        com[1] = false;
                        com[2] = false;
                        com[3] = false;
                        master.WriteMultipleCoils(39, com);
                    }
                    client.Close();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ReadAndDisplay() {
            //using(var context=new FacilityContext()) {
            //    ModbusDevice device = new ModbusDevice("GB-1","Gas Bay", "172.20.1.62", 502, 0, "");
            //    context.Add(device);
            //    context.SaveChanges();
            //}
            //Console.BufferHeight = 800;
            //Console.BufferWidth = 800;
            //Console.WindowHeight = 600;
            //Console.WindowWidth = 600;

            if (CheckConnection("172.21.100.30", 100)) {
                ushort[] regData;
                bool[] coilData;
                while (true) {
                    using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                        regData = master.ReadHoldingRegisters(0, 16);
                        coilData = master.ReadCoils(0, 48);
                        client.Close();
                    }
                    Console.WriteLine("Analog");
                    for (int i = 0; i < regData.Length; i++) {
                        double x = regData[i];
                        x = (x / 1000);
                        double y = SlopeValues[i] * x + OffsetValues[i];
                        double current = (RValues[i] != 0) ? (y / RValues[i]) * 1000 : 0.00;

                        Console.WriteLine(" A{0}: Voltage: {1} Current: {2}", i, y, current);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Digitals Pull-up: ");
                    for (int i = 0; i < 22; i++) {
                        Console.WriteLine(" D{0}: {1}", i, coilData[i]);
                    }

                    //Console.WriteLine("Digitals 24Volt");
                    //for (int i = 22; i < 38; i++) {
                    //    Console.WriteLine(" D{0}: {1}", i, coilData[i]);
                    //}

                    //Console.WriteLine("Output States: ");
                    //for (int i = 38; i < coilData.Length; i++) {
                    //    Console.WriteLine(" O{0}: {1}", i, coilData[i]);
                    //}
                    Console.WriteLine();
                    Console.WriteLine("Press C to continue");
                    var key = Console.ReadKey();
                    if (key.Key != ConsoleKey.C)
                        break;
                }
            } else {
                Console.WriteLine("Connection Failed");
            }
            Console.ReadKey();
        }

        private static bool CheckConnection(string address, int timeout) {
            try {
                Ping check = new Ping();
                PingReply reply = check.Send(address, 1000);
                if (reply.Status == IPStatus.Success) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception) {
                return false;
            }
        }

        private static void NHReadTimer() {
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

        private static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh) {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
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
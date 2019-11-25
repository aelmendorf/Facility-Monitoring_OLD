using Console_Table;
using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace FacilityMonitoring.ConsoleTesting {
    class Program3 {
        static string[] AnalogIdentifiers = {"H2 Detector 1","NH3 Detector","O2 Detector","Bad Channel","N2 Dew Point","H2 Detector",
                                            "Bad Channel" , "Bad Channel","Not Connected","Not Connected","Not Connected","Not Connected" ,"Not Connected","Not Connected" ,
                                            "Not Connected","Not Connected"};
        static double[] SlopeValues = { 1.00476, 1.00577, 1.004007, 1.00488, 1.00448, 1.00454, .99849, 0.000, 1.00448, 1.00455, 1.00450, 1.00451, 1.00456, 1.00414, 1.00456, 0 };
        static double[] OffsetValues = { 0.00998, 0.00920, .008976, 0.00919, 0.00744, 0.00791, -.00462, 0.0000, 0.00787, 0.00763, 0.00761, 0.00761, 0.00761, 0.00833, 0.00782, 0.0000 };
        static double[] RValues = { 250.81, 250.474, 246.2776, 210.223, 250.58, 240.204, 332.018, 0.000, 250.902, 250.918, 250.684, 250.808, 251.002, 250.576, 250.821, 0.000 };
        static double[] MinValues = { 4, 4, 4, 0, 4, 4.152, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] MaxValues = { 20, 20, 20, 0, 20, 20.868, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm1SetPoints = { 300, 10, 0, 0, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm2SetPoints = { 500, 25, 23, 0, -118, 500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm3SetPoints = { 1000, 50, 25, 0, -118, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static string IpAddress = "172.20.1.202";

        static void Main(string[] args) {
            //ImportModbus();
            //TestingFacilityModel();
            ////ViewModelTesting();
            //TestingCategories();
            //TestingCategories2();
            //TestingAnalogRead();
            //TestingAddChannels();
            //DisplayMeasurments();
            TestingProgram();
            //ReadAndDisplay2();
            //SendMaintNew();
            //SendAlarmNew(false);
        }


        private static void TestingProgram() {
            if (CheckConnection(IpAddress, 500)) {
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
                        Console.WriteLine("Thank you Come Again!,press any key to exit");
                        Console.ReadKey();
                        break;
                    }
                }
            } else {
                Console.WriteLine("Connection Failed,Press any key to exit");
                Console.ReadKey();
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
            if (CheckConnection(IpAddress, 500)) {
                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    bool[] com = { true, false, false, false };
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    com[1] = false;
                    com[2] = false;
                    com[3] = true;
                    master.WriteMultipleCoils(39, com);
                    bool success = false;
                    while (true) {
                        var check = master.ReadCoils(39, 1);
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
            if (CheckConnection(IpAddress, 500)) {
                bool[] com = { true, false, false, false };

                using (TcpClient client = new TcpClient("172.21.100.30", 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    com[1] = false;
                    com[2] = true;
                    com[3] = false;
                    master.WriteMultipleCoils(39, com);
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
            if (CheckConnection(IpAddress, 100)) {
                ushort[] regData;
                using (TcpClient client = new TcpClient(IpAddress, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    regData = master.ReadHoldingRegisters(0, 16);
                    client.Close();
                }
                Console.WriteLine();
                Console.WriteLine("Analog Values");
                for (int i = 0; i < regData.Length; i++) {
                    double x = regData[i];
                    x = (x / 1000);
                    //double y = SlopeValues[i] * x + OffsetValues[i];
                    //double current = (RValues[i] != 0) ? (y / RValues[i]) * 1000 : 0.00;
                    //Console.WriteLine(" A{0}: Voltage: {1} Current: {2}", i, Math.Round(y, 3), Math.Round(current, 3));
                    Console.WriteLine(" A{0}: Voltage: {1}", i,Math.Round(x,3));
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
            if (CheckConnection(IpAddress, 100)) {
                bool[] coilData;
                using (TcpClient client = new TcpClient(IpAddress, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    coilData = master.ReadCoils(0, 48);
                    client.Close();
                }
                Console.WriteLine("Digitals Pull-up: ");
                for (int i = 0; i < 22; i++) {
                    Console.WriteLine(" D{0}: {1}", i + 1, coilData[i]);
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
            if (CheckConnection(IpAddress, 100)) {
                bool[] coilData;
                using (TcpClient client = new TcpClient(IpAddress, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    coilData = master.ReadCoils(0, 48);
                    client.Close();
                }
                Console.WriteLine();
                Console.WriteLine("Digitals 24Volt");
                for (int i = 22; i < 38; i++) {
                    Console.WriteLine(" D{0}: {1}", i - 22, coilData[i]);
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
            if (CheckConnection(IpAddress, 500)) {
                ushort[] regData = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
                using (TcpClient client = new TcpClient(IpAddress, 502)) {
                    bool[] com = { true, false, false, false };
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    com[1] = true;
                    com[2] = false;
                    com[3] = false;
                    master.WriteMultipleCoils(39, com);
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
            if (CheckConnection(IpAddress, 100)) {
                ushort[] regData;
                bool[] coilData;
                while (true) {
                    using (TcpClient client = new TcpClient(IpAddress, 502)) {
                        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                        regData = master.ReadHoldingRegisters(0, 16);
                        coilData = master.ReadCoils(0, 48);
                        client.Close();
                        master.Dispose();
                    }
                    Console.WriteLine("Analog");
                    for (int i = 0; i < regData.Length; i++) {
                        //double x = regData[i];
                        //x = (x / 1000);
                        //double y = SlopeValues[i] * x + OffsetValues[i];
                        //double current = (RValues[i] != 0) ? (y / RValues[i]) * 1000 : 0.00;

                        Console.WriteLine(" A{0}: Voltage: {1} Current: {2}", i,regData[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Digitals Pull-up: ");
                    for (int i = 0; i < 22; i++) {
                        Console.WriteLine(" D{0}: {1}", i, coilData[i]);
                    }

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

        private static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh) {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }
    }
}

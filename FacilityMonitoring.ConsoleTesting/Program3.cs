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

        static double[] SlopeValues2 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        static double[] OffsetValues2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] RValues2 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        //static double[] SlopeValues2 = { 1.01174103164028,1.01223435559576,1.01215128068412,1.01174426957096,1.01281383805584,
        //    1.00082150332614,1.01185081367171,1.0125878200513,1.01223915022351,1.01209679127947,1.01263492500699,1.01218416712602,
        //    1.01234793849366,1.01285282346365,1.01306112354811,1.01231805170184};
        //static double[] OffsetValues2 = {0.00882890508871403,0.00816490767368672,
        //    0.00940871971475588,0.00837291496113712,0.00951697364154436,0.0224976841510642,
        //    0.00972872122543178,0.0101032927963121,0.0088550029428931,0.00957254261189355,
        //    0.0092690590719684,0.00987601328296295,0.0093797500824353, 0.00848172687139748,
        //    0.00841940842012434,0.0104318273225825};
        //static double[] RValues2 = {247.925473427001,248.257788637752,249.254734270006,
        //    248.627367135003,249.013439218082,248.643249847282,248.020158827123,
        //    248.114233353696,249.453879047037,249.040317654246,248.362248014661,
        //    249.221136224801,248.781307269395,248.401343921808,248.957849725107,249.018937080024};

        static double[] MinValues = { 4, 4, 4, 0, 4, 4.152, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] MaxValues = { 20, 20, 20, 0, 20, 20.868, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm1SetPoints = { 300, 10, 0, 0, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm2SetPoints = { 500, 25, 23, 0, -118, 500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static double[] Alarm3SetPoints = { 1000, 50, 25, 0, -118, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static bool maintState, alarmState, warnState, okayState = false;
        //static ushort CoilCount = 4;
        //static ushort DiscreteInputCount = 46;
        //static ushort HoldingRegisterCount = 16;

        static ushort CoilCount = 4;
        static ushort DiscreteInputCount = 54;
        static ushort HoldingRegisterCount = 16;


        static string IpAddress = "172.20.5.46";
        //static string IpAddress = "172.21.100.31";

        static void Main(string[] args) {
            //ImportModbus();
            //TestingFacilityModel();
            ////ViewModelTesting();
            //TestingCategories();
            //TestingCategories2();
            //TestingAnalogRead();
            //TestingAddChannels();
            //DisplayMeasurments();
            //TestingProgram();
            //RaiseWarning();
            //ReadAndDisplay2();
            //SendMaintNew();
            //SendAlarmNew(false);
            //ReadAnalog();
            //RaiseAlarmNew();
            //ReadDigital();
            // ReadAnalogSimple();
            TestingProgram();
        }

        private static void TestingProgram() {
            if (CheckConnection(IpAddress, 500)) {
                while (true) {
                    bool exit = false;
                    var key = DisplayMenu();
                    switch (key) {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1: {
                            ToggleMaintence();
                            break;
                        }

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2: {
                            ToggleAlarm();
                            break;
                        }

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3: {
                            ToggleWarning();
                            break;
                        }

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4: {
                            ReadAnalog();
                            break;
                        }

                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5: {
                            ReadCoils();
                            break;
                        }

                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6: {
                            ReadDiscrete();
                            break;
                        }

                        case ConsoleKey.D7:
                        case ConsoleKey.NumPad7:{
                            ReadAndDisplay();
                            break;
                        }

                        case ConsoleKey.D8:
                        case ConsoleKey.NumPad8: {
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
            Console.WriteLine("1: Toggle Maintence Mode");
            Console.WriteLine("2: Toggle Alarm");
            Console.WriteLine("3: Toggle Warning");
            Console.WriteLine("4: Read Analog");
            Console.WriteLine("5: Read Coils ");
            Console.WriteLine("6: Read Discrete ");
            Console.WriteLine("7: Read All");
            Console.WriteLine("8: Exit");
            var responce = Console.ReadKey();
            return responce.Key;
        }

        private static void ToggleMaintence()
        {
            Console.Clear();
            Console.WriteLine("Switching To Maintence Mode Please Wait....");
            if (CheckConnection(IpAddress, 500))
            {
                using (TcpClient client = new TcpClient(IpAddress, 502))
                {
                    bool[] com = { true, false, false, false };
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    maintState = !maintState;
                    master.WriteSingleCoil(1, 2, maintState);
                    client.Close();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ToggleAlarm(){
            Console.WriteLine("Raising Alarm Please Wait....");
            if (CheckConnection(IpAddress, 500))
            {
                using (TcpClient client = new TcpClient(IpAddress, 502))
                {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    alarmState= !alarmState;
                    master.WriteSingleCoil(1,0, alarmState);
                    client.Close();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }else
            {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ToggleWarning() {
            Console.Clear();
            Console.WriteLine("Raising Warning Signal Please Wait....");
            if (CheckConnection(IpAddress, 500)) {
                bool[] com = { true, false, false, false };

                using (TcpClient client = new TcpClient(IpAddress, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    warnState = !warnState;
                    master.WriteSingleCoil(1,1,warnState);
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
                    regData = master.ReadInputRegisters(1,0, HoldingRegisterCount);
                    client.Close();
                }
                for(int i=0; i < regData.Length; i++) {
                    Console.WriteLine("A{0}:{1}",i,regData[i]);
                }
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ReadCoils() {
            Console.Clear();
            Console.WriteLine("Retrieving Values, Please Wait");
            if (CheckConnection(IpAddress, 100)) {
                bool[] coilData;
                using (TcpClient client = new TcpClient(IpAddress, 502)) {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    coilData = master.ReadCoils(1,0,CoilCount);
                    client.Close();
                }
                Console.WriteLine("Coils ");
                for (int i = 0; i < coilData.Length; i++) {
                    Console.WriteLine(" C{0}: {1}", i + 1, coilData[i]);
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } else {
                Console.WriteLine("Connection Failed");
            }
        }

        private static void ReadDiscrete()
        {
            Console.Clear();
            Console.WriteLine("Retrieving Values, Please Wait");
            if (CheckConnection(IpAddress, 100))
            {
                bool[] inputData;
                using (TcpClient client = new TcpClient(IpAddress, 502))
                {
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    inputData = master.ReadCoils(1, 0, DiscreteInputCount);
                    client.Close();
                }

                Console.WriteLine("Discrete Inputs: ");
                for (int i = 0; i < inputData.Length; i++){
                    Console.WriteLine(" C{0}: {1}", i + 1, inputData[i]);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Connection Failed");
            }
        }


        private static void ReadAndDisplay() {
            if (CheckConnection(IpAddress, 100)) {
                ushort[] regData;
                bool[] coilData;
                bool[] inputData;
                while (true) {
                    using (TcpClient client = new TcpClient(IpAddress, 502)) {
                        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                        regData = master.ReadInputRegisters(1,0, HoldingRegisterCount);
                        coilData = master.ReadCoils(1,0, CoilCount);
                        inputData = master.ReadInputs(1, 0, DiscreteInputCount);
                        client.Close();
                        master.Dispose();
                    }
                    Console.WriteLine();
                    Console.WriteLine("Analog");
                    for (int i = 0; i < regData.Length; i++) {
                        Console.WriteLine(" A{0}: {1}", i,regData[i]);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Discrete Inputs");
                    for (int i = 0; i < inputData.Length; i++){
                        Console.WriteLine("D{0}: {1}", i, inputData[i]);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Coils:  ");
                    for (int i = 0; i < coilData.Length; i++) {
                        Console.WriteLine(" C{0}: {1}", i, coilData[i]);
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

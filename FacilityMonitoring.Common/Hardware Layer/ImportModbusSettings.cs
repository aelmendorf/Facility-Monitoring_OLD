using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FacilityMonitoring.Common.Harware {
    public static class ImportModbusSettings {
        private static readonly string analogFile = @"D:\Software Development\Monitoring\ImportFiles\AnalogChannels.txt";
        private static readonly string digitalFile = @"D:\Software Development\Monitoring\ImportFiles\DigitalChannels.txt";
        private static readonly string outputFile = @"D:\Software Development\Monitoring\ImportFiles\OutputChannels.txt";
        private static readonly string sensorFile = @"D:\Software Development\Monitoring\ImportFiles\SensorTypes.txt";     

        public static bool ImportAnalog(GenericMonitorBox box,FacilityContext context) {
            if (File.Exists(analogFile)) {
                try {
                    List<AnalogChannel> channels = new List<AnalogChannel>();
                    var lines = File.ReadAllLines(analogFile);
                    foreach (var line in lines) {
                        var row = line.Split('\t');
                        bool connected = (row[3] == "TRUE") ? true : false;
                        AnalogChannel channel = new AnalogChannel(row[0],Convert.ToInt32(row[1]),Convert.ToInt32(row[2]),connected,row[4]);
                        channel.Slope = Convert.ToDouble(row[5]);
                        channel.Offset= Convert.ToDouble(row[6]);
                        channel.Resistance= Convert.ToDouble(row[7]);

                        channel.ZeroValue = Convert.ToDouble(row[8]);
                        channel.MaxValue= Convert.ToDouble(row[9]);

                        channel.Alarm1SetPoint = Convert.ToDouble(row[10]);
                        channel.Alarm1Enabled = (row[11] == "TRUE") ? true : false;
                        channel.Alarm1Action = GetAction(row[12]);

                        channel.Alarm2SetPoint = Convert.ToDouble(row[13]);
                        channel.Alarm2Enabled = (row[14] == "TRUE") ? true : false;
                        channel.Alarm2Action = GetAction(row[15]);

                        channel.Alarm1SetPoint = Convert.ToDouble(row[16]);
                        channel.Alarm1Enabled = (row[17] == "TRUE") ? true : false;
                        channel.Alarm1Action = GetAction(row[18]);
                        string sname = row[19];
                        channel.ValueDivisor = Convert.ToDouble(row[20]);
                        var sensor = context.Categories.OfType<SensorType>().Include(e => e.AnalogChannels).FirstOrDefault(e=>e.Name==sname);
                        if (sensor != null) {
                            channel.SensorType = sensor;
                        }
                        channel.GenericMonitorBox = box;
                        box.Registers.Add(channel);
                        context.Registers.Add(channel);
                    }
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static bool ImportDigital(GenericMonitorBox box, FacilityContext context) {
            if (File.Exists(digitalFile)) {
                try {
                    List<DigitalInputChannel> channels = new List<DigitalInputChannel>();
                    var lines = File.ReadAllLines(digitalFile);
                    foreach (var line in lines) {
                        var row = line.Split('\t');
                        bool connected = (row[4] == "TRUE") ? true : false;
                        LogicType type = (row[3] == "HIGH") ? LogicType.HIGH : LogicType.LOW;
                        DigitalInputChannel channel = new DigitalInputChannel(row[0], Convert.ToInt32(row[1]), Convert.ToInt32(row[2]), connected, row[5],type);
                        channel.AlarmAction = GetAction(row[7]);
                        channel.Bypass = (row[6] == "TRUE") ? false : true;              
                        channel.GenericMonitorBox = box;
                        box.Registers.Add(channel);
                        context.Registers.Add(channel);
                    }
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static bool ImportOutput(GenericMonitorBox box, FacilityContext context) {
            if (File.Exists(outputFile)) {
                try {
                    List<DigitalOutputChannel> channels = new List<DigitalOutputChannel>();
                    var lines = File.ReadAllLines(outputFile);
                    foreach (var line in lines) {
                        var row = line.Split('\t');
                        bool connected = (row[4] == "TRUE") ? true : false;
                        LogicType type = (row[3] == "HIGH") ? LogicType.HIGH : LogicType.LOW;
                        DigitalOutputChannel channel = new DigitalOutputChannel(row[0], Convert.ToInt32(row[1]),Convert.ToInt32(row[2]), connected, row[5], type);
                        channel.OutputControl = (row[7] == "SOFTWARE") ? OutputControl.SOFTWARE : OutputControl.HARDWARE;
                        channel.Bypass = (connected) ? false : true;
                        channel.GenericMonitorBox = box;
                        box.Registers.Add(channel);
                        context.Registers.Add(channel);
                    }
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static bool ImportSensorType(GenericMonitorBox box, FacilityContext context) {
            if (File.Exists(sensorFile)) {
                try {
                    var lines = File.ReadAllLines(sensorFile);
                    foreach (var line in lines) {
                        var row = line.Split('\t');

                        SensorType sensor = new SensorType();
                        sensor.Name = row[0];
                        sensor.ZeroPoint = Convert.ToDouble(row[1]);
                        sensor.MaxPoint = Convert.ToDouble(row[2]);
                        sensor.Units = row[3];
                        context.Categories.Add(sensor);
                    }
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static AlertAction GetAction(string str) {
            switch (str) {
                case "ALARM": {
                    return AlertAction.ALARM;
                }
                case "WARN": {
                    return AlertAction.WARN;
                }
                case "SOFTWARN": {
                    return AlertAction.SOFTWARN;
                }
                case "NOTHING": {
                    return AlertAction.NOTHING;
                }
                case "MAINTENANCE": {
                    return AlertAction.MAINTENANCE;
                }
                default: {
                    return AlertAction.NOTHING;
                }

            }
        }
    }

}

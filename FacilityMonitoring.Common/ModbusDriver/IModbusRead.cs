using FacilityMonitoring.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Modbus.Device;
using ModbusDevice = FacilityMonitoring.Common.Model.ModbusDevice;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace FacilityMonitoring.Common.Server {
    public interface IModbusOperations {
        Tuple<ushort[],bool[]> Read(GenericMonitorBox device);
        void Write(GenericMonitorBox device, ushort regStart, ushort length);
        Tuple<ushort[], bool[]> Read(ModbusDevice device);
        void Write(ModbusDevice device, ushort regStart, ushort length);

        bool CheckConnection(ModbusDevice device);
    }

    public class ModbusOperations : IModbusOperations {

        public bool CheckConnection(ModbusDevice device) {
            try {
                Ping check = new Ping();
                PingReply reply = check.Send(device.IpAddress, 500);
                if (reply.Status == IPStatus.Success) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception) {
                return false;
            }
        }

        public Tuple<ushort[], bool[]> Read(GenericMonitorBox device) {
            if (this.CheckConnection(device)) {
                ushort[] regData;
                bool[] coilData;
                try {
                    using (TcpClient client = new TcpClient(device.IpAddress, 502)) {
                        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                        regData = master.ReadHoldingRegisters(0, (ushort)device.AnalogChannelCount);
                        coilData = master.ReadCoils(0, (ushort)device.DigitalInputChannelCount);
                        client.Close();
                    }
                    return new Tuple<ushort[], bool[]>(regData, coilData);
                } catch {
                    return null;
                }
            } else {
                return null;
            }
        }

        public Tuple<ushort[], bool[]> Read(ModbusDevice device) {
            return null;
        }

        public void Write(GenericMonitorBox device, ushort regStart, ushort length) => throw new NotImplementedException();
        public void Write(ModbusDevice device, ushort regStart, ushort length) => throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;

namespace FacilityMonitoring.Common.Hardware {

    public class ModbusOperations : IModbusOperations {

        public string IpAddress { get; set; }
        public int Port { get; set; }
        public byte SlaveAddress { get; set; }

        public ModbusOperations(string ip, int port, int slaveAddr = 0) {
            this.IpAddress = ip;
            this.Port = port;
            this.SlaveAddress = (byte)slaveAddr;
        }



        public ushort[] ReadRegisters(int address, int length) {
            ushort[] regData;
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        regData = master.ReadHoldingRegisters(this.SlaveAddress, (ushort)address, (ushort)length);
                    } else {
                        regData = master.ReadHoldingRegisters((ushort)address, (ushort)length);
                    }
                    client.Close();
                    master.Dispose();
                    return regData;
                } catch {
                    return null;
                }
            return null;
        }

        public async Task<ushort[]> ReadRegistersAsync(int address, int length) {
            ushort[] regData;
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        regData = await master.ReadHoldingRegistersAsync(this.SlaveAddress, (ushort)address, (ushort)length);
                    } else {
                        regData = await master.ReadHoldingRegistersAsync((ushort)address, (ushort)length);
                    }
                    client.Close();
                    master.Dispose();
                    return regData;
                } catch {
                    return null;
                }
            return null;
        }


        public bool[] ReadCoils(int address, int length) {
            bool[] regData;
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        regData = master.ReadCoils(this.SlaveAddress, (ushort)address, (ushort)length);
                    } else {
                        regData = master.ReadCoils((ushort)address, (ushort)length);
                    }
                    client.Close();
                    master.Dispose();
                    return regData;
                } catch {
                    return null;
                }
            return null;
        }

        public async Task<bool[]> ReadCoilsAsync(int address, int length) {
            bool[] regData;
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        regData = await master.ReadCoilsAsync(this.SlaveAddress, (ushort)address, (ushort)length);
                    } else {
                        regData = await master.ReadCoilsAsync((ushort)address, (ushort)length);
                    }
                    client.Close();
                    master.Dispose();
                    return regData;
                } catch {
                    return null;
                }
            return null;
        }

        public Tuple<ushort[], bool[]> ReadRegistersAndCoils(int regAddress, int regLength, int coilAddr, int coilLength) {
            ushort[] regData;
            bool[] coilData;
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        regData = master.ReadHoldingRegisters(this.SlaveAddress, (ushort)regAddress, (ushort)regLength);
                        coilData = master.ReadCoils(this.SlaveAddress, (ushort)coilAddr, (ushort)coilLength);
                    } else {
                        regData = master.ReadHoldingRegisters((ushort)regAddress, (ushort)regLength);
                        coilData = master.ReadCoils((ushort)coilAddr, (ushort)coilLength);
                    }
                    client.Close();
                    master.Dispose();
                    return new Tuple<ushort[], bool[]>(regData,coilData);
                } catch {
                    return null;
                }
            return null;
        }

        public async Task<Tuple<ushort[], bool[]>> ReadRegistersAndCoilsAsync(int regAddress, int regLength, int coilAddr, int coilLength) {
            ushort[] regData;
            bool[] coilData;
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        regData =await master.ReadHoldingRegistersAsync(this.SlaveAddress, (ushort)regAddress, (ushort)regLength);
                        coilData =await master.ReadCoilsAsync(this.SlaveAddress, (ushort)coilAddr, (ushort)coilLength);
                    } else {
                        regData = await master.ReadHoldingRegistersAsync((ushort)regAddress, (ushort)regLength);
                        coilData = await master.ReadCoilsAsync((ushort)coilAddr, (ushort)coilLength);
                    }
                    client.Close();
                    master.Dispose();
                    return new Tuple<ushort[], bool[]>(regData, coilData);
                } catch {
                    return null;
                }
            return null;

        }

        public bool WriteRegisters(int address, ushort[] values) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        master.WriteMultipleRegisters(this.SlaveAddress, (ushort)address, values);
                    } else {
                        master.WriteMultipleRegisters((ushort)address, values);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public async Task<bool> WriteRegistersAsync(int address, ushort[] values) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        await master.WriteMultipleRegistersAsync(this.SlaveAddress, (ushort)address, values);
                    } else {
                        await master.WriteMultipleRegistersAsync((ushort)address, values);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public bool WriteCoils(int address, bool[] values) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        master.WriteMultipleCoils(this.SlaveAddress, (ushort)address, values);
                    } else {
                        master.WriteMultipleCoils((ushort)address, values);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public async Task<bool> WriteCoilsAsync(int address, bool[] values) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        await master.WriteMultipleCoilsAsync(this.SlaveAddress, (ushort)address, values);
                    } else {
                        await master.WriteMultipleCoilsAsync((ushort)address, values);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public bool WriteSingleRegister(int address, ushort value) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        master.WriteSingleRegister(this.SlaveAddress, (ushort)address, value);
                    } else {
                        master.WriteSingleRegister((ushort)address, value);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public async Task<bool> WriteSingleRegisterAsync(int address, ushort value) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        await master.WriteSingleRegisterAsync(this.SlaveAddress, (ushort)address, value);
                    } else {
                        await master.WriteSingleRegisterAsync((ushort)address, value);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public bool WriteSingleCoil(int address, bool value) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        master.WriteSingleCoil(this.SlaveAddress, (ushort)address, value);
                    } else {
                        master.WriteSingleCoil((ushort)address, value);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }

        public async Task<bool> WriteSingleCoilAsync(int address, bool value) {
            if (this.CheckConnection())
                try {
                    using TcpClient client = new TcpClient(this.IpAddress, this.Port);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    if (this.SlaveAddress != 0) {
                        await master.WriteSingleCoilAsync(this.SlaveAddress, (ushort)address, value);
                    } else {
                        await master.WriteSingleCoilAsync((ushort)address, value);
                    }
                    client.Close();
                    master.Dispose();
                    return true;
                } catch {
                    return false;
                }
            return false;
        }


        private bool CheckConnection() {
            try {
                Ping check = new Ping();
                PingReply reply = check.Send(this.IpAddress, 1000);
                if (reply.Status == IPStatus.Success) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception) {
                return false;
            }
        }
    }

}

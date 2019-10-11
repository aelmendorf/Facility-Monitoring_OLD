using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacilityMonitoring.Common.Data;
using FacilityMonitoring.Common.Model;

namespace FacilityMonitoring.Common.Converters {
    public static class RegisterConverters {

        public static float BAR_TO_PSI = 14.5037738f;
        public static float KGHR_TO_SLM = 199.656f;

        public static int ToInt32(ushort first,ushort second) {
            return BitConverter.ToInt32(BitConverter.GetBytes(second).Concat(BitConverter.GetBytes(first)).ToArray(),0);
        }

        public static double ToDouble(ushort first,ushort second) {
            return (double)BitConverter.ToSingle(BitConverter.GetBytes(first).Concat(BitConverter.GetBytes(second)).ToArray(), 0);
        }

        public static ushort[] ToUshortArray(int value) {
            ushort[] s = new ushort[2];
            byte[] fBytes = BitConverter.GetBytes(value);
            s[0] = BitConverter.ToUInt16(fBytes, 2);
            s[1] = BitConverter.ToUInt16(fBytes, 0);
            return s;
        }

        public static float[] GetFloatArray(object fltArray) {
            if (fltArray.GetType() == typeof(float[])) {
                object[] t = ((Array)fltArray).Cast<object>().ToArray();

                return Array.ConvertAll(t, item => (float)item);
            }
            return null;
        }

        public static ushort[] GetUshortArray(object array) {
            if (array.GetType() == typeof(ushort[])) {
                object[] t = ((Array)array).Cast<object>().ToArray();

                return Array.ConvertAll(t, item => (ushort)item);
            }
            return null;
        }

        public static ushort[] ConvertCalToReg(AmmoniaCalibrationData nHcal,int tank) {
            List<ushort> reg = new List<ushort>();
            reg.AddRange(ToUshortArray(nHcal.CalZero));
            reg.AddRange(ToUshortArray(nHcal.CalNonZero));
            reg.AddRange(ToUshortArray(nHcal.ActualZero));
            reg.AddRange(ToUshortArray(nHcal.ActualNonZero));
            reg.AddRange(ToUshortArray(nHcal.TotalWeight));
            reg.AddRange(ToUshortArray(nHcal.GasWeight));
            reg.Add((ushort)tank);
            return reg.ToArray();
        }

        public static ushort[] ConvertCalToReg(AmmoniaCalibrationData nHcal) {
            List<ushort> reg = new List<ushort>();
            reg.AddRange(ToUshortArray(nHcal.CalZero));
            reg.AddRange(ToUshortArray(nHcal.CalNonZero));
            reg.AddRange(ToUshortArray(nHcal.ActualZero));
            reg.AddRange(ToUshortArray(nHcal.ActualNonZero));
            reg.AddRange(ToUshortArray(nHcal.TotalWeight));
            reg.AddRange(ToUshortArray(nHcal.GasWeight));
            reg.Add(nHcal.Tank);
            return reg.ToArray();
        }

        public static byte[] GetBytes(ushort[] arr) {
            return arr.Select(x => (byte)x).ToArray();
        }

        public static H2Type GetH2Type(string str) {
            switch (str) {
                case "WATERLEVEL": {
                    return H2Type.WATERLEVEL;
                }

                case "WATERFLOW": {
                    return H2Type.WATERFLOW;
                }

                case "ONOFF": {
                    return H2Type.ONOFF;
                }

                case "INT32": {
                    return H2Type.INT32;
                }

                case "FAULTSTATE": {
                    return H2Type.FAULTSTATE;
                }

                case "ENABLESTATE": {
                    return H2Type.ENABLESTATE;
                }

                case "SYSTEMMODE": {
                    return H2Type.SYSTEMMODE;
                }

                case "OPERATIONMODE": {
                    return H2Type.OPERATIONMODE;
                }

                case "SYSTEMSTATE": {
                    return H2Type.SYSTEMSTATE;
                }

                case "DOUBLE": {
                    return H2Type.DOUBLE;
                }

                case "GENERATORSYSTEMWARNING": {
                    return H2Type.GENERATORSYSTEMWARNING;
                }

                case "GENERATORSYSTEMERROR": {
                    return H2Type.GENERATORSYSTEMERROR;
                }

                default: return H2Type.DOUBLE;
            }
        }

        public static FunctionCode GetFunctionCode(int num) {
            switch (num) {
                case 1: {
                    return FunctionCode.ReadCoil;
                }
                case 3: {
                    return FunctionCode.ReadHoldingRegisters;
                }
                case 4: {
                    return FunctionCode.ReadInputRegisters;
                }
                default: {
                    return FunctionCode.WriteMultipleCoils;
                }
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

        public static GeneratorSystemError ToGeneratorSystemError(ushort[] regData) {
            GeneratorSystemError error = new GeneratorSystemError();
            byte[] bytes = GetBytes(regData);
            for (int i = 0; i < bytes.Length; i++) {
                SystemError index = (SystemError)i;
                error[index] = (WarningErrorKey)bytes[i];
            }
            return error;
        }

        public static GeneratorSystemWarning ToGeneratorSystemWarning(ushort[] regData) {         
            GeneratorSystemWarning warning = new GeneratorSystemWarning();
            byte[] bytes = GetBytes(regData);
            for (int i = 0; i < bytes.Length; i++) {
                SystemWarning index = (SystemWarning)i;
                warning[index] = (WarningErrorKey)bytes[i];
            }
            return warning;
        }

        public static object GetH2RegisterValue(GeneratorRegister register,ushort[] regData=null,bool[] coilData = null) {
            switch (register.DataType) {
                case H2Type.WATERLEVEL: {
                    return coilData[0] ? WaterLevel.WET:WaterLevel.DRY;
                }
                case H2Type.WATERFLOW: {
                    return coilData[0] ? WaterFlow.FLOW : WaterFlow.FLOW;
                }
                case H2Type.ONOFF: {
                    return coilData[0] ? ONOFF.ON : ONOFF.OFF;
                }
                case H2Type.INT32: {
                    return (int)regData[0];
                }
                case H2Type.FAULTSTATE: {
                    return (FaultState)regData[0];
                }
                case H2Type.ENABLESTATE: {
                    return (EnableState)regData[0];
                }
                case H2Type.SYSTEMMODE: {
                    return (SystemMode)regData[0];
                }
                case H2Type.OPERATIONMODE: {
                    return (OperationMode)regData[0];
                }
                case H2Type.SYSTEMSTATE: {
                    return (SystemState)regData[0];
                }
                case H2Type.DOUBLE: {
                    return RegisterConverters.ToDouble(regData[0],regData[1]);
                }
                case H2Type.GENERATORSYSTEMWARNING: {
                    if (regData != null) {
                        return RegisterConverters.ToGeneratorSystemWarning(regData);
                    } else {
                        return null;
                    }
                }
                case H2Type.GENERATORSYSTEMERROR: {
                    if (regData != null) {
                        return RegisterConverters.ToGeneratorSystemError(regData);
                    } else {
                        return null;
                    }
                }
                default:
                    return null;
            }
        }

    }
}
